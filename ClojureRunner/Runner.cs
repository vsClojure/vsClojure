using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using clojure.lang;

namespace ClojureRunner
{
    public class Runner
    {
        private static string _frameworkPath = "";

        public static int Run(string[] args)
        {
            AppDomain.CurrentDomain.AssemblyResolve += currentDomainAssemblyResolve;

            _frameworkPath = args[0];
            string applicationPath = args[1];
            string startupNamespace = args[2];
            string startupFunction = args[3];

            Environment.SetEnvironmentVariable("clojure.load.path", _frameworkPath + ";" + applicationPath);
            Environment.SetEnvironmentVariable("PATH", Environment.GetEnvironmentVariable("PATH") + ";" + _frameworkPath);

            executeClojureFunction(startupNamespace, startupFunction, args);
            return 0;
        }

        private static void executeClojureFunction(string startupNamespace, string startupFunction, string[] args)
        {
            Var require = RT.var("clojure.core", "require");
            require.invoke(Symbol.intern(startupNamespace));
            Var var = RT.var(startupNamespace, startupFunction);
            var.applyTo(RT.list(args.Skip(4)));
        }

        private static Assembly currentDomainAssemblyResolve(object sender, ResolveEventArgs args)
        {
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
                if (assembly.FullName == args.Name)
                    return assembly;

            string assemblyName = args.Name.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[0];
            List<string> frameworkFiles = new List<string>(Directory.GetFiles(_frameworkPath));
            string targetAssembly = frameworkFiles.Find(f => Path.GetFileName(f).ToLower() == assemblyName.ToLower() + ".dll");
            if (targetAssembly == null) return null;
            return Assembly.LoadFrom(targetAssembly);
        }
    }
}