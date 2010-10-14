Project:

	vsClojure

Description:

	A Visual Studio 2010 Extension for ClojureCLR.

Status:

	In development - Not ready for use.

Building:

	1.) Install the Visual Studio 2010 SDK.
	2.) Clone the vsClojure repository.
	3.) Download the Managed Package Framework (http://mpfproj.codeplex.com/).
	4.) Create a folder named "ManagedPackageFramework" in the solution directory.
	5.) Copy the ManagedPackageFramework source to the new folder. 
	6.) Open the vsClojure solution.
	7.) Download the Antlr 3 runtime (http://www.antlr.org/).
	8.) Reference the Antlr 3 runtime assemblies from the ClojureExtension project.

Running:

	1.) Download or build ClojureCLR (http://github.com/richhickey/clojure-clr).
	2.) Build vsClojure.
	3.) Run vsClojure from Visual Studio.
	4.) Add a new Clojure project.
	5.) Set the path to ClojureCLR in the project properties.

Current Features:

	1.) Editor
		* Syntax highlighting
		* Auto-indentation
		* Brace matching

	2.) Project
		* Clojure project type

	2.) Repl
		* Support for multiple open repls.
		* Loading files from the Solution Explorer into the repl.
		* Loading a project from the Solution Explorer into the repl.

License:

	See LICENSE.txt.

Attribution:

	See ATTRIBUTION.txt.