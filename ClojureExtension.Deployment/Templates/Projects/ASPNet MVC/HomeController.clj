;Compilation notes:
; The solution configuration manager defaults to NOT build clojure projects. To correct this, right-click on the solution, choose Configuration Manager, and check the "build" box next to your clojure projects.
; Because this uses gen-class, a re-compilation is required with each class signature change.

;Setup notes: 
; The clojure MVC Controller Project will generate two files [namespace].clj.dll and [classname].dll
; Add the compiled dlls as references to your MVC project (from debug/release folder, not as a project reference)

(System.Reflection.Assembly/LoadWithPartialName "System.Web.Mvc")

;class definition
(ns HomeController
  (:require [clojure.core])
  (:gen-class
    :main false
    :name MvcApplication1.Controllers.HomeController
    :extends System.Web.Mvc.Controller
    :methods [
              [Index [] System.Web.Mvc.ActionResult]
              ]
    :constructors {[] []}
  )
)

;implementation

 (defn -Index [this] (.View this))