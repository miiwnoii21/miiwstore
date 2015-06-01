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
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.ProductDetails.Sum(pd => pd.Price)))
                .ForMember(dest => dest.Category, opt =>
                    opt.MapFrom(src =>
                        ((src.ProductDetails.FirstOrDefault() != null)
                        && (src.ProductDetails.FirstOrDefault().CategoryProductDetails.FirstOrDefault() != null)
                        ? src.ProductDetails.FirstOrDefault().CategoryProductDetails.FirstOrDefault().Category.CategoryName
                        : string.Empty))
                );
            //.AfterMap((src, dest) =>
            //{
            //    var prodChild = src.ProductDetails.FirstOrDefault();

            //    if (prodChild != null)
            //    {
            //        var category = prodChild.CategoryProductDetails.FirstOrDefault();
            //        if (category != null)
            //        {
            //            dest.Category = category.Category.CategoryName;
            //        }
            //    }
            //})
            //;

            AutoMapper.Mapper.CreateMap<Product, ProductModel>()
               .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.ProductDetails.Sum(pd => pd.Price)))
               .ForMember(dest => dest.Category, opt =>
                   opt.MapFrom(src =>
                       ((src.ProductDetails.FirstOrDefault() != null)
                       && (src.ProductDetails.FirstOrDefault().CategoryProductDetails.FirstOrDefault() != null)
                       ? src.ProductDetails.FirstOrDefault().CategoryProductDetails.FirstOrDefault().Category.CategoryName
                       : string.Empty))
               );

            AutoMapper.Mapper.CreateMap<ProductDetail, ProductDetailModel>();
            AutoMapper.Mapper.CreateMap<ProductDetailModel, ProductDetail>()
                .ForMember(dest => dest.ProductID, opt => 
                    opt.MapFrom(src => (src.ProductID.HasValue && src.ProductID.Value > 0) ? src.ProductID : null))
                ;
        }
    }
}