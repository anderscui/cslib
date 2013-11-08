using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;

namespace Andersc.CodeLib.Common
{
    public class RegistryHelper
    {
        private static RegistryKey defaultKey = Registry.CurrentUser;

        public static string ReadValue(string subKeyName, string name)
        {
            return ReadValue(defaultKey, subKeyName, name);
        }

        public static string ReadValue(RegistryKey rootKey, string subKeyName, string name)
        {
            if (string.IsNullOrEmpty(subKeyName))
            {
                throw new ArgumentException("Invalid argument 'subKeyName'");
            }

            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Invalid argument 'name'");
            }

            RegistryKey subKey = null;
            string result = string.Empty;

            subKey = rootKey.OpenSubKey(subKeyName);

            if (subKey != null)
            {
                result = subKey.GetValue(name) as string;
            }

            return result;
        }

        public static Dictionary<string, string> ReadValues(string subKeyName)
        {
            return ReadValues(defaultKey, subKeyName);
        }

        public static Dictionary<string, string> ReadValues(RegistryKey rootKey, string subKeyName)
        {
            if (string.IsNullOrEmpty(subKeyName))
            {
                throw new ArgumentException("Invalid argument 'subKeyName'");
            }

            RegistryKey subKey = null;
            Dictionary<string, string> result = new Dictionary<string, string>();

            subKey = rootKey.OpenSubKey(subKeyName);

            if (subKey != null)
            {
                string[] names = subKey.GetValueNames();
                foreach (string name in names)
                {
                    result.Add(name, (subKey.GetValue(name) as string));
                }
            }

            return result;
        }

        public static void WriteValue(string subKeyName, string name, object value)
        {
            WriteValue(defaultKey, subKeyName, name, value);
        }

        public static void WriteValue(RegistryKey rootKey, string subKeyName, string name, object value)
        {
            if (string.IsNullOrEmpty(subKeyName))
            {
                throw new ArgumentException("Invalid argument 'subKeyName'");
            }

            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Invalid argument 'name'");
            }

            RegistryKey subKey = rootKey.OpenSubKey(subKeyName, true);

            if (subKey.IsNull())
            {
                CreateSubKey(rootKey, subKeyName);
                subKey = rootKey.OpenSubKey(subKeyName, true);
            }
            subKey.SetValue(name, value);
        }

        // TODO: How to do unit test?
        public static void CreateSubKey(string subKeyName)
        {
            CreateSubKey(defaultKey, subKeyName);
        }

        // TODO: How to do unit test?
        public static void CreateSubKey(RegistryKey rootKey, string subKeyName)
        {
            if (string.IsNullOrEmpty(subKeyName))
            {
                throw new ArgumentException("Invalid argument 'subKeyName'");
            }

            rootKey.CreateSubKey(subKeyName);
        }

        // TODO: How to do unit test?
        public static void DeleteSubKey(string subKeyName)
        {
            DeleteSubKey(defaultKey, subKeyName);
        }

        // TODO: How to do unit test?
        public static void DeleteSubKey(RegistryKey rootKey, string subKeyName)
        {
            if (string.IsNullOrEmpty(subKeyName))
            {
                throw new ArgumentException("Invalid argument 'subKeyName'");
            }

            rootKey.DeleteSubKey(subKeyName);
        }

        public static void DeleteValues(string subKeyName)
        {
            DeleteValues(defaultKey, subKeyName);
        }

        public static void DeleteValues(RegistryKey rootKey, string subKeyName)
        {
            if (string.IsNullOrEmpty(subKeyName))
            {
                throw new ArgumentException("Invalid argument 'subKeyName'");
            }

            RegistryKey subKey = rootKey.OpenSubKey(subKeyName);

            if (subKey != null)
            {
                string[] names = subKey.GetValueNames();
                foreach (string name in names)
                {
                    subKey.DeleteValue(name);
                }
            }
        }
    }
}
