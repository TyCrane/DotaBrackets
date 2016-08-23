using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DotaBrackets_WEB_2016.Models;
using Newtonsoft.Json;

namespace DotaBrackets_WEB_2016.Controllers
{
    public class SignalRGroupsController : Controller
    {
        //changes a gamers 'isSearching' status to true
        public string StartSearching(string incData)
        {
            Gamer gamer = new Gamer();

            gamer = JsonConvert.DeserializeObject<Gamer>(incData);

            gamer.isSearching = true;

            string returnData = JsonConvert.SerializeObject(gamer);

            return returnData;
        }

        //changes a gamers 'isSearching' status to false
        public string StopSearching(string incData)
        {
            Gamer gamer = new Gamer();

            gamer = JsonConvert.DeserializeObject<Gamer>(incData);

            gamer.isSearching = false;

            string returnData = JsonConvert.SerializeObject(gamer);

            return returnData;
        }
    }
}
