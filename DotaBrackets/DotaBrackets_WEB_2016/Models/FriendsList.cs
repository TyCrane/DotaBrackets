using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DotaBrackets_WEB_2016.Models
{
    public class FriendsList
    {
        public FriendsList()
        {
            this.friendsList = new List<FriendID>();
        }

        public List<FriendID> friendsList { get; set; }
    }

    public class FriendID
    {
        public int friendID { get; set; }
        public string userName { get; set; }
        public long dota2ID { get; set; }
    }
}