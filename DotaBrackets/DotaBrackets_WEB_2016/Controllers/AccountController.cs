using DotaBrackets_WEB_2016.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DotaBrackets_WEB_2016.Controllers
{
    public class AccountController : Controller
    {
//********************************************** Methods to create a user ************************************************************

    //creates a user and passes the model to the Login Method to display logged in screen
    public ActionResult Create(FormCollection col)
        {
            try
            {
                ViewModel viewModel = new ViewModel();


                viewModel.gamer.userName = col["userName"];
                viewModel.gamer.access = col["access"];
                viewModel.gamer.traits.mmr = Convert.ToInt32(col["tmmr"]);
                viewModel.gamer.traits.language = Convert.ToInt32(col["tlang"]);
                viewModel.gamer.traits.hasMic = Convert.ToInt32(col["thasMic"]);
                viewModel.gamer.traits.server = Convert.ToInt32(col["tserv"]);
                viewModel.gamer.preferences.mmr = Convert.ToInt32(col["pmmr"]);
                viewModel.gamer.preferences.hasMic = Convert.ToInt32(col["phasMic"]);
                viewModel.gamer.preferences.language = Convert.ToInt32(col["plang"]);
                viewModel.gamer.steamID = Convert.ToInt64(col["steamID"]);

                var dota2ID = (viewModel.gamer.steamID - 76561197960265728);
                viewModel.gamer.dota2ID = Convert.ToInt32(dota2ID);

                APIController api = new APIController();

                viewModel = api.GetPlayerSummaries(viewModel);

                AccountDataController dataController = new AccountDataController();

                    

                viewModel = dataController.CreateUser(viewModel);

                //new user added, now login
                if (viewModel.gamer.userName != null)
                {
                    //login
                    TempData["viewModel"] = viewModel;
                    return RedirectToAction("LoginWithModel");
                }
                //new user not added, already exists
                else
                {
                    //already exits or input error
                    return View("Error");
                }
             }
            catch
            {
                //improper data entry format
                return View("Error");
            }
        }

//********************************************** Methods to Login ********************************************************************

        //logs user in w/ form collection and returns view: overload (formcollection)
        public ActionResult LoginWithForm(FormCollection col)
        {
            //create model and fill it witht the access credentials from the form
            ViewModel viewModel = new ViewModel();

            viewModel.gamer.userName = col["userName"];
            viewModel.gamer.access = col["access"];

            //send the credentials to the database to check and see if they match
            AccountDataController dataController = new AccountDataController();

            viewModel = dataController.LoginExistingUser(viewModel);

            //if model is valid return logged in screen
            if (viewModel.gamer.userName != null)
            {
                APIController api = new APIController();

                viewModel = api.GetSteamNews(viewModel);

                Session["viewModel"] = viewModel;

                return View("Login", viewModel);
            }
            //if model is null return wrong passsword screen
            else
            {
                return View("Error");
            }
        }

        //returns logged in screen: overload (ViewModel)
        public ActionResult LoginWithModel(ViewModel incModel)
        {
            //send the credentials to the database to check and see if they match
            AccountDataController dataController = new AccountDataController();

            if (TempData["viewModel"] != null)
            {
                incModel = (ViewModel)TempData["viewModel"];
                incModel = dataController.LoginNewUser(incModel);
            }
            else
            {
                incModel = dataController.LoginNewUser(incModel);
            }

            //if model is valid return logged in screen
            if (incModel.gamer.userName != null)
            {
                APIController api = new APIController();

                incModel = api.GetSteamNews(incModel);

                Session["viewModel"] = incModel;

                return View("Login", incModel);
            }
            //if model is null return wrong passsword screen
            else
            {
                return View("Error");
            }
        }

//**************************************************** Edit Account ******************************************************************
        //returns edit account page
        public ActionResult EditAccount()
        {
            ViewModel blankModel = new ViewModel();

            TypeController typeController = new TypeController();

            SelectList mmrList = typeController.GetMmrTypeList();
            SelectList hasMicList = typeController.GetHasMicType();
            SelectList langList = typeController.GetLangType();
            SelectList servList = typeController.GetServType();

            ViewBag.mmr = mmrList;
            ViewBag.hasMic = hasMicList;
            ViewBag.lang = langList;
            ViewBag.serv = servList;

            return View(blankModel);
        }

        //method to eddit account information
        public ActionResult Edit(ViewModel viewModel)
        {
            AccountDataController dataController = new AccountDataController();

            viewModel = dataController.EditUser(viewModel);

            //new user added, now login
            if (viewModel.gamer.userName != null)
            {
                //put new account info into session
                Session["viewModel"] = viewModel;

                //return success page
                //TODO: make updatesuccesful page in account folder
                return View("UpdateSuccesful");
            }
            //new user not added, already exists
            else
            {
                //input error
                return View("Error");
            }
        }

//************************************************ Methods to Return the Loggedin Page ***********************************************
        public ActionResult LoginNoParameter()
        {
            ViewModel viewModel = new ViewModel();
            if (Session["viewModel"] != null)
            {
                viewModel = (ViewModel)Session["viewModel"];

                return View("Login", viewModel);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }
        //returns the loggedin page and passes it the filled viewModel
        public ActionResult Login(ViewModel viewModel)
        {
            if (viewModel.gamer.userName != null)
            {
                APIController api = new APIController();

                viewModel = api.GetSteamNews(viewModel);

                Session["viewModel"] = viewModel;

                return View(viewModel);
            }
            else
            {
                return View("Error");
            }
        }
    }
}
