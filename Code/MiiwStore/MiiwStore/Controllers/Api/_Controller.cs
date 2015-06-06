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

        private ProductListModel Mapper(ProductSet product)
        {

            decimal productPrice = 0;
            foreach (var item in product.ProductDetails)
            {
                productPrice += item.Price;
            }
            var prodModel = new ProductListModel
            {
                ID = product.ProductID,
                Name = product.ProductName,
                Price = productPrice,
                PicUrl = product.PicUrl
            };

            var prodChild = product.ProductDetails.FirstOrDefault();

            if (prodChild != null)
            {
                var category = prodChild.CategoryProductDetails.FirstOrDefault();
                if (category != null)
                {
                    prodModel.Category = category.Category.CategoryName;
                }
            }

            //prodModel.Category = (product.ProductDetails.FirstOrDefault()!=null 
            //                        && product.ProductDetails.FirstOrDefault().CategoryProductDetails.FirstOrDefault() != null) 
            //                        ? product.ProductDetails.FirstOrDefault().CategoryProductDetails.FirstOrDefault().Category.CategoryName 
            //                        : string.Empty;

            return prodModel;

        }

        #region Get Methods

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
        [ActionName("ProductById")]
        [ResponseType(typeof(ProductModel))]
        public IHttpActionResult GetProductById(int id)
        {
            ProductSet product = db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(AutoMapper.Mapper.Map<ProductModel>(product));
        }

        [ActionName("Search")]
        public IHttpActionResult GetProductByProductName(string productName)
        {
            List<ProductListModel> productListModels = new List<ProductListModel>();
            db.Products.Where(pd => pd.ProductName.Contains(productName)).ToList().ForEach(p=>productListModels.Add(Mapper(p)));
            //db.Products.Where(pd => pd.ProductName.Contains(productName)).ToList().ForEach(p => productListModels.Add(AutoMapper.Mapper.Map<ProductListModel>(p)));
            // return Ok(productListModels);
            return Ok(db.Products.Where(pd => pd.ProductName.Contains(productName)).ToList().Select(AutoMapper.Mapper.Map<ProductListModel>));
        }
        #endregion

        // PUT: api/Products/5
        //[Route("api/products/detail")]
        [ActionName("UpdateDetail")]
        public IHttpActionResult PutProductDetail(ProductDetailModel productDetailModel)
        {
            string failedList = string.Empty;

            if (!IsValid(productDetailModel, ref failedList, false)) return BadRequest(failedList);

            try
            {
                // ProductDetail productDetail = AutoMapper.Mapper.Map<ProductDetail>(productDetailModel);
                // db.Entry(productDetail).State = EntityState.Modified;
                Product productDetail = db.ProductDetails.Find(productDetailModel.ProductDetailID);

                if (productDetail == null) return NotFound();

                productDetail.ProductDetailDesc = productDetailModel.ProductDetailDesc;
                productDetail.Price = productDetailModel.Price;
                productDetail.Image = productDetailModel.PicURL;
                productDetail.Quantity = productDetailModel.Quantity;
                if (productDetailModel.ProductID.HasValue && productDetailModel.ProductID.Value > 0)
                    productDetail.ProductID = productDetailModel.ProductID;

                db.SaveChanges();

                return Ok(AutoMapper.Mapper.Map<ProductDetailModel>(productDetail));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

           
        }

        // POST: api/Products/Details
        //[Route("api/products/detail")]
        [ActionName("CreateDetail")]
        [ResponseType(typeof(ProductDetailModel))]
        public IHttpActionResult PostProductDetail(ProductDetailModel productDetailModel)
        {
            string failedList = string.Empty;
            if (!IsValid(productDetailModel, ref failedList)) return BadRequest(failedList);

            try
            {
                Product productDetail = db.ProductDetails.Add(AutoMapper.Mapper.Map<Product>(productDetailModel));
                db.SaveChanges();

                return Ok(AutoMapper.Mapper.Map<ProductDetailModel>(productDetail));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

        private bool IsValid(ProductDetailModel productDetailModel, ref string failedList, bool isCreate = true)
        {
            List<string> failList = new List<string>();


            if (productDetailModel != null)
            {

                if (!string.IsNullOrEmpty(productDetailModel.ProductDetailDesc)
                    && !string.IsNullOrEmpty(productDetailModel.PicURL)
                    && productDetailModel.Price > 0
                    && (isCreate || (productDetailModel.ProductDetailID > 0
                    /*&& db.ProductDetails.Count(p => p.ProductDetailID == productDetailModel.ProductDetailID) >= 1*/)))
                    //&& db.ProductDetails.Find(productDetailModel.ProductDetailID) != null))) //product id ไม่มีจริงๆ ส่งproduct id มั่วๆมา
                {
                    
                    return true;
                }
                else
                {
                    if (!isCreate && productDetailModel.ProductDetailID <= 0)
                    {
                        failList.Add("Invalid product detail id");
                    }
                    if (string.IsNullOrEmpty(productDetailModel.ProductDetailDesc))
                    {
                        failList.Add("Please Insert Description");
                    }
                    if (string.IsNullOrEmpty(productDetailModel.PicURL))
                    {
                        failList.Add("Please Insert PicUrl");
                    }
                    if (productDetailModel.Price <= 0)
                    {
                        failList.Add("Please Insert Price");
                    }

                    failedList = string.Join(", ", failList.ToArray());
                }

            }
            else
            {
                failedList = "Invalid model";
            }

            return false;
        }

        private bool IsValid(ProductModel model, ref string failedString, bool isCreate = true)
        {
            List<string> failedList = new List<string>();
            if (model == null)
            {
                failedString = "Invalid model";
            }
            else
            {
                if((!isCreate || model.ProductID > 0) 
                    && !string.IsNullOrEmpty(model.ProductName)
                    && !string.IsNullOrEmpty(model.PicUrl)
                    && model.ProductDetails != null
                    && model.ProductDetails.Count()>0)
                {
                    return true;
                }
                else
                {
                    if (!isCreate && model.ProductID <= 0)
                    {
                        failedList.Add("Invalid product id");
                    }
                    if (string.IsNullOrEmpty(model.ProductName))
                    {
                        failedList.Add("Please insert name");
                    }
                    if (string.IsNullOrEmpty(model.PicUrl))
                    {
                        failedList.Add("Please insert PicUrl");
                    }

                }

                failedString = string.Join(", ", failedList.ToArray());
                
            }

            return true;
        }

        //POST: api/Products/product
        [ActionName("CreateProduct")]
        public IHttpActionResult PostProduct(ProductModel model)
        {
            string failedList = string.Empty;

            if (!IsValid(model, ref failedList)) return BadRequest(failedList);

            try
            {
                ProductSet product = db.Products.Add(AutoMapper.Mapper.Map<ProductSet>(model));
                //List<ProductDetail> productDetail = new List<ProductDetail>();
                //foreach (var item in model.ProductDetails)
                //{
                //    productDetail.Add(AutoMapper.Mapper.Map<ProductDetail>(item));
                //}
                ////model.ProductDetails = null;
                //Product product = AutoMapper.Mapper.Map<Product>(model);

                db.SaveChanges();
                return Ok(AutoMapper.Mapper.Map<ProductModel>(product));
                
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

        

        // DELETE: api/Products/5
        [ResponseType(typeof(ProductSet))]
        public IHttpActionResult DeleteProduct(int id)
        {
            ProductSet product = db.Products.Find(id);
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