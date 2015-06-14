using MiiwStore.DAL;
using MiiwStore.Models;
using MiiwStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiiwStore.Services
{
    public class CategoryService : IDisposable
    {
        private readonly StoreContext db;

        public CategoryService()
        {
            db = new StoreContext();
        }

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

        public IEnumerable<SubCategoryModel> GetSubCategories()
        {
            List<SubCategoryModel> models = new List<SubCategoryModel>();
            //db.SubCategories.ToList().ForEach(s => models.Add(new SubCategoryModel(s)));
            db.SubCategories.ToList().ForEach(s => models.Add(AutoMapper.Mapper.Map<SubCategoryModel>(s)));
            return models;

            //return db.SubCategories.ToList().Select(s => AutoMapper.Mapper.Map<SubCategoryModel>(s));
            //return db.SubCategories.ToList().Select(AutoMapper.Mapper.Map<SubCategoryModel>);

        }
        public IEnumerable<SubCategoryModel> GetSubCategories(int id)
        {
            List<SubCategoryModel> models = new List<SubCategoryModel>();
            db.SubCategories.Where(s => s.CategoryID == id).ToList().ForEach(s => models.Add(AutoMapper.Mapper.Map<SubCategoryModel>(s)));
            return models;

            //return db.SubCategories.ToList().Select(s => AutoMapper.Mapper.Map<SubCategoryModel>(s));
            //return db.SubCategories.ToList().Select(AutoMapper.Mapper.Map<SubCategoryModel>);

        }

        public IEnumerable<CategoryModel> GetCategories()
        {
            List<Category> categories = db.Categories.ToList();
            if (categories.Count == 0)
            {
                return null;
            }
            return categories.Select(s => AutoMapper.Mapper.Map<CategoryModel>(s));
        }

        public CategoryModel GetById(int id)
        {
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return null;
            }

            return AutoMapper.Mapper.Map<CategoryModel>(category);
        }

        public CategoryModel Update(CategoryModel model, ref string errorMessage)
        {
            if (!IsValid(model, ref errorMessage, false))
            {
                return null;
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

                return AutoMapper.Mapper.Map<CategoryModel>(category);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CategoryModel Update(CategoryModel model)
        {
            string errorMessage = string.Empty;
            return Update(model, ref errorMessage);
        }

        public CategoryModel Create(CategoryModel model, ref string errorMessage)
        {
            if (!IsValid(model, ref errorMessage))
            {
                return null;
            }

            try
            {
                Category category = db.Categories.Add(AutoMapper.Mapper.Map<Category>(model));
                db.SaveChanges();

                return AutoMapper.Mapper.Map<CategoryModel>(category);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CategoryModel Create(CategoryModel model)
        {
            string errorMessage = string.Empty;
            return Create(model, ref errorMessage);
        }

        public CategoryModel Delete(int id, ref string errorMessage)
        {
            Category category = db.Categories.Find(id);
            if (category == null) return null;
            if (!CanDelete(category, ref errorMessage)) return null;

            db.Categories.Remove(category);
            db.SaveChanges();

            return AutoMapper.Mapper.Map<CategoryModel>(category);
        }

        public CategoryModel Delete(int id)
        {
            string errorMessage = string.Empty;
            return Delete(id, ref errorMessage);

        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}