using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiiwStore.Models.ViewModels
{
    public class ProductModel
    {
        public int ProductID { get; set; }
        public string ProdName { get; set; }

        public virtual ICollection<ProductDetailModel> ProductDetails { get; set; }
    }
}