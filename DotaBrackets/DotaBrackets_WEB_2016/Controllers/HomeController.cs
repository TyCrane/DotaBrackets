using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Configuration;
using DotaBrackets_WEB_2016.Models;
using Newtonsoft.Json;
using System.Net;

namespace DotaBrackets_WEB_2016.Controllers
{
    public class HomeController : Controller
    {
        string apiKey = WebConfigurationManager.AppSettings["ApiKey"];

        public ActionResult Index()
        {
            ViewModel allArticles = new ViewModel();

            string jsonResult;

            using (WebClient wc = new WebClient())
            {
                try
                {
                    jsonResult = wc.DownloadString("http://api.steampowered.com/ISteamNews/GetNewsForApp/v0002/?appid=570&count=3&maxlength=300&format=json");
                }
                catch
                {
                    return View();
                }

                try
                {
                    allArticles.rootObject = JsonConvert.DeserializeObject<RootObject>(jsonResult);
                }
                catch
                {
                    return View();
                }
            }

                return View(allArticles);
        }

        public ActionResult Register()
        {

            return View();
        }

       
    }
}