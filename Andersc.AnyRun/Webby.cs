using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Andersc.AnyRun
{
    public class Webby
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UrlFormat { get; set; }
        public DateTime CreatedDate { get; set; }
        public string[] Params { get; set; }
    }
}
