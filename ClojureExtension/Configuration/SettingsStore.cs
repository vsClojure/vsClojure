using System;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.VisualStudio.Shell.Interop;

namespace Microsoft.ClojureExtension.Configuration
{
    public class SettingsStore
    {
        private readonly string _collectionName;
        private readonly IVsWritableSettingsStore _writableSettingsStore;

        public SettingsStore(string collectionName, IVsWritableSettingsStore writableSettingsStore)
        {
            _collectionName = collectionName;
            _writableSettingsStore = writableSettingsStore;
        }

        public void Save(string name, object data)
        {
            int exists = 0;
            _writableSettingsStore.CollectionExists(_collectionName, out exists);

            if (exists != 1) _writableSettingsStore.CreateCollection(_collectionName);

            BinaryFormatter binaryFormatter = new BinaryFormatter();
            MemoryStream memoryStream = new MemoryStream();
            binaryFormatter.Serialize(memoryStream, data);
            _writableSettingsStore.SetBinary(_collectionName, name, (uint) memoryStream.Length, memoryStream.ToArray());
        }

        public bool Exists(string name)
        {
            int exists;
            _writableSettingsStore.CollectionExists(_collectionName, out exists);
            if (exists == 0) return false;
            _writableSettingsStore.PropertyExists(_collectionName, name, out exists);
            return exists == 1;
        }

        public T Get<T>(string name)
        {
            int exists;
            _writableSettingsStore.CollectionExists(_collectionName, out exists);
            if (exists != 1) throw new Exception("Property " + name + " does not exist.");

            uint[] actualNumberOfBytes = new uint[1];
            _writableSettingsStore.GetBinary(_collectionName, name, 0, null, actualNumberOfBytes);
            byte[] data = new byte[actualNumberOfBytes[0]];
            _writableSettingsStore.GetBinary(_collectionName, name, (uint) data.Length, data, actualNumberOfBytes);

            BinaryFormatter binaryFormatter = new BinaryFormatter();
            binaryFormatter.AssemblyFormat = FormatterAssemblyStyle.Simple;
            MemoryStream memoryStream = new MemoryStream(data);
            return (T) binaryFormatter.Deserialize(memoryStream);
        }
    }
}