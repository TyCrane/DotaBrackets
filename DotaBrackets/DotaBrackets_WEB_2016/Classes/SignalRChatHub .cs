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
        public Group removeFromGroup = new Group();
        public int i;
        public int n;

        //allows messages within a group
        public void BroadCastGroupMessage(string msgFrom, string msg, string groupName)
        {
            try
            {
                Clients.Group(groupName).addChatMessage(msgFrom, msg);
            }
            catch
            {
                Clients.Caller.addChatMessage("Server", "Not Connected");
            }
        }

        //sends matched party member's username and steamid to page
        public void GetPartyMembers(string groupName)
        {
            try
            {
                Group thisGroup = allGroups.allGroups.Single(group => group.groupName == groupName);
                Clients.Group(groupName).recievePartyMember(thisGroup);
            }
            catch
            {
                return;
            }
        }

        //sends the name of the clients connected group to the page (used to chat with group members)
        public void GetGroupName(string groupName)
        {
            Clients.Group(groupName).addContosoChatMessageToPage(groupName);
        }

        //allows users to leave all groups
        public void LeaveGroup(string data)
        {
            Gamer thisGamer = new Gamer();
            thisGamer = JsonConvert.DeserializeObject<Gamer>(data);

            if (allGroups.allGroups.Count != 0)
            {
                for (int i = 0; i < allGroups.allGroups.Count; i++)
                {
                    if (allGroups.allGroups[i].members.Count != 0)
                    {
                        try
                        {
                            Groups.Remove(Context.ConnectionId, allGroups.allGroups[i].members[0].gamerID.ToString());
                            allGroups.allGroups[i].members.RemoveAll(s => s.steamID == thisGamer.steamID);
                            GetPartyMembers(allGroups.allGroups[i].groupName);
                        }
                        catch
                        {
                            return;
                        }
                    }
                }
            }
        }

        //allows users to join a group
        public void JoinGroup(string data)
        {
            Gamer thisGamer = new Gamer();
            thisGamer = JsonConvert.DeserializeObject<Gamer>(data);

            if (allGroups.allGroups.Count != 0)
            {
                for (int i = 0; i < allGroups.allGroups.Count; i++)
                {
                    //if the gamer is still looking for a match
                    if (thisGamer.isSearching == false)
                    {
                        //if the group has members in it
                        if (allGroups.allGroups[i].members.Count != 0)
                        {
                            //if the group has less than 5 members and there is a match 
                            if (allGroups.allGroups[i].members.Count < 5 && CheckMatch(thisGamer, allGroups.allGroups[i].members[0]) == true)
                            {
                                allGroups.allGroups[i].members.Add(thisGamer);
                                Groups.Add(Context.ConnectionId, allGroups.allGroups[i].groupName);
                                GetGroupName(allGroups.allGroups[i].groupName);
                                GetPartyMembers(allGroups.allGroups[i].groupName);
                                thisGamer.isSearching = true;
                            }
                            //if we reach the end of the groups and no match was found
                            //then create a group and add it to the end of the list
                            else if (i == allGroups.allGroups.Count)
                            {
                                allGroups.allGroups.Add(new Group());
                                allGroups.allGroups[i + 1].members.Add(thisGamer);
                                allGroups.allGroups[i + 1].groupName = thisGamer.gamerID.ToString();
                                Groups.Add(Context.ConnectionId, thisGamer.gamerID.ToString());
                                GetGroupName(thisGamer.gamerID.ToString());
                                GetPartyMembers(thisGamer.gamerID.ToString());
                                thisGamer.isSearching = true;
                            }
                            //there was no match on this iteration
                            //return to loop for next iteration
                            else
                            {
                                return;
                            }
                        }
                        //the current iterated group has no members in it
                        else
                        {
                            allGroups.allGroups[i].members.Add(thisGamer);
                            allGroups.allGroups[i].groupName = thisGamer.gamerID.ToString();
                            Groups.Add(Context.ConnectionId, thisGamer.gamerID.ToString());
                            GetGroupName(thisGamer.gamerID.ToString());
                            GetPartyMembers(thisGamer.gamerID.ToString());
                            thisGamer.isSearching = true;
                        }
                    }
                    //if the match was found but there are more groups to iterate through
                    //iterate through till end while taking no actions
                    else
                    {
                        return;
                    }
                }
            }
            //the list of groups is empty
            //create a new group in the list
            else
            {
                allGroups.allGroups.Add(new Group());
                allGroups.allGroups[0].members.Add(thisGamer);
                allGroups.allGroups[0].groupName = thisGamer.gamerID.ToString();
                Groups.Add(Context.ConnectionId, thisGamer.gamerID.ToString());
                GetGroupName(thisGamer.gamerID.ToString());
                GetPartyMembers(thisGamer.gamerID.ToString());
                thisGamer.isSearching = true;
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