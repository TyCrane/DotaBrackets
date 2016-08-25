using DotaBrackets_WEB_2016.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DotaBrackets_WEB_2016.Controllers
{
    public class HomeController : Controller
    {
        //*************************************************** Methods to return the index ****************************************************
        //returns the index: overload (ViewModel)
        public ActionResult Index()
        {
            ViewModel viewModel = new ViewModel();

            if (Session["viewModel"] != null)
            {
                viewModel = (ViewModel)Session["viewModel"];
            }
            APIController api = new APIController();

            viewModel = api.GetSteamNews(viewModel);

            return View(viewModel);
        }

        //logs the user out and returns index page
        public ActionResult LogOut()
        {
            Session["viewModel"] = null;

            return RedirectToAction("Index");
        }

//************************************************* Methods to return Registration Page **********************************************
    //returns the Registration page and fills the dropdown Boxes
    public ActionResult Register()
        {
            TypeController typeController = new TypeController();

            SelectList mmrList = typeController.GetMmrTypeList();
            SelectList hasMicList = typeController.GetHasMicType();
            SelectList langList = typeController.GetLangType();
            SelectList servList = typeController.GetServType();

            ViewBag.mmr = mmrList;
            ViewBag.hasMic = hasMicList;
            ViewBag.lang = langList;
            ViewBag.serv = servList;

            return View();
        }
    }
}
