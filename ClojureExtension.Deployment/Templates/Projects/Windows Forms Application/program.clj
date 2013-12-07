(System.Reflection.Assembly/LoadWithPartialName "System.Windows.Forms")

(ns program
  (:import [System.Windows.Forms Application])
  (:gen-class)
  (:require Form1)
)

(defn -main [& args]
			(Application/EnableVisualStyles)
			(Application/SetCompatibleTextRenderingDefault false)
			(Application/Run (Form1/createForm1))
)