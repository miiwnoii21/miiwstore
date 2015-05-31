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
            AutoMapper.Mapper.CreateMap<Product, ProductListModel>()
                .ForMember(dest => dest.Price,
                            opt => opt.MapFrom(src => src.ProductDetails.Sum(pd => pd.Price)));



            AutoMapper.Mapper.CreateMap<ProductDetail, ProductDetailModel>();
        }
    }
}