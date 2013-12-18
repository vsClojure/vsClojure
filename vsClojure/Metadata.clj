(System.Reflection.Assembly/Load "Microsoft.VisualStudio.Language.Intellisense, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")

(ns Metadata
  (:require [clojure.core])
  (:import (System.Linq Enumerable))
  (:import (System.Text.RegularExpressions Regex RegexOptions))
  (:import (System.IO Directory SearchOption Path File FileInfo IOException))
  (:import (System.Reflection Assembly BindingFlags))
  (:import (Microsoft.VisualStudio.Language.Intellisense Completion))
  (:gen-class
    :main false
    :name vsClojure.Metadata
    :methods [[LoadCoreCompletionsMatchingString [String] |System.Collections.Generic.IEnumerable`1[Microsoft.VisualStudio.Language.Intellisense.Completion]|]
              [LoadCompletionsInAssemblyMatchingString [System.Reflection.Assembly String] |System.Collections.Generic.IEnumerable`1[Microsoft.VisualStudio.Language.Intellisense.Completion]|]
              [LoadCompletionsInCljFileMatchingString [String String] |System.Collections.Generic.IEnumerable`1[Microsoft.VisualStudio.Language.Intellisense.Completion]|]
              [GetCompilerErrors [String] |System.Collections.Generic.IEnumerable`1[System.String]|]]
    :constructors {[] []}))

(def clojureVersion (String/Format "{0}.{1}.{2}" (:major *clojure-version*) (:minor *clojure-version*) (:incremental *clojure-version*)))
(def VsClojureRuntimesDir (Environment/GetEnvironmentVariable "VSCLOJURE_RUNTIMES_DIR"))
(def clojureClrRuntime (String/Format "{0}\\{1}-{2}" VsClojureRuntimesDir "ClojureCLR" clojureVersion))

(defn search-files [directory wildcard recursive]
  (Directory/GetFiles directory  wildcard (if recursive SearchOption/AllDirectories SearchOption/TopDirectoryOnly)))

(defn convert-filename-to-namespace [fileName runtimePath]
  (-> fileName (.Replace (str runtimePath Path/DirectorySeparatorChar) "") (.Replace "\\" ".") (.Replace ".clj" "")))

(defn get-all-public-functions-in-namespace [namespace]
  (let [ns (find-ns (symbol namespace))]
    (if ns (vals (ns-publics ns)) nil)))

