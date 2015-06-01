using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiiwStore.Models.ViewModels
{
    public class ProductDetailModel
    {
        public int ProductDetailID { get; set; }
        public string ProductDetailDesc { get; set; }
        public int? ProductID { get; set; }
        public string PicURL { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
    }
}