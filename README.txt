Project:

	vsClojure

Description:

	A Visual Studio 2010, 2012, & 2013 Extension for ClojureCLR & ClojureScript.

	Runtimes 1.5.0 available

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
	10.) Simple auto-completion (may require disabling other intellisense extensions such as Resharper)
	11.) Auto-compile .cljs ClojureScript on save to a code-behind JavaScript file

Notes:
	1.) Compile errors show in the Output window, not the error list window.
	2.) The solution configuration manager defaults to NOT build clojure projects. To correct this, right-click on the solution, choose Configuration Manager, and check the "build" box next to your clojure projects.
	3.) The vsClojure installer should have stored the core framework directory in the CLOJURE_LOAD_PATH environment variable pointing to C:\Users\[username]\AppData\Local\Microsoft\VisualStudio\11.0\Extensions\[randomCharacters]\Runtimes\1.5.0
	4.) There is a known bug that you cannot open a .sln file containing a clojure project directly from windows. You must open visual studio first and then open your solution file.
	5.) Autocomplete may not work if another extension (such as Resharper) has taken over intellisense. You may need to disable it while developing in Clojure.

Source:

	https://github.com/vsClojure/vsClojure

Discussion:

	http://gplus.to/clojureclr
	
	#vsclojure on irc.freenode.net

Installing:

	1.) Install vsClojure from the Visual Studio Gallery using the extension manager in Visual Studio.
	2.) For ClojureScript, install the latest JDK from Oracle.com and add the folder containing Java.exe to your path environment variable (usually C:\Program Files (x86)\Java\jre7\bin)

Building:

	1.) Install the Visual Studio 2010, 2012, & 2013 SDK.
	2.) Clone the vsClojure repository.
	3.) Open the vsClojure solution.
	4.) Build

Note: There is a bug in the bindingRedirect in devenv.exe.config included in vs2013, breaking all extensions which use Microsoft.VisualStudio.Package.LanguageService.10.0.dll
This causes it to attempt to load Microsoft.VisualStudio.Package.LanguageService.10.0, Version=12.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a' (version 10 & 12 in same strong name), which does not exist.
The .vsix includes an identical copy of the original .dll which has been resigned with a new strong name. The public key token does not match the original because the dll was not signed by Microsoft.
Please vote to correct this vs2013 bug at http://connect.microsoft.com/VisualStudio/feedback/details/794961/previous-version-assemblies-cannot-load-in-visual-studio-2013-preview

License:

	See LICENSE.txt.

Attribution:

	See ATTRIBUTION.txt.