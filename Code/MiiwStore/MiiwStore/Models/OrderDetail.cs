using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MiiwStore.Models
{
  
    public class OrderDetail
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int OrderID { get; set; }
       /// <summary>
       /// RefID = ProductID or ProductSetID
       /// </summary>
        public int RefID { get; set; }
        public int Amount { get; set; }
        public ProductType ProductType { get; set; }

        public virtual Order Order { get; set; }
    }
}