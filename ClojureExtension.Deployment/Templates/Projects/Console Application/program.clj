;Compilation notes:
; The vsClojure installer should have stored the core framework directory in the VSCLOJURE_RUNTIMES_DIR environment variable pointing to C:\Users\[username]\AppData\Local\Microsoft\VisualStudio\[version]\Extensions\[randomCharacters]\Runtimes\

(ns program
  (:gen-class))

(defn -main [& args]
  (println "Hello, World!")
  (println "Press any to quit...")
  (System.Console/ReadKey))