(defn filter-function-metas-by-name-starts-with [functionMetas startsWithText]
  (filter #(.StartsWith (str (:name %)) startsWithText StringComparison/InvariantCultureIgnoreCase) functionMetas))

(defn convert-function-meta-to-completion [functionMeta]
  (let* [name (:name functionMeta)
         namespace (:ns functionMeta)
         documentation (str (:arglists functionMeta) "\r" (:doc functionMeta))]
    (Completion. (str name "\t" namespace) (str namespace "/" name) documentation nil nil)))

(defn load-core-completions-matching-string [startsWithText]
  (let* [cljFiles (search-files clojureClrRuntime "*.clj" true)
         namespaces (map #(convert-filename-to-namespace % clojureClrRuntime) cljFiles)
         matchingFunctions (filter-function-metas-by-name-starts-with (map meta (flatten (map get-all-public-functions-in-namespace namespaces))) startsWithText)]
    (map convert-function-meta-to-completion matchingFunctions)))

(defn get-metadata-for-member-info [memberInfo]
  {:ns (.Namespace (.DeclaringType memberInfo))
   :name (.Name memberInfo)
   :arglists (if (instance? System.Reflection.MethodInfo memberInfo) (vec (map #(.Name %) (.GetParameters memberInfo))) '())})

(defn get-all-public-meta-in-assembly [assembly] 
  (let* [types (.GetTypes assembly)
         publicInstanceMethods (remove nil? (flatten (map #(seq (.GetMethods % (bit-or BindingFlags/Instance BindingFlags/Public))) types)))
         publicStaticMethods (remove nil? (flatten (map #(seq (.GetMethods % (bit-or BindingFlags/Static BindingFlags/Public))) types)))
         publicInstanceProperties (remove nil? (flatten (map #(seq (.GetProperties % (bit-or BindingFlags/Instance BindingFlags/Public))) types)))
         publicStaticProperties (remove nil? (flatten (map #(seq (.GetProperties % (bit-or BindingFlags/Static BindingFlags/Public))) types)))
         publicInstanceFields (remove nil? (flatten (map #(seq (.GetFields % (bit-or BindingFlags/Instance BindingFlags/Public))) types)))
         publicStaticFields (remove nil? (flatten (map #(seq (.GetFields % (bit-or BindingFlags/Static BindingFlags/Public))) types)))
         allPublicMembers (concat publicInstanceMethods publicStaticMethods publicInstanceProperties publicStaticProperties publicInstanceFields publicStaticFields)]
    (map get-metadata-for-member-info allPublicMembers)))

(defn -LoadCoreCompletionsMatchingString [this startsWithText]
  (Enumerable/Cast (type-args Completion) (load-core-completions-matching-string startsWithText)))

(defn load-completions-in-assembly-matching-string [assembly startsWithText]
  (let* [matchingFunctions (filter-function-metas-by-name-starts-with (get-all-public-meta-in-assembly assembly) startsWithText)]
    (map convert-function-meta-to-completion matchingFunctions)))

(defn -LoadCompletionsInAssemblyMatchingString [this assembly startsWithText]
  (Enumerable/Cast (type-args Completion) (load-completions-in-assembly-matching-string assembly startsWithText)))

(defn load-completions-in-clj-file-matching-string [cljFileName startsWithText]
  (let* [namespace (convert-filename-to-namespace cljFileName (Path/GetDirectoryName cljFileName))
         matchingFunctions (filter-function-metas-by-name-starts-with (map meta (get-all-public-functions-in-namespace namespace)) startsWithText)]
    (map convert-function-meta-to-completion matchingFunctions)))

(defn -LoadCompletionsInCljFileMatchingString [this cljFileName startsWithText]
  (Enumerable/Cast (type-args Completion) (load-completions-in-clj-file-matching-string cljFileName startsWithText)))

(defn indexes-of-characters [text characters]
  (map (fn [x] {:index (first x) :char (second x)}) (filter #(contains? (set characters) (second %)) (map-indexed (fn [x y] [x y]) text))))

(defn read-string-without-eval [s]
  (binding [*read-eval* false]
    (read-string s)))

(defn replace-string [s index replacement]
  (.Insert (.Remove s index (.Length replacement)) index replacement))

;todo: don't recalculate parenthesisCharacters every time, just remove them on each iteration
(defn match-parenthesis [codeWithoutStringsOrCommentsOrEscapedParenthesis]
  (loop [code codeWithoutStringsOrCommentsOrEscapedParenthesis result []]
    (let* [
          parenthesisCharacters (indexes-of-characters code [\( \)])
          pairedParenthesis (loop [head (first parenthesisCharacters) peek (second parenthesisCharacters) tail (rest parenthesisCharacters)]
                              (cond (empty? tail) {} (and (= (:char head) \() (= (:char peek) \))) {:leftIndex (:index head) :rightIndex (:index peek)} :else (recur peek (first tail) (rest tail))))]
      (cond (empty? pairedParenthesis) result :else (recur (replace-string (replace-string code (:rightIndex pairedParenthesis) " ") (:leftIndex pairedParenthesis) " ") (cons pairedParenthesis result))))))

(defn match-parenthesis-with-location [codeWithoutStringsOrCommentsOrEscapedParenthesis]
  (let* [pairedParenthesis (match-parenthesis codeWithoutStringsOrCommentsOrEscapedParenthesis)
         newlineCharacters (map :index (indexes-of-characters codeWithoutStringsOrCommentsOrEscapedParenthesis [\newline]))
         find-location (fn [parenthesis] (let [leftParenthesisNewLines (filter (fn [x] (< x (:leftIndex parenthesis))) newlineCharacters) rightParenthesisNewLines (filter (fn [x] (< x (:rightIndex parenthesis))) newlineCharacters)] { :startLine (inc (count leftParenthesisNewLines)) :startColumn (- (:leftIndex parenthesis) (or (last leftParenthesisNewLines) 0)) :endLine (inc (count rightParenthesisNewLines)) :endColumn (inc (- (:rightIndex parenthesis) (or (last rightParenthesisNewLines) 0))) }))]
    (map #(conj % (find-location %)) pairedParenthesis)))

(defn regex-replace [text regex replacementChar]
  (let [ matches (map (fn [x] {:index (.Index x) :length (.Length x)}) (.Matches regex text)) ]
    (reduce #(replace-string %1 (:index %2) (String. replacementChar (:length %2))) (cons text matches))))

(defn remove-strings-and-comments-and-escaped-parenthesis-from-code [code]
  (let* [codeWithoutSingleLineStrings (regex-replace code #"\"(\\\\|\\\"|[^\"\n])*\"" \ )
         codeWithoutSingleLineStringsOrComments (regex-replace codeWithoutSingleLineStrings (Regex. ";.*$" (RegexOptions/Multiline)) \ )
         codeWithoutSingleLineStringsOrCommentsOrMultiLineStrings (regex-replace codeWithoutSingleLineStringsOrComments #"\"(\\\\|\\\"|[^\"])*\"" \ )]
    (regex-replace codeWithoutSingleLineStringsOrCommentsOrMultiLineStrings #"(\\\(|\\\))" \ )))

(defn parse-for-error [expression]
  (try
    (do
      (read-string-without-eval (:code expression))
      nil)
    (catch Exception e
      (conj {:error (.Message e) } expression))))

(defn compile-string [code]
  (let* [cljFile (Path/ChangeExtension (Path/GetTempFileName) ".clj")
         cljFileWithoutPath (Path/GetFileName cljFile)
         cljFileWithoutPathOrExtension (.Remove cljFileWithoutPath (- (.Length cljFileWithoutPath) (.Length ".clj")))
         relativePath (Path/GetDirectoryName cljFile)]
    (File/Delete cljFile)
    (File/Delete (str cljFile ".dll"))
    (spit cljFile code)
    (Directory/SetCurrentDirectory relativePath)
    (binding [*compile-files* true]
      (clojure.lang.RT/load cljFileWithoutPathOrExtension false))))

(defn select-all-location [code]
  (let [newlineCharacters (map :index (indexes-of-characters code [\newline]))]
    {:startLine 1 :startColumn 1 :endLine (inc (count newlineCharacters)) :endColumn (inc (- (inc (last newlineCharacters)) (nth newlineCharacters (dec (count newlineCharacters)))))}))

(defn compile-for-error [code]
  (try
    (binding [*compile-path* (Directory/GetCurrentDirectory)]
      (compile-string code)
      nil)
    (catch IOException _)
    (catch Exception e (conj {:error (.Message e)} (select-all-location code)))))

(defn parse-for-errors [code]
  (let* [codeWithoutStringsOrCommentsOrEscapedParenthesis (remove-strings-and-comments-and-escaped-parenthesis-from-code code)
         parenthesisCharacters (indexes-of-characters codeWithoutStringsOrCommentsOrEscapedParenthesis [\( \)])
         pairedParenthesis (match-parenthesis-with-location codeWithoutStringsOrCommentsOrEscapedParenthesis)
         expressions (map #(conj {:code (.Substring code (:leftIndex %) (inc (- (:rightIndex %) (:leftIndex %)))) } %) pairedParenthesis)
         leftParenthesisCharacters (count (filter #(= \( (:char %)) parenthesisCharacters))
         rightParenthesisCharacters (count (filter #(= \) (:char %)) parenthesisCharacters))
         missingParenthesisError (cond (< leftParenthesisCharacters rightParenthesisCharacters) (conj {:error "Missing left parenthesis"} (select-all-location code)) (> leftParenthesisCharacters rightParenthesisCharacters) (conj { :error "Missing right parenthesis"} (select-all-location code)) :else nil)
         parseErrors (map parse-for-error expressions)]
    (remove nil? (cons missingParenthesisError parseErrors))))

(defn compiler-errors [code]
  (let [parseErrors (parse-for-errors code)
        compilerError (compile-for-error code)]
    (remove nil? (conj (vec parseErrors) compilerError))))

(defn -GetCompilerErrors [this code]
  (Enumerable/Cast (type-args System.String) (map #(str "(" (:startLine %) "," (:startColumn %) "," (:endLine %) "," (:endColumn %) "): " (:error %)) (compiler-errors code))))