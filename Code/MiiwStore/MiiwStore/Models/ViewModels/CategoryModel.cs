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
    }
}