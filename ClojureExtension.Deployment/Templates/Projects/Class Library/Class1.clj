;;Compilation notes:
; The solution configuration manager defaults to NOT build clojure projects. To correct this, right-click on the solution, choose Configuration Manager, and check the "build" box next to your clojure projects.
; Because this uses gen-class, a re-compilation is required with each class signature change.
; Add a reference from your other .net projects to this project (as a project reference, not to a specific dll) to allow access to these classes
; The vsClojure installer should have stored the core framework directory in the VSCLOJURE_RUNTIMES_DIR environment variable pointing to C:\Users\[username]\AppData\Local\Microsoft\VisualStudio\[version]\Extensions\[randomCharacters]\Runtimes\

;class definition
(ns Class1
  (:require [clojure.core])
  (:gen-class
    :main false
    :name ClassLibrary.Class1
    :methods [[MyMethod [] String]]
    :constructors {[] []}))

;implementation

(defn -MyMethod [this] "MyResult")