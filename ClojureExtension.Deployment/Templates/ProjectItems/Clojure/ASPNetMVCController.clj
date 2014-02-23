;Compilation notes:
; The solution configuration manager defaults to NOT build clojure projects. To correct this, right-click on the solution, choose Configuration Manager, and check the "build" box next to your clojure projects.
; Because this uses gen-class, a re-compilation is required with each class signature change.
; Add a reference from your MVC web project to this project (as a project reference, not to a specific dll)
; The vsClojure installer should have stored the core framework directory in the VSCLOJURE_RUNTIMES_DIR environment variable pointing to C:\Users\[username]\AppData\Local\Microsoft\VisualStudio\[version]\Extensions\[randomCharacters]\Runtimes\

(System.Reflection.Assembly/LoadWithPartialName "System.Web.Mvc")

;class definition
(ns %namespace%
  (:require [clojure.core])
  (:gen-class
    :main false
    :name MvcApplication1.Controllers.%namespace%
    :extends System.Web.Mvc.Controller
    :methods [[Index [] System.Web.Mvc.ActionResult]]
    :constructors {[] []}))

;implementation

(defn -Index [this] (.View this))