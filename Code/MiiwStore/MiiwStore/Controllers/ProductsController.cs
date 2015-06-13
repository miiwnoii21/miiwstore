using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MiiwStore.DAL;
using MiiwStore.Models;
using MiiwStore.Services;
using MiiwStore.Models.ViewModels;

namespace MiiwStore.Controllers
{
    public class ProductsController : Controller
    {
        // private StoreContext db = new StoreContext();
        private readonly ProductService productService;
        private readonly CategoryService categoryService;
        public ProductsController()
        {
            productService = new ProductService();
            categoryService = new CategoryService();
        }


        // GET: Products
        public ActionResult Index()
        {
            var products = productService.GetProducts();
            return View(products);
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductModel product = productService.GetById(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.SubCategoryID = new SelectList(categoryService.GetSubCategories(), "ID", "FullName");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Image,Price,Discount,SubCategoryID,Description,Size,Quantity")] ProductModel product)
        {
            if (ModelState.IsValid)
            {
                productService.Create(product);
                return RedirectToAction("Index");

            }

            ViewBag.SubCategoryID = new SelectList(categoryService.GetSubCategories(), "ID", "FullName", product.SubCategoryID);
            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductModel product = productService.GetById(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.SubCategoryID = new SelectList(categoryService.GetSubCategories(), "ID", "FullName", product.SubCategoryID);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Image,Price,Discount,SubCategoryID,Description,Size,Quantity")] ProductModel product)
        {
            if (ModelState.IsValid)
            {
                productService.Update(product);
                return RedirectToAction("Index");
            }
            ViewBag.SubCategoryID = new SelectList(categoryService.GetSubCategories(), "ID", "FullName", product.SubCategoryID);
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductModel product = productService.GetById(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            productService.Delete(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                productService.Dispose();
                categoryService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
