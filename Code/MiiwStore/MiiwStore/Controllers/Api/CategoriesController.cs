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
    [RoutePrefix("api/Categories")]
    public class CategoriesController : ApiController
    {
        private readonly CategoryService categoryService;

        public CategoriesController()
        {
            categoryService = new CategoryService();
        }

        // GET: api/Categories
        [ResponseType(typeof(CategoryModel))]
        public IHttpActionResult GetCategories()
        {
            IEnumerable<CategoryModel> categories = categoryService.GetCategories();

            if (categories.Count() == 0)
            {
                return NotFound();
            }
            return Ok(categories);
        }

        // GET: api/Categories/5
        [ResponseType(typeof(CategoryModel))]
        public IHttpActionResult GetCategory(int id)
        {
            CategoryModel category = categoryService.GetById(id);
            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        // PUT: api/Categories/5
        [ResponseType(typeof(CategoryModel))]
        public IHttpActionResult PutCategory(CategoryModel model)
        {

            string errorMessage = string.Empty;
            try
            {
                CategoryModel resultModel = categoryService.Update(model, ref errorMessage);
                if (string.IsNullOrEmpty(errorMessage)) return Ok(resultModel);
                else return BadRequest(errorMessage);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST: api/Categories
        [ResponseType(typeof(CategoryModel))]
        public IHttpActionResult PostCategory(CategoryModel model)
        {
            string errorMessage = string.Empty;

            try
            {
                CategoryModel category = categoryService.Create(model, ref errorMessage);
                return CreatedAtRoute("DefaultApi", new { id = model.ID }, category);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE: api/Categories/5
        [ResponseType(typeof(CategoryModel))]
        public IHttpActionResult DeleteCategory(int id)
        {
            string errorMessage = string.Empty;
            CategoryModel model = categoryService.Delete(id, ref errorMessage);
            if (string.IsNullOrEmpty(errorMessage)) return Ok(model);
            else return BadRequest(errorMessage);
            
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                categoryService.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}