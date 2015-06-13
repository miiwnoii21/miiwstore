using MiiwStore.DAL;
using MiiwStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiiwStore.Services
{
    public class CategoryService : IDisposable
    {
        private readonly StoreContext db;

        public CategoryService()
        {
            db = new StoreContext();
        }

        public IEnumerable<SubCategoryModel> GetSubCategories()
        {
            List<SubCategoryModel> models = new List<SubCategoryModel>();
            db.SubCategories.ToList().ForEach(s => models.Add(AutoMapper.Mapper.Map<SubCategoryModel>(s)));
            return models;

            //return db.SubCategories.ToList().Select(s => AutoMapper.Mapper.Map<SubCategoryModel>(s));
            //return db.SubCategories.ToList().Select(AutoMapper.Mapper.Map<SubCategoryModel>);

        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}