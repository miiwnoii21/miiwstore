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
using MiiwStore.Services;

namespace MiiwStore.Controllers.Api
{
    public class ProductsController : ApiController
    {

        private readonly ProductService productService;

        public ProductsController()
        {
            productService = new ProductService();
        }



        // GET: api/Products
        [ActionName("List")]
        public IHttpActionResult GetProducts()
        {
            var products = productService.GetProducts();
            if (products == null)
            {
                return NotFound();
            }

            return Ok(products);
        }

        // GET: api/Products/5
        [ActionName("ProductById")]
        [ResponseType(typeof(Product))]
        public IHttpActionResult GetProduct(int id)
        {
            ProductModel model = productService.GetById(id);
            if (model == null)
            {
                return NotFound();
            }

            return Ok(model);
        }

        [ActionName("Search")]
        public IHttpActionResult GetProduct(string name = "", string category = "", string subCategory = "")
        {

            IEnumerable<ProductModel> model = productService.Search(name, category, subCategory);

            if (model.Count() <= 0)
            {
                return NotFound();
            }

            return Ok(model.Select(s => AutoMapper.Mapper.Map<ProductModel>(s)));

        }

        // PUT: api/Products/5
        [ActionName("UpdateProduct")]
        [ResponseType(typeof(ProductModel))]
        public IHttpActionResult PutProduct(ProductModel model)
        {

            try
            {
                string errorMessage = string.Empty;
                ProductModel resultModel = productService.Update(model, ref errorMessage);

                if (string.IsNullOrEmpty(errorMessage)) return Ok(resultModel);
                else return BadRequest(errorMessage);

            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

        // POST: api/Products
        [ActionName("CreateProduct")]
        [ResponseType(typeof(ProductModel))]
        public IHttpActionResult PostProduct(ProductModel model)
        {
            
            try
            {
                string errorMessage = string.Empty;
                ProductModel resultModel = productService.Create(model, ref errorMessage);

                if (resultModel != null) return CreatedAtRoute("DefaultProductApi", new { id = resultModel.ID }, resultModel);
                else return BadRequest(errorMessage);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }


        }

        // DELETE: api/Products/5
        [ActionName("DeleteProductById")]
        [ResponseType(typeof(ProductModel))]
        public IHttpActionResult DeleteProduct(int id)
        {
            ProductModel model = productService.Delete(id);
            if (model == null)
            {
                return NotFound();
            }

            return Ok(model);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                productService.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}