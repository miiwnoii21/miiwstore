using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MiiwStore.Models
{
    public class ProductDetail
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductDetailID { get; set; }
        public string ProductDetailDesc { get; set; }
        public int ProductID { get; set; }
        public string PicUrl { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }

        public virtual Product Product { get; set; }
    }
}