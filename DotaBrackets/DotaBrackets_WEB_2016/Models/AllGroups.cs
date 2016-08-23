using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotaBrackets_WEB_2016.Models;

namespace DotaBrackets_WEB_2016.Models
{
    public class AllGroups
    {
        public AllGroups()
        {
            this.allGroups = new List<Group>();
        }

        public List<Group> allGroups { get; set; }
    }
}