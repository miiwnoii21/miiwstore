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

namespace MiiwStore.Controllers.Api
{
    public class ProductsController : ApiController
    {
        private StoreContext db = new StoreContext();

        private ProductListModel Mapper(Product product)
        {

            decimal productPrice = 0;
            foreach (var item in product.ProductDetails)
            {
                productPrice += item.Price;
            }
            return new ProductListModel
            {
                ProductID = product.ProductID,
                ProductName = product.ProductName,
                Price = productPrice,
                PicUrl = product.PicUrl
            };

        }
        // GET: api/Products
        [ActionName("List")]
        public IHttpActionResult GetProducts()
        {
            //List<ProductListModel> productListModels = new List<ProductListModel>();

            //foreach (var item in db.Products.ToList())
            //{
            //    productListModels.Add(Mapper(item));
            //}
            //return Ok(productListModels);

            //return Ok(db.Products.ToList().Select(p => Mapper(p)));

            return Ok(db.Products.ToList().Select(AutoMapper.Mapper.Map<ProductListModel>));
        }

        // GET: api/Products/5
        [ResponseType(typeof(Product))]
        public IHttpActionResult GetProduct(int id)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [ActionName("Search")]
        public IHttpActionResult GetProductByProductName(string productName)
        {
            //List<ProductListModel> productListModels = new List<ProductListModel>();
            //db.Products.Where(pd => pd.ProductName.Contains(productName)).ToList().ForEach(p=>productListModels.Add(Mapper(p)));
            //db.Products.Where(pd => pd.ProductName.Contains(productName)).ToList().ForEach(p => productListModels.Add(AutoMapper.Mapper.Map<ProductListModel>(p)));
            // return Ok(productListModels);
            return Ok(db.Products.Where(pd => pd.ProductName.Contains(productName)).ToList().Select(AutoMapper.Mapper.Map<ProductListModel>));
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