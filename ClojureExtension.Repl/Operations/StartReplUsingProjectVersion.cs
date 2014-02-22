// MIT License Copyright 2010-2013 jmis
// See LICENSE.txt or http://opensource.org/licenses/MIT
// See AUTHORS.txt for a complete list of all contributors

using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Controls;
using ClojureExtension.Utilities;

namespace ClojureExtension.Repl.Operations
{
	public class StartReplUsingProjectVersion
	{
		private readonly ReplFactory _replFactory;
		private readonly Func<Process> _replProcessProvider;

		public StartReplUsingProjectVersion(
			ReplFactory replFactory,
			Func<Process> replProcessProvider)
		{
			_replProcessProvider = replProcessProvider;
			_replFactory = replFactory;
		}

		public void Execute()
		{
      _replFactory.CreateRepl(_replProcessProvider());
		}
	}
}