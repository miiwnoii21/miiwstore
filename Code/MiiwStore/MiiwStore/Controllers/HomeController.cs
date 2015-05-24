﻿using MiiwStore.DAL;
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

            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult Products()
        {
            StoreContext db = new StoreContext();
            var prodWithFirstChild = from p in db.Products.ToList()
                                     select new Product
                                     {
                                         ProductID = p.ProductID,
                                         ProdName = p.ProdName,
                                         ProductDetails = new List<ProductDetail> { p.ProductDetails.FirstOrDefault() }
                                     };

            List<ProductModel> productModelList = new List<ProductModel>();

            foreach (var item in prodWithFirstChild)
            {
                productModelList.Add(AutoMapper.Mapper.Map<ProductModel>(item));
            }


            return View(productModelList);
        }

        public ActionResult Catalog()
        {
            return View();
        }
    }
}