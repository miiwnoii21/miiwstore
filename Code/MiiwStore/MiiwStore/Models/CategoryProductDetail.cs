using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MiiwStore.Models
{
    public class CategoryProductDetail
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int CategoryID { get; set; }
        public int ProductDetailID { get; set; }

        public virtual Category Category { get; set; }
        public virtual ProductDetail ProductDetail { get; set; }
    }
}