;Compilation notes:
; The clojure compiler will generate many dlls [namespace].clj.dll & [classname].dll
; All the dlls & clj files should be automatically copied to the bin folder on run/build.
; The vsClojure installer should have stored the core framework directory in the CLOJURE_LOAD_PATH environment variable pointing to C:\Users\[username]\AppData\Local\Microsoft\VisualStudio\11.0\Extensions\[randomCharacters]\Runtimes\1.5.0

(ns program
  (:gen-class))

(defn -main [& args]
  (println "Press enter to quit...")
  (read-line)
)