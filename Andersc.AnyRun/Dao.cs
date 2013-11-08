using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Andersc.AnyRun.Config;
using Andersc.CodeLib.Common;
using Andersc.CodeLib.Common.Data;

namespace Andersc.AnyRun
{
    public class Dao
    {
        public class Tables
        {
            public static readonly string RunnerCommands = "runner_cmds";
            public static readonly string RunnerWebby = "runner_webby";
        }

        public static List<Cmd> GetAllCmds()
        {
            var ds = SQLiteHelper.DatasetOfTable(ConfigManager.ConnString, Tables.RunnerCommands);
            var cmds = new List<Cmd>();

            if (ds.IsNotNull() && ds.Tables.Count > 0)
            {
                var cmdRows = ds.Tables[0].Rows;
                foreach (DataRow cmd in cmdRows)
                {
                    var cmdItem = new Cmd() { Alias = cmd["bias"].ToString(), CmdPath = cmd["cmd_path"].ToString() };
                    cmds.Add(cmdItem);
                } 
            }

            return cmds;
        }

        public static List<Cmd> GetDefaultShortcuts()
        {
            var userStartMenu = Environment.GetFolderPath(Environment.SpecialFolder.StartMenu);
            var cmnStartMenu = Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu);
            var quickLaunch = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                                           @"Microsoft\Internet Explorer\Quick Launch");

            var folders = new[] { userStartMenu, cmnStartMenu, quickLaunch };

            return GetShortcutCmds(folders);
        }

        public static List<Cmd> GetRecentShortcuts()
        {
            var recentFiles = Environment.GetFolderPath(Environment.SpecialFolder.Recent);

            var folders = new[] { recentFiles };
            return GetShortcutCmds(folders);
        }

        private static List<Cmd> GetShortcutCmds(IEnumerable<string> folders)
        {
            var cmds = new List<Cmd>();
            foreach (var folder in folders)
            {
                var shortcuts = Directory.GetFiles(folder, "*.lnk", SearchOption.AllDirectories);
                foreach (var shortcut in shortcuts.Where(sc => !sc.Contains("uninstall", StringComparison.OrdinalIgnoreCase)))
                {
                    try
                    {
                        var cmdFile = API.GetLinkPath(shortcut);
                        if (cmds.All(cmd => cmd.CmdPath != cmdFile))
                        {
                            cmds.Add(new Cmd()
                            {
                                Alias = Path.GetFileNameWithoutExtension(shortcut),
                                CmdPath = cmdFile
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Read shortcut error: {0} - {1}".FormatWith(shortcut, ex.Message));
                    }
                }
            }

            return cmds;
        }

        public static List<Webby> GetAllWebbies()
        {
            var ds = SQLiteHelper.DatasetOfTable(ConfigManager.ConnString, Tables.RunnerWebby);
            var webbies = new List<Webby>();
            var webRows = ds.Tables[0].Rows;
            foreach (DataRow row in webRows)
            {
                var cmdItem = new Webby()
                {
                    Id = Convert.ToInt32(row["id"]),
                    Name = row["name"].ToString(),
                    UrlFormat = row["url_format"].ToString(),
                    CreatedDate = DateTime.Parse(row["created_date"] as string),
                };
                webbies.Add(cmdItem);
            }

            return webbies;
        }
    }
}
