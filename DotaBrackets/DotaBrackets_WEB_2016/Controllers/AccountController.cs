using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DotaBrackets_WEB_2016.Models;

namespace DotaBrackets_WEB_2016.Controllers
{
    public class AccountController : Controller
    {

      //************************************* Called when login button is pressed **************************************
        //login an existing member
       public ActionResult Login(FormCollection col)
        {
            //create model and fill it witht the access credentials from the form
            ViewModel viewModel = new ViewModel();

            viewModel.gamer.userName = col["userName"];
            viewModel.gamer.access = col["access"];

            //send the credentials to the database to check and see if they match
            AccountDataController dataController = new AccountDataController();

            viewModel = dataController.Login(viewModel);

            //if model is valid return logged in screen
            if (viewModel != null)
            {
                return View(viewModel);
            }
            //if model is null return wrong passsword screen
            else
            {
                return View("Error");
            }
        }
     //********************************** Called when Submit button is pressed on Register****************************
        //Creates a new User
        public ActionResult Create(FormCollection col)
        {
            try
            {
                ViewModel viewModel = new ViewModel();

                viewModel.gamer.userName = col["userName"];
                viewModel.gamer.access = col["access"];
                viewModel.gamer.traits.language = Convert.ToInt32(col["tlang"]);
                viewModel.gamer.traits.hasMic = Convert.ToInt32(col["thasMic"]);
                viewModel.gamer.traits.server = Convert.ToInt32(col["tserv"]);
                viewModel.gamer.traits.mmr = Convert.ToInt32(col["tmmr"]);
                viewModel.gamer.preferences.mmr = Convert.ToInt32(col["pmmr"]);
                viewModel.gamer.preferences.hasMic = Convert.ToInt32(col["phasMic"]);
                viewModel.gamer.preferences.language = Convert.ToInt32(col["plang"]);
                viewModel.gamer.steamID = Convert.ToInt64(col["steamID"]);
                


                AccountDataController dataController = new AccountDataController();

                dataController.CreateUser(viewModel);

                return View("Login");
            }
            catch
            {
                return View("Error");
            }
        }
    }
}
