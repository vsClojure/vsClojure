;Compilation notes:
; The solution configuration manager defaults to NOT build clojure projects. To correct this, right-click on the solution, choose Configuration Manager, and check the "build" box next to your clojure projects.
; Because this uses gen-class, a re-compilation is required with each class signature change.
; Add a reference from your MVC web project to this project (as a project reference, not to a specific dll)
; The clojure compiler will generate many dlls [namespace].clj.dll & [classname].dll
; All the dlls & clj files should be automatically copied to the bin folder of the MVC web project which references this clojure project on run/build of the MVC web application project.
; The vsClojure installer should have stored the core framework directory in the CLOJURE_LOAD_PATH environment variable pointing to C:\Users\[username]\AppData\Local\Microsoft\VisualStudio\11.0\Extensions\[randomCharacters]\Runtimes\1.5.0

(System.Reflection.Assembly/LoadWithPartialName "System.Web.Mvc")

;class definition
(ns %namespace%
  (:require [clojure.core])
  (:gen-class
    :main false
    :name MvcApplication1.Controllers.%namespace%
    :extends System.Web.Mvc.Controller
    :methods [
              [Index [] System.Web.Mvc.ActionResult]
              ]
    :constructors {[] []}
  )
)

;implementation

 (defn -Index [this] (.View this))