using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotaBrackets_WEB_2016.Models;

namespace DotaBrackets_WEB_2016.Classes
{
    public class GroupsLogic
    {
        public AllGroups allGroups = new AllGroups();
        public Group group = new Group();
        public int i;
        public int n;
        
        public void joinGroup(Gamer thisGamer)
        {
            for (i = 0; i < allGroups.allGroups.Count; i++) ;
            for (n = 0; n < allGroups.allGroups[i].members.Count; i++) ;

            foreach(Group group in allGroups.allGroups)
            {
                if(group.members.Count != 0)
                {
                    foreach(Gamer excGamer in group.members)
                    {
                        if(checkMatch(thisGamer, excGamer) == true && thisGamer.isSearching == true && group.members.Count < 5)
                        {
                            
                        }
                    }
                }
            }
        }



        public bool checkMatch(Gamer incGamer, Gamer excGamer)
        {
            bool match;
            if((excGamer.preferences.hasMic == incGamer.traits.hasMic) || excGamer.preferences.hasMic == 1)
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