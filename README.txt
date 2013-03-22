Project:

	vsClojure

Description:

	A Visual Studio 2012 Extension for ClojureCLR & ClojureScript.

	Runtimes 1.5.0 (stable beta) available

Features:

	1.) Clojure project type
	2.) Building
	3.) Running
	4.) Syntax highlighting
	5.) Brace matching
	6.) Auto-indentation
	7.) File formatting (Editor menu command)
	8.) Integrated REPL
		* Start project REPL (Project menu command)
		* Load all project files into REPL (Project menu command)
		* Load active file into REPL (Editor menu command)
		* Switch to active file's namespace (Editor menu command)
		* History (UP ARROW or DOWN ARROW)
	9.) Block commenting
	10.) Simple auto-completion
	11.) Auto-compile .cljs ClojureScript on save to a code-behind JavaScript file

Notes:
	1.) Compile errors show in the Output window, not the error list window.
	2.) The solution configuration manager defaults to NOT build clojure projects. To correct this, right-click on the solution, choose Configuration Manager, and check the "build" box next to your clojure projects.
	3.) The vsClojure installer should have stored the core framework directory in the CLOJURE_LOAD_PATH environment variable pointing to C:\Users\[username]\AppData\Local\Microsoft\VisualStudio\11.0\Extensions\[randomCharacters]\Runtimes\1.5.0
	4.) There is a known bug that you cannot open a .sln file containing a clojure project directly from windows. You must open visual studio first and then open your solution file.

Source:

	-original-
	https://github.com/vsClojure/vsClojure
	-fork-
	https://github.com/speige/vsClojure

Discussion:

	http://gplus.to/clojureclr

Installing:

	1.) Install vsClojure from the Visual Studio Gallery using the extension manager in Visual Studio.
	2.) For ClojureScript, install the latest JDK from Oracle.com and add the folder containing Java.exe to your path environment variable (usually C:\Program Files (x86)\Java\jre7\bin)

Building:

	1.) Install the Visual Studio 2012 SDK.
	2.) Clone the vsClojure repository.
	3.) Open the vsClojure solution.
	4.) Build

License:

	See LICENSE.txt.

Attribution:

	See ATTRIBUTION.txt.