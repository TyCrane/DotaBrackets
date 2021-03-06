﻿using DotaBrackets_WEB_2016.Models;
using Newtonsoft.Json;
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
                viewModel = api.GetDotaSummaries(viewModel);

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
                incModel = api.GetDotaSummaries(incModel);

                Session["viewModel"] = incModel;

                return View("Login", incModel);
            }
            //if model is null return wrong passsword screen
            else
            {
                return View("Error");
            }
        }

        //gets a users friendslist
        public string refreshFriends(string incData)
        {
            Gamer gamer = new Gamer();
            FriendsList friends = new FriendsList();

            gamer = JsonConvert.DeserializeObject<Gamer>(incData);

            AccountDataController dataController = new AccountDataController();

            friends = dataController.GetFriendsList(gamer);

            FriendID[] friendArray = friends.friendsList.ToArray();

            string friendJson = JsonConvert.SerializeObject(friendArray);

            return friendJson;
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
            ViewModel steamUser = new ViewModel();

            steamUser = (ViewModel)Session["viewModel"];
            viewModel.gamer.avatar = steamUser.gamer.avatar;
            viewModel.gamer.steamID = steamUser.gamer.steamID;
            viewModel.gamer.traitsID = steamUser.gamer.traitsID;
            viewModel.gamer.preferencesID = steamUser.gamer.preferencesID;
            viewModel.gamer.gamerID = steamUser.gamer.gamerID;

            viewModel = dataController.EditUser(viewModel);


            //new user added, now login
            if (viewModel.gamer.userName != null)
            {
                APIController api = new APIController();

                viewModel = api.GetPlayerSummaries(viewModel);
                viewModel = api.GetSteamNews(viewModel);
                //put new account info into session
                Session["viewModel"] = viewModel;

                //return success page
                return View("UpdateSuccesful", viewModel);
            }
            //new user not added, already exists
            else
            {
                //input error
                return View("Error");
            }
        }

        //add a friend to your friendsList
        public string addFriend(string incData, string dota2ID, string friendUserName)
        {
            Gamer gamer = new Gamer();
            FriendID friendToAdd = new FriendID();

            gamer = JsonConvert.DeserializeObject<Gamer>(incData);
            friendToAdd.friendID = gamer.gamerID;
            friendToAdd.dota2ID = Convert.ToInt64(dota2ID);
            friendToAdd.userName = friendUserName;

            gamer.friendsList.friendsList.Add(friendToAdd);

            AccountDataController dataController = new AccountDataController();

            friendToAdd = dataController.AddFriendList(friendToAdd);

            string friendJson = JsonConvert.SerializeObject(friendToAdd);

            return friendJson;
        }

        //************************************************ Methods to Return the Loggedin Page ***********************************************
        //called when navigating between menus
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
                viewModel = api.GetDotaSummaries(viewModel);

                Session["viewModel"] = viewModel;

                return View(viewModel);
            }
            else
            {
                return View("Error");
            }
        }
        /*********************************************** Methods to delete ****************************************/
        //deletes a friend from a users friend list
        public string deleteFriend(string incData, string dota2ID)
        {
            Gamer gamer = new Gamer();
            FriendID friendToDelete = new FriendID();

            gamer = JsonConvert.DeserializeObject<Gamer>(incData);

            friendToDelete.dota2ID = Convert.ToInt64(dota2ID);
            friendToDelete.friendID = gamer.gamerID;

            AccountDataController dataController = new AccountDataController();

            dataController.DeleteFriend(friendToDelete);

            string json = refreshFriends(incData);

            return json;

        }
    }
}
