using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using MiiwStore.DAL;
using MiiwStore.Models;
using MiiwStore.Models.ViewModels;

namespace MiiwStore.Controllers
{
    public class ProductsController : ApiController
    {
        private StoreContext db = new StoreContext();

        // GET: api/Products/List
        [ActionName("List")]
        public IEnumerable<ProductModel> GetProducts()
        {
            List<ProductModel> productModelList = new List<ProductModel>();

            foreach (var item in db.Products.ToList())
            {
                productModelList.Add(AutoMapper.Mapper.Map<ProductModel>(item));
            }

            return productModelList;

            //return db.Products.ToList().Select(AutoMapper.Mapper.Map<ProductModel>);
        }

        [ActionName("Catalog")]
        // GET: api/Products/Catalog
        public IEnumerable<ProductModel> GetProductCatalog()
        {
            //var prod = from p in db.Products
            //           where p.ProductID == 1 
            //           select p;

            // select * from Products where Products.ProductID = 1

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
            return productModelList;
        }

        // GET: api/products/5/
        [ActionName("ProductById")]
        [ResponseType(typeof(ProductModel))]
        public IHttpActionResult GetProduct(int id)
        {
            Product product = db.Products.Find(id);

            ProductModel productModel = new ProductModel();
            productModel = AutoMapper.Mapper.Map<ProductModel>(product);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(productModel);
        }

        // PUT: api/Products/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProduct(int id, Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != product.ProductID)
            {
                return BadRequest();
            }

            db.Entry(product).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Products
        [ResponseType(typeof(Product))]
        public IHttpActionResult PostProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Products.Add(product);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = product.ProductID }, product);
        }

        // DELETE: api/Products/5
        [ResponseType(typeof(Product))]
        public IHttpActionResult DeleteProduct(int id)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            db.Products.Remove(product);
            db.SaveChanges();

            return Ok(product);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductExists(int id)
        {
            return db.Products.Count(e => e.ProductID == id) > 0;
        }
    }
}