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
        private StoreContext db = new StoreContext();
        public ActionResult Index()
        {
            List<Product> products = db.Products.ToList();
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
    }
}
