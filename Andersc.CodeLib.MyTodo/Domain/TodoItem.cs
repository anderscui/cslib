using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Andersc.CodeLib.Common;

namespace Andersc.CodeLib.MyTodo.Domain
{
    public class TodoItem
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public short Score { get; set; }
        public string Comment { get; set; }
        public DateTime? StartOn { get; set; }
        public DateTime? EndOn { get; set; }

        // TODO: Parse tag list;
        public List<Tag> Tags { get; set; }

        public Status Status
        {
            get
            {
                if (StartOn.DoesNotHaveValue())
                {
                    return Domain.Status.TODO;
                }

                if (EndOn.DoesNotHaveValue())
                {
                    return Domain.Status.Doing;
                }

                return Domain.Status.Done;
            }
        }

        // TODO: 
        public Priority Priority
        {
            get { return null; }
        }
    }
}
