using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using DotaBrackets_WEB_2016.Models;
using Microsoft.AspNet.SignalR.Hubs;
using Newtonsoft.Json;

namespace DotaBrackets_WEB_2016.Classes
{
    public class SignalRChatHub : Hub
    {
        public static AllGroups allGroups = new AllGroups();
        public static Group group = new Group();
        public int i;
        public int n;


        public void BroadCastMessage(string msgFrom, string msg)
        {
            Clients.All.recieveMessage(msgFrom, msg);
        }

        //allows messages within a group
        public void BroadCastGroupMessage(string msgFrom, string msg, string groupName)
        {
            Clients.Group(groupName).addChatMessage(msgFrom, msg);
        }


        public void GetPartyMembers(string userName, string steamID, string groupName)
        {
            Clients.Group(groupName).recievePartyMember(userName, steamID);
        }

        public void GetGroupName(string groupName)
        {
            Clients.Caller.addContosoChatMessageToPage(groupName);
        }


        public void JoinGroup(string data)
        {
            Gamer thisGamer = new Gamer();
            thisGamer = JsonConvert.DeserializeObject<Gamer>(data);
            string groupName;


            for (i = 0; i < allGroups.allGroups.Count; i++) ;

            if(allGroups.allGroups.Count == 0)
            {
                allGroups.allGroups.Add(new Group());
                allGroups.allGroups[0].members.Add(thisGamer);
                Groups.Add(Context.ConnectionId, thisGamer.gamerID.ToString());
                allGroups.allGroups[0].groupName = thisGamer.gamerID.ToString();
                groupName = thisGamer.gamerID.ToString();
                GetGroupName(groupName);
                GetPartyMembers(thisGamer.userName, thisGamer.steamID.ToString(), groupName);
            }
            else
            {
                foreach (Group group in allGroups.allGroups)
                {
                    if (group.members.Count != 0)
                    {
                        foreach (Gamer excGamer in group.members)
                        {
                            while (thisGamer.isSearching == true)
                            {
                                if (CheckMatch(thisGamer, excGamer) == true && group.members.Count < 5)
                                {
                                    Groups.Add(Context.ConnectionId, group.members[0].gamerID.ToString());
                                    groupName = group.members[0].gamerID.ToString();
                                    GetGroupName(groupName);
                                    GetPartyMembers(thisGamer.userName, thisGamer.steamID.ToString(), groupName);
                                }
                                else
                                {
                                    allGroups.allGroups.Add(new Group());
                                    allGroups.allGroups[i + 1].members.Add(thisGamer);
                                    Groups.Add(Context.ConnectionId, thisGamer.gamerID.ToString());
                                    groupName = thisGamer.gamerID.ToString();
                                    GetGroupName(groupName);
                                    GetPartyMembers(thisGamer.userName, thisGamer.steamID.ToString(), groupName);
                                }
                            }
                        }
                    }
                }
            }
        }






        public bool CheckMatch(Gamer incGamer, Gamer excGamer)
        {
            bool match;

            if ((excGamer.preferences.hasMic == incGamer.traits.hasMic) || excGamer.preferences.hasMic == 1)
            {
                match = true;
            }
            else
            {
                match = false;
            }

            if ((excGamer.preferences.language == incGamer.traits.language) || excGamer.preferences.hasMic == 1)
            {
                match = true;
            }
            else
            {
                match = false;
            }

            if ((excGamer.preferences.mmr == incGamer.traits.mmr) || excGamer.preferences.mmr == 1)
            {
                match = true;
            }
            else
            {
                match = false;
            }

            if ((excGamer.traits.server == incGamer.traits.server) || excGamer.traits.server == 1)
            {
                match = true;
            }
            else
            {
                match = false;
            }
        
            return match;
        }


    }
}