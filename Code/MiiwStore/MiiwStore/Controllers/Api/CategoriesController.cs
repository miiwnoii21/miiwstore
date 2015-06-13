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
    [RoutePrefix("api/Categories")]
    public class CategoriesController : ApiController
    {
        private StoreContext db = new StoreContext();

        private bool IsValid(CategoryModel model, ref string errorMessage, bool isCreate = true)
        {
            List<string> failList = new List<string>();


            if (model != null)
            {

                if (!isCreate && db.Categories.Find(model.ID) == null)
                {
                    failList.Add("Invalid category id");
                }
                if (isCreate && db.Categories.Find(model.ID) != null)
                {
                    failList.Add("Duplicate category id");
                }
                if (model.Name == null)
                {
                    failList.Add("Please enter category name");
                }
                if (isCreate && db.Categories.Any(s => s.Name == model.Name))
                {
                    failList.Add("Duplicate category name");
                }
                if (model.SubCategories.Count() == 0)
                {
                    failList.Add("Please insert subcategory");
                }
                else
                {
                    model.SubCategories.All(sc =>
                    {
                        if (!isCreate && model.ID != sc.CategoryID)
                        {
                            failList.Add("Invalid category id (in sub)");
                            return false;
                        }
                        if (string.IsNullOrEmpty(sc.Name))
                        {
                            failList.Add("Please enter subcategory name");
                            return false;
                        }
                        if (sc.IsDelete == true)
                        {
                            SubCategory tmpSubCategory = db.SubCategories.Find(sc.ID);
                            if (tmpSubCategory != null && tmpSubCategory.Products.Count > 0)
                            {
                                failList.Add("Subcategory is used by products");
                                return false;
                            }

                        }
                        return true;
                    });
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

        private bool CanDelete(Category model, ref string errorMessage)
        {

            if (model.SubCategories.Any(s => s.Products.Count > 0))
            {
                errorMessage = "Subcategory is used by product";
                return false;
            }
            else return true;
        }

        // GET: api/Categories
        public IHttpActionResult GetCategories()
        {
            List<Category> categories = db.Categories.ToList();
            if (categories.Count == 0)
            {
                return NotFound();
            }
            return Ok(categories.Select(s => AutoMapper.Mapper.Map<CategoryModel>(s)));
        }

        // GET: api/Categories/5
        [ResponseType(typeof(CategoryModel))]
        public IHttpActionResult GetCategory(int id)
        {
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }

            return Ok(AutoMapper.Mapper.Map<CategoryModel>(category));
        }

        // PUT: api/Categories/5
        [ResponseType(typeof(CategoryModel))]
        public IHttpActionResult PutCategory(CategoryModel model)
        {

            string errorMessage = string.Empty;
            if (!IsValid(model, ref errorMessage, false))
            {
                return BadRequest(errorMessage);
            }

            try
            {
                Category category = db.Categories.Find(model.ID);
                category.Name = model.Name;


                foreach (var item in model.SubCategories)
                {
                    SubCategory tmpSubcat = category.SubCategories.SingleOrDefault(s => s.ID == item.ID);

                    if (tmpSubcat == null)
                    {
                        category.SubCategories.Add(AutoMapper.Mapper.Map<SubCategory>(item));
                    }
                    else
                    {
                        if (item.IsDelete == true) db.SubCategories.Remove(tmpSubcat);
                        else tmpSubcat.Name = item.Name;

                    }
                }
                //category.SubCategories.Clear();
                //category.SubCategories = new List<SubCategory>(model.SubCategories.Select(AutoMapper.Mapper.Map<SubCategory>));
                db.SaveChanges();

                return Ok(AutoMapper.Mapper.Map<CategoryModel>(category));
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
            if (!IsValid(model, ref errorMessage))
            {
                return BadRequest(errorMessage);
            }

            try
            {
                Category category = db.Categories.Add(AutoMapper.Mapper.Map<Category>(model));
                db.SaveChanges();

                return CreatedAtRoute("DefaultApi", new { id = model.ID }, AutoMapper.Mapper.Map<CategoryModel>(category));
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
            Category category = db.Categories.Find(id);
            if (category == null) return NotFound();
            if (!CanDelete(category, ref errorMessage)) return BadRequest(errorMessage);

            db.Categories.Remove(category);
            db.SaveChanges();

            return Ok(AutoMapper.Mapper.Map<CategoryModel>(category));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CategoryExists(int id)
        {
            return db.Categories.Count(e => e.ID == id) > 0;
        }
    }
}