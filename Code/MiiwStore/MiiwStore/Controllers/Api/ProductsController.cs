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
            var prodModel = new ProductListModel
            {
                ProductID = product.ProductID,
                ProductName = product.ProductName,
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
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(AutoMapper.Mapper.Map<ProductModel>(product));
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
                ProductDetail productDetail = db.ProductDetails.Find(productDetailModel.ProductDetailID);

                if (productDetail == null) return NotFound();

                productDetail.ProductDetailDesc = productDetailModel.ProductDetailDesc;
                productDetail.Price = productDetailModel.Price;
                productDetail.PicUrl = productDetailModel.PicURL;
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
                ProductDetail productDetail = db.ProductDetails.Add(AutoMapper.Mapper.Map<ProductDetail>(productDetailModel));
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
                    && (isCreate || productDetailModel.ProductDetailID > 0))
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
                        failList.Add("Please Insert PicURL");
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