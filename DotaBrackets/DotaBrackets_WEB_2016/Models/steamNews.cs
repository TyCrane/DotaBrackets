using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DotaBrackets_WEB_2016.Models
{  
        public class Newsitem
        {
            public string gid { get; set; }
            public string title { get; set; }
            public string url { get; set; }
            public bool is_external_url { get; set; }
            public string author { get; set; }
            public string contents { get; set; }
            public string feedlabel { get; set; }
            public int date { get; set; }
            public string feedname { get; set; }
        }

        public class Appnews
        {
            public int appid { get; set; }
            public List<Newsitem> newsitems { get; set; }
        }

        public class RootObject
        {
            public Appnews appnews { get; set; }
        }
}