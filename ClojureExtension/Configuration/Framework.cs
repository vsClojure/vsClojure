using System;

namespace Microsoft.ClojureExtension.Configuration
{
    [Serializable]
    public class Framework
    {
        private readonly string _name;
        private readonly string _location;

        public Framework(string name, string location)
        {
            _name = name;
            _location = location;
        }

        public string Location
        {
            get { return _location; }
        }

        public string Name
        {
            get { return _name; }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}