using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Andersc.CodeLib.DoubanDownloader
{
    internal class ConfigManager : IConfigManager
    {
        internal class ConfigItems
        {
            public string LastSelectedPath { get; set; }
            public DateTime LastAccessTime { get; set; }
        }

        private static readonly char ConfigFileSeparator = '|';
        private static readonly string DefaultDataFilePath = "config.txt";

        private ConfigItems items = null;

        #region Singleton

        private static readonly object padlock = new object();
        private static ConfigManager _instance;
        public static ConfigManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (padlock)
                    {
                        if (_instance == null)
                        {
                            _instance = new ConfigManager();
                        }
                    }
                }
                return _instance;
            }
        } 

        #endregion

        #region IConfigManager members

        public string LastSelectedPath
        {
            get
            {
                if (items == null)
                {
                    LoadConfigInfo();
                }

                return items.LastSelectedPath;
            }
            set
            {
                if (items == null)
                {
                    LoadConfigInfo();
                }
                items.LastSelectedPath = value;
            }
        }

        public string AlbumDownloadTips
        {
            get
            {
                return "Input album ID (an integer number, like 22694572)";
            }
        }

        public void Refresh()
        {
            LoadConfigInfo();
        }

        public void Save()
        {
            SaveConfig(items);
        } 

        #endregion

        #region Private members

        private string DataFilePath
        {
            get
            {
                string relativePath = DefaultDataFilePath;
                if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["dataFilePath"]))
                {
                    relativePath = ConfigurationManager.AppSettings["dataFilePath"];
                }

                return Path.Combine(Application.StartupPath, relativePath);
            }
        }

        private string DefaultSelectedPath
        {
            get { return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); }
        }

        private void SaveConfig(ConfigItems items)
        {
            if (HasLoadedData())
            {
                // TODO: Refactor.
                using (StreamWriter sw = File.CreateText(DataFilePath))
                {
                    sw.WriteLine(string.Format("lastSelectedPath{0}{1}", ConfigFileSeparator, items.LastSelectedPath));
                    sw.WriteLine(string.Format("lastAccessTime{0}{1}", ConfigFileSeparator, DateTime.Now));
                }
            }
        }

        private bool HasLoadedData()
        {
            return (items != null);
        }

        private void LoadConfigInfo()
        {
            if (!File.Exists(DataFilePath))
            {
                CreateDefulatConfigFile();
            }

            items = new ConfigItems();
            using (StreamReader sr = File.OpenText(DataFilePath))
            {
                string selectedPathLine = sr.ReadLine();
                items.LastSelectedPath = selectedPathLine.Split(ConfigFileSeparator)[1];
                string accessTimeLine = sr.ReadLine();
                items.LastAccessTime = Convert.ToDateTime(accessTimeLine.Split(ConfigFileSeparator)[1]);
            }
        }

        private void CreateDefulatConfigFile()
        {
            // TODO: Refactor.
            using (StreamWriter sw = File.CreateText(DataFilePath))
            {
                sw.WriteLine(string.Format("lastSelectedPath{0}{1}", ConfigFileSeparator, DefaultSelectedPath));
                sw.WriteLine(string.Format("lastAccessTime{0}{1}", ConfigFileSeparator, DateTime.Now));
            }
        } 

        #endregion
    }
}
