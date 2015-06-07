using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiiwStore.Models.ViewModels
{
    public class OrderListModel
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public string CustomerName { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ShipDate { get; set; }
    }
}