;Compilation notes:
; The vsClojure installer should have stored the core framework directory in the VSCLOJURE_RUNTIMES_DIR environment variable pointing to C:\Users\[username]\AppData\Local\Microsoft\VisualStudio\[version]\Extensions\[randomCharacters]\Runtimes\

(System.Reflection.Assembly/LoadWithPartialName "System.Windows.Forms")

(ns program
  (:import [System.Windows.Forms Application])
  (:gen-class)
  (:require Form1))

(defn -main [& args]
  (Application/EnableVisualStyles)
  (Application/SetCompatibleTextRenderingDefault false)
  (Application/Run (Form1/createForm1)))