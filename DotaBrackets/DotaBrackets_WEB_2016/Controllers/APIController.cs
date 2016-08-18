using DotaBrackets_WEB_2016.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace DotaBrackets_WEB_2016.Controllers
{
    public class APIController : Controller
    {
        //This is the api key for accessing the api
        string apiKey = WebConfigurationManager.AppSettings["ApiKey"];


        //******************************************************** Get News Methods *****************************************************

        //retrieves news: overload (ViewModel)
        public ViewModel GetSteamNews(ViewModel viewModel)
        {
            string jsonResult;

            using (WebClient wc = new WebClient())
            {
                try
                {
                    jsonResult = wc.DownloadString("http://api.steampowered.com/ISteamNews/GetNewsForApp/v0002/?appid=570&count=3&maxlength=300&format=json");
                }
                catch
                {
                    return viewModel;
                }

                try
                {
                    viewModel.rootObject = JsonConvert.DeserializeObject<RootObject>(jsonResult);
                }
                catch
                {
                    return viewModel;
                }

                return viewModel;
            }
        }

        //retrieves news: overload (no parameter)
        public ViewModel GetSteamNews()
        {
            ViewModel viewModel = new ViewModel();

            string jsonResult;

            using (WebClient wc = new WebClient())
            {
                try
                {
                    jsonResult = wc.DownloadString("http://api.steampowered.com/ISteamNews/GetNewsForApp/v0002/?appid=570&count=3&maxlength=300&format=json");
                }
                catch
                {
                    return viewModel;
                }

                try
                {
                    viewModel.rootObject = JsonConvert.DeserializeObject<RootObject>(jsonResult);
                }
                catch
                {
                    return viewModel;
                }

                return viewModel;
            }
        }

        //********************************************** Get Player Summaries Methods ************************************************
        //retrieves player summaries
        public ViewModel GetPlayerSummaries(ViewModel viewModel)
        {
            string jsonResult;
            string url;

            url = "http://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?key=" + apiKey + "&steamids=" + viewModel.gamer.steamID;


            using (WebClient wc = new WebClient())
            {
                jsonResult = wc.DownloadString(url);

                viewModel.steamUser = JsonConvert.DeserializeObject<RootObject1>(jsonResult);
            }

            //if the api call was succesful
            if (viewModel.steamUser.response.players != null)
            {

                Player thisPlayer = new Player();
                thisPlayer = viewModel.steamUser.response.players[0];
                viewModel.gamer.avatar = thisPlayer.avatar;

                return viewModel;
            }
            //if the api call failed
            else
            {
                return viewModel;
            }
        }
    }
}
