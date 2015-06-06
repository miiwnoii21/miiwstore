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

        private bool IsValid(ProductModel model, ref string errorMessage, bool isCreate = true)
        {
            List<string> failList = new List<string>();


            if (model != null)
            {

                if (!isCreate && db.Products.Find(model.ID) == null)
                {
                    failList.Add("Invalid product id");
                }
                if (isCreate && db.Products.Find(model.ID) != null)
                {
                    failList.Add("Duplicate product id");
                }
                if (string.IsNullOrEmpty(model.Name))
                {
                    failList.Add("Please insert name");
                }
                if (string.IsNullOrEmpty(model.Image))
                {
                    failList.Add("Please Insert image");
                }
                if (model.Price <= 0)
                {
                    failList.Add("Please Insert Price");
                }
                if (db.SubCategories.Find(model.SubCategoryID) == null)
                {
                    failList.Add("Invalid subcategory id");
                }
                if (failList.Count > 0)
                {
                    errorMessage = string.Join(", ", failList.ToArray());
                    return false;
                }

                return true;
            }
            else
            {
                errorMessage = "Invalid model";
                return false;
            }

        }

        // GET: api/
        [ActionName("List")]
        public IHttpActionResult GetProducts()
        {
            List<Product> products = db.Products.ToList();
            if (products.Count == 0)
            {
                return NotFound();
            }

            //List<ProductListModel> list = new List<ProductListModel>();

            //products.ForEach(s => list.Add(AutoMapper.Mapper.Map<ProductListModel>(s)));

            //return Ok(list);

            //return Ok(products.Select(s => AutoMapper.Mapper.Map<ProductListModel>(s)));

            //return Ok(AutoMapper.Mapper.Map<List<ProductListModel>>(products));
            return Ok(products.Select(AutoMapper.Mapper.Map<ProductModel>));
        }

        // GET: api/Products/5
        [ActionName("ProductById")]
        [ResponseType(typeof(Product))]
        public IHttpActionResult GetProduct(int id)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(AutoMapper.Mapper.Map<ProductModel>(product));
        }

        [ActionName("Search")]
        public IHttpActionResult GetProduct(string name = "", string category = "", string subCategory = "")
        {

            //bool chkName = (!string.IsNullOrEmpty(name) && contains(name));
            // select * from table1 where ( ( name is null || table1.name like '%name%') and ( category is null || table1.category like '%cat%')
            IEnumerable<Product> products = db.Products.ToList().Where(s =>
            {

                return ((string.IsNullOrEmpty(name) || s.Name.ToLowerInvariant().Contains(name.ToLowerInvariant()))
                && (string.IsNullOrEmpty(subCategory) || s.SubCategory.Name.ToLowerInvariant().Contains(subCategory.ToLowerInvariant()))
                && (string.IsNullOrEmpty(category) || s.SubCategory.Category.Name.ToLowerInvariant().Contains(category.ToLowerInvariant())));

            });

            if (products.Count() <= 0)
            {
                return NotFound();
            }

            return Ok(products.Select(s => AutoMapper.Mapper.Map<ProductModel>(s)));

        }

        // PUT: api/Products/5
        [ActionName("UpdateProduct")]
        [ResponseType(typeof(ProductModel))]
        public IHttpActionResult PutProduct(ProductModel model)
        {

            //db.Entry(product).State = EntityState.Modified;
            string errorMessage = string.Empty;

            if (!IsValid(model,ref errorMessage, false))
            {
                return BadRequest(errorMessage);
            }
            
            try
            {

                Product product = db.Products.Find(model.ID);
                product.Name = model.Name;
                product.Price = model.Price;
                product.Image = model.Image;
                product.Description = model.Description;
                product.Discount = model.Discount;
                product.SubCategoryID = model.SubCategoryID;
                product.Size = model.Size;
                db.SaveChanges();

                return Ok(AutoMapper.Mapper.Map<ProductModel>(product));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

        // POST: api/Products
        [ActionName("createProduct")]
        [ResponseType(typeof(ProductModel))]
        public IHttpActionResult PostProduct(ProductModel model)
        {
            string errorMessage = string.Empty;

            if (!IsValid(model, ref errorMessage))
            {
                return BadRequest(errorMessage);
            }

            try
            {
                Product product = db.Products.Add(AutoMapper.Mapper.Map<Product>(model));
                db.SaveChanges();

                return Ok(AutoMapper.Mapper.Map<ProductModel>(product));
            }
            catch(Exception ex){
                return InternalServerError(ex);
            }

            
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
            return db.Products.Count(e => e.ID == id) > 0;
        }
    }
}