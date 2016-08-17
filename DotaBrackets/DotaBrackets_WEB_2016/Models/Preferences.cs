using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DotaBrackets_WEB_2016.Models
{
    public class Preferences
    {
        public int preferencesID { get; set; }
        public int mmr { get; set; }
        public int hasMic { get; set; }
        public int language { get; set; }
    }
}