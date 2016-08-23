using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DotaBrackets_WEB_2016.Models
{
    public class Group
    {
        public Group()
        {
            this.members = new List<Gamer>();
        }

        public List<Gamer> members { get; set; }
        public string groupName { get; set; }
    }
}