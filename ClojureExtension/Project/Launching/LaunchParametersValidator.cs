using System;

namespace Microsoft.ClojureExtension.Project.Launching
{
	public class LaunchParametersValidator
	{
		public void Validate(LaunchParameters launchParameters)
		{
			if (string.IsNullOrEmpty(launchParameters.FrameworkPath))
				throw new Exception("No clojure framework path defined in project properties.");

			if (string.IsNullOrEmpty(launchParameters.TargetFile))
				throw new Exception("No file to load defined in project properties.");
		}
	}
}