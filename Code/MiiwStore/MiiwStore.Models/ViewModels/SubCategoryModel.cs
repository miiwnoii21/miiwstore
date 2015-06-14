using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiiwStore.Models.ViewModels
{
    public class SubCategoryModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public int CategoryID { get; set; }
        public bool IsInUse { get; set; }
        public bool IsDelete { get; set; }

        //public SubCategoryModel(SubCategory data)
        //{
        //    ID = data.ID;
        //    Name = data.Name;
        //    FullName = string.Format("{0} : {1}", data.Category.Name, data.Name);
        //    CategoryID = data.CategoryID;
        //    IsInUse = (data.Products.Count > 0);
        //}
    }
}