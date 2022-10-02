using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WpfTests.DependencyInjection
{
    public interface ISettingsService
    {
        void ReadSettings(string path);
        string GetValue();
    }

    public class SettingsService : ISettingsService
    {
        private string _value;

        public void ReadSettings(string path)
        {
            if (File.Exists(path))
                _value = File.ReadAllText(path, Encoding.UTF8);
            else
                _value = "file not found !";
        }

        public string GetValue()
        {
            return _value;
        }
    }
}
