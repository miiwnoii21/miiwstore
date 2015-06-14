using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MiiwStore.Models
{
    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public int Discount { get; set; }
        public int SubCategoryID { get; set; }
        public string Description { get; set; }
        public string Size { get; set; }
        public int Quantity { get; set; }

        public virtual SubCategory SubCategory { get; set; }
    }
}