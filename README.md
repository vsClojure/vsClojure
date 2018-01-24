#!!!HELP!!! THIS PROJECT NEEDS DEVELOPERS !!!HELP!!! 

#vsClojure

##Description

A Visual Studio 2010, 2012, & 2013 Extension for ClojureCLR & ClojureScript.

##Features

- Clojure project type
- Building
- Running
- Syntax highlighting
- Brace matching
- Auto-indentation
- File formatting (Editor menu command)
- Integrated REPL
		- Start project REPL (Project menu command)
		- Load all project files into REPL (Project menu command)
		- Load active file into REPL (Editor menu command)
		- Switch to active file's namespace (Editor menu command)
		- History (UP ARROW or DOWN ARROW)
- Block commenting
- Simple auto-completion
- Auto-compile .cljs ClojureScript on save to a code-behind JavaScript file

##Notes

- Compile errors show in the Output window, not the error list window.
- The solution configuration manager defaults to NOT build clojure projects. To correct this, right-click on the solution, choose Configuration Manager, and check the "build" box next to your clojure projects.
- The vsClojure installer should have stored the core framework directory in the VSCLOJURE_RUNTIMES_DIR environment variable pointing to C:\Users\[username]\AppData\Local\Microsoft\VisualStudio\[version]\Extensions\[randomCharacters]\Runtimes\

##Source

https://github.com/vsClojure/vsClojure

##Discussion

\#vsclojure on irc.freenode.net

##Installing

- Install vsClojure from the Visual Studio Gallery using the extension manager in Visual Studio or from http://visualstudiogallery.msdn.microsoft.com/0e53eaf6-b031-4d12-84c1-9163db0b757e
- For ClojureScript, install the latest JDK from Oracle.com and add the folder containing Java.exe to your path environment variable (usually C:\Program Files (x86)\Java\jre7\bin)

##Building

- Install the Visual Studio 2010, 2012, & 2013 SDK.
- Clone the vsClojure repository.
- Open the vsClojure solution.
- Build solution
- Install the compiled visual studio extension located at \ClojureExtension.Deployment\bin\Debug\ClojureExtension.Deployment.vsix

##License

http://opensource.org/licenses/MIT

The MIT License (MIT)

Copyright 2010 jmis

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.

##Author

Jon Mis - https://github.com/jmis  

##Contributors

Devin Garner - https://github.com/speige  
Frank Hale - https://github.com/frankhale

NOTE: Submit a pull request if you have helped and are not listed

##Attribution

###Project Type

*Adapted the CustomProject example from the Managed Package Framework for Clojure.*

- MPF Team (http://mpfproj.codeplex.com/team/view)  
- Managed Package Framework (http://mpfproj.codeplex.com/)  
- Ms-PL (http://msdn.microsoft.com/en-us/library/aa721778.aspx)  

###Clojure Images
- Clojure Project (http://clojure.org/)
- Eclipse Public License v1.0 (http://opensource.org/licenses/eclipse-1.0.php)

###Clojure CLR
- clojure-clr (https://github.com/clojure/clojure-clr)
- Eclipse Public License v1.0 (http://opensource.org/licenses/eclipse-1.0.php)
