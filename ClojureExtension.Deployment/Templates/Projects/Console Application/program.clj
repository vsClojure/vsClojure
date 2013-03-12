(ns program
  (:gen-class))

(defn -main [& args]
  (println "Press any key to quit...")
  (read-line)
)