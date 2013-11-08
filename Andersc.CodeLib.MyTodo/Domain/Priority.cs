using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Andersc.CodeLib.MyTodo.Domain
{
    /// <summary>
    /// Priority of TODO items.
    /// </summary>
    public class Priority
    {
        /// <summary>
        /// Medium priority.
        /// </summary>
        private static readonly int DefaultID = 3;

        private static List<Priority> AllPriorities = null;

        // TODO: Impl this readonly type.
        public int ID { get; set; }
        public string Description { get; set; }

        public Priority() : this(DefaultID) { }

        public Priority(int id)
        {
            ID = id;
        }        
    }
}
