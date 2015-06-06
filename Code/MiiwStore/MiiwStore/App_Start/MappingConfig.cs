using MiiwStore.Models;
using MiiwStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiiwStore
{
    public class MappingConfig
    {
        internal static void RegisterMapping()
        {
            AutoMapper.Mapper.CreateMap<Product, ProductModel>()
                .ForMember(d => d.DiscountPrice, o => o.MapFrom(s => s.Price * (100 - s.Discount) / 100))
                .ForMember(d => d.SubCategory, o => o.MapFrom(s => s.SubCategory.Name));

            AutoMapper.Mapper.CreateMap<ProductModel, Product>();

        }
    }
}