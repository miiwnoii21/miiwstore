using MiiwStore.DAL;
using MiiwStore.Models;
using MiiwStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiiwStore.Services
{
    public class ProductService : IDisposable
    {
        private readonly StoreContext db;

        public ProductService()
        {
            db = new StoreContext();
        }

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
                if (model.Quantity <= 0)
                {
                    failList.Add("Please Insert Quantity");
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
        public IEnumerable<ProductModel> GetProducts()
        {
            List<Product> products = db.Products.ToList();
            if (products.Count == 0)
            {
                return null;
            }

            //List<ProductListModel> list = new List<ProductListModel>();

            //products.ForEach(s => list.Add(AutoMapper.Mapper.Map<ProductListModel>(s)));

            //return Ok(list);

            //return Ok(products.Select(s => AutoMapper.Mapper.Map<ProductListModel>(s)));

            //return Ok(AutoMapper.Mapper.Map<List<ProductListModel>>(products));
            return products.Select(AutoMapper.Mapper.Map<ProductModel>);
        }

        public ProductModel GetById(int id)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return null;
            }

            return AutoMapper.Mapper.Map<ProductModel>(product);
        }

        public IEnumerable<ProductModel> Search(string name = "", string category = "", string subCategory = "")
        {
            //bool chkName = (!string.IsNullOrEmpty(name) && contains(name));
            // select * from table1 where ( ( name is null || table1.name like '%name%') and ( category is null || table1.category like '%cat%')
            IEnumerable<Product> products = db.Products.ToList().Where(s =>
            {

                return ((string.IsNullOrEmpty(name) || s.Name.ToLowerInvariant().Contains(name.ToLowerInvariant()))
                && (string.IsNullOrEmpty(subCategory) || s.SubCategory.Name.ToLowerInvariant().Contains(subCategory.ToLowerInvariant()))
                && (string.IsNullOrEmpty(category) || s.SubCategory.Category.Name.ToLowerInvariant().Contains(category.ToLowerInvariant())));

            });

            if (products.Count() == 0)
            {
                return null;
            }

            return products.Select(s => AutoMapper.Mapper.Map<ProductModel>(s));
        }

        public ProductModel Update(ProductModel model, ref string errorMessage)
        {
            //string errorMessage = string.Empty;

            if (!IsValid(model, ref errorMessage, false))
            {
                return null;
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
                product.Quantity = model.Quantity;
                product.Size = model.Size;
                db.SaveChanges();

                return AutoMapper.Mapper.Map<ProductModel>(product);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ProductModel Update(ProductModel model)
        {
            string errorMessage = string.Empty;
            return Update(model, ref errorMessage);
        }

        public ProductModel Create(ProductModel model, ref string errorMessage)
        {

            if (!IsValid(model, ref errorMessage))
            {
                return null;
            }

            try
            {
                Product product = db.Products.Add(AutoMapper.Mapper.Map<Product>(model));
                db.SaveChanges();

                return AutoMapper.Mapper.Map<ProductModel>(product);

                // return Ok(AutoMapper.Mapper.Map<ProductModel>(product));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ProductModel Create(ProductModel model)
        {

            string errorMessage = string.Empty;
            return Create(model, ref errorMessage);
        }


        public ProductModel Delete(int id)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return null;
            }

            db.Products.Remove(product);
            db.SaveChanges();

            return AutoMapper.Mapper.Map<ProductModel>(product);
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}