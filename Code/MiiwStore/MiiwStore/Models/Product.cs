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
        public int ProductID { get; set; }
        public string ProdName { get; set;}

        public virtual ICollection<ProdDetail> ProdDetails { get; set; }
        public virtual ICollection<CategoryProduct> CategoryProducts { get; set; }
    }
}