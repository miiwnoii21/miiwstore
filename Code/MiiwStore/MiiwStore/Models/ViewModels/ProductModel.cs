using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiiwStore.Models.ViewModels
{
    public class ProductModel
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string Category { get; set; }
        public string PicUrl { get; set; }
        public decimal Price { get; set; }
        public IEnumerable<ProductDetailModel> ProductDetails { get; set; }
    }
}