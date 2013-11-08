using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

using Andersc.CodeLib.MyTodo.Domain;

namespace Andersc.CodeLib.MyTodo.Dao
{
    public class PriorityManager
    {
        private static List<Priority> priorities = null;

        private static readonly int HighestID = 1;
        private static readonly int HighID = 2;
        private static readonly int MediumID = 3;
        private static readonly int LowID = 4;
        private static readonly int LowestID = 5;

        static PriorityManager()
        {
            priorities = new List<Priority>();

            XDocument doc = new XDocument(ConfigManager.PriorityConfigFilePath);
            foreach (var priorElem in doc.Elements("priorities").Elements("priority"))
            {
                priorities.Add(new Priority() { 
                    ID = (int)priorElem.Attribute("id"), 
                    Description = priorElem.Element("desc").Value
                });
            }
        }

        public static Priority Highest
        {
            get { return priorities.Find(p => p.ID == HighestID); }
        }

        public static Priority High
        {
            get { return priorities.Find(p => p.ID == HighID); }
        }

        public static Priority Medium
        {
            get { return priorities.Find(p => p.ID == MediumID); }
        }

        public static Priority Low
        {
            get { return priorities.Find(p => p.ID == LowID); }
        }

        public static Priority Lowest
        {
            get { return priorities.Find(p => p.ID == LowestID); }
        }
    }
}
