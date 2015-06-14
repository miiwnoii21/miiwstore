using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiiwStore.Models.ViewModels
{
    public class CategoryModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public IEnumerable<SubCategoryModel> SubCategories { get; set; }
        //public IEnumerable<System.Web.Mvc.SelectListItem> SubCatList
        //{
        //    get
        //    {
        //        return SubCategories.Select(e => new System.Web.Mvc.SelectListItem { Value = e.ID.ToString(), Text = e.Name });
        //    }
        //}
    }
}