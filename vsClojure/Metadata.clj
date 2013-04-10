(System.Reflection.Assembly/Load "Microsoft.VisualStudio.Language.Intellisense, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")

(ns Metadata
  (:require [clojure.core])
  (:import (System.Linq Enumerable))
  (:import (System.IO Directory SearchOption Path))
  (:import (Microsoft.VisualStudio.Language.Intellisense Completion))
  (:gen-class
    :main false
    :name vsClojure.Metadata
    :methods [
              [LoadCoreCompletionsMatchingString [String] |System.Collections.Generic.IEnumerable`1[Microsoft.VisualStudio.Language.Intellisense.Completion]|]
             ]
    :constructors {[] []}
  )
)

(def clojureVersion (String/Format "{0}.{1}.{2}" (:major *clojure-version*) (:minor *clojure-version*) (:incremental *clojure-version*)))
(def VsClojureRuntimesDir (Environment/GetEnvironmentVariable "VSCLOJURE_RUNTIMES_DIR"))
(def clojureClrRuntime (String/Format "{0}\\{1}-{2}" VsClojureRuntimesDir "ClojureCLR" clojureVersion))

(defn searchFiles [directory wildcard recursive]
  (Directory/GetFiles directory  wildcard (if recursive SearchOption/AllDirectories SearchOption/TopDirectoryOnly))
)

(defn convertFileNameToNamespace [fileName runtimePath]
  (-> fileName (.Replace (str runtimePath Path/DirectorySeparatorChar) "") (.Replace "\\" ".") (.Replace ".clj" ""))
)

(defn getAllPublicFunctionsInNamespace [namespace]
  (let [ns (find-ns (symbol namespace))]
    (if ns (vals (ns-publics ns)) nil)
  )
)

(defn filterFunctionsByNameStartsWith [functions startsWithText]
  (filter (fn [x] (.StartsWith (str (:name (meta x))) startsWithText)) functions)
)

(defn convertFunctionToCompletion [function]
  (let* [
          functionMeta (meta function)
          name (:name functionMeta)
          namespace (:ns functionMeta)
          documentation (:doc functionMeta)
        ]
    (Completion. (str name "\t" namespace) (str namespace "/" name) documentation nil nil)
  )
)

(defn loadCoreCompletionsMatchingString [startsWithText]
  (let* [
         cljFiles (searchFiles clojureClrRuntime "*.clj" true)
         namespaces (map (fn [x] (convertFileNameToNamespace x clojureClrRuntime)) cljFiles)
         matchingFunctions (filterFunctionsByNameStartsWith (flatten (map getAllPublicFunctionsInNamespace namespaces)) startsWithText)
        ]

    (map convertFunctionToCompletion matchingFunctions)
  )
)

(defn -LoadCoreCompletionsMatchingString [this startsWithText]
  (Enumerable/Cast (type-args Completion) (loadCoreCompletionsMatchingString startsWithText))
)