using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiiwStore.Models.ViewModels
{
    public class OrderDetailModel
    {
        public int ID { get; set; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public int Amount { get; set; }
    }
}