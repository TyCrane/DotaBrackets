using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DotaBrackets_WEB_2016.Models
{
    public class Gamer
    {
        public int gamerID { get; set; }
        public int traitsID { get; set; }
        public int preferencesID { get; set; }
        public string userName { get; set; }
        public string access { get; set; }
        public long steamID { get; set; }
        public int dota2ID { get; set; }
        public bool isSearching { get; set; }
        public string avatar { get; set; }
        public FriendsList friendsList { get; set; }
        public Preferences preferences { get; set; }
        public Traits traits { get; set; }
    }
}