using MiiwStore.DAL;
using MiiwStore.Models;
using MiiwStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MiiwStore.Controllers
{
    public class HomeController : Controller
    {
       
        public ActionResult Index()
        {
            ViewBag.Title = "Welcome to Miiw's Store";

            return View();
        }

        public ActionResult Pluton()
        {
            ViewBag.Title = "Welcome to Miiw's Store";

            return View();
        }


        public ActionResult Catalog()
        {
            return View();
        }

        public ActionResult MyApp()
        {
            return View();
        }

        public ActionResult AboutUs()
        {
            return View();
        }

        public ActionResult ContactUs()
        {
            return View();
        }
    }
}
