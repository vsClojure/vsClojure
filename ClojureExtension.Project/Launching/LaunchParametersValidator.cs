// MIT License Copyright 2010-2013 jmis
// See LICENSE.txt or http://opensource.org/licenses/MIT
// See AUTHORS.txt for a complete list of all contributors

using System;

namespace ClojureExtension.Project.Launching
{
	public class LaunchParametersValidator
	{
		public void Validate(LaunchParameters launchParameters)
		{
			if (string.IsNullOrEmpty(launchParameters.FrameworkPath))
				throw new Exception("No clojure framework path defined in project properties.");

			if (string.IsNullOrEmpty(launchParameters.StartupFile))
				throw new Exception("No startup file defined in project properties.");

			if (launchParameters.StartupFileType == StartupFileType.Unknown)
				throw new Exception("Cannot start file " + launchParameters.StartupFile);
		}
	}
}