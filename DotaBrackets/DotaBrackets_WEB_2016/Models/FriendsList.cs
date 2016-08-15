using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DotaBrackets_WEB_2016.Models
{
    public class FriendsList
    {
        public int friendsListID { get; set; }
        public int gamerID { get; set; }
        public List<Gamer> friendsList { get; set; }
    }
}