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
                .ForMember(d => d.SubCategoryName, o => o.MapFrom(s => s.SubCategory.Name));
            AutoMapper.Mapper.CreateMap<ProductModel, Product>();

            AutoMapper.Mapper.CreateMap<User, UserListModel>()
                .ForMember(d => d.HasOrder, o => o.MapFrom(s => (s.Orders != null && s.Orders.Any(ord => ord.ShipDate > DateTime.Now.Date))));
            AutoMapper.Mapper.CreateMap<UserModel, User>();

            AutoMapper.Mapper.CreateMap<Order,OrderListModel>()
                .ForMember(d => d.CustomerName, o => o.MapFrom(s => string.Format("{0} {1}", s.User.FirstName, s.User.LastName)));
            AutoMapper.Mapper.CreateMap<Order, OrderModel>();
            AutoMapper.Mapper.CreateMap<OrderModel, Order>();
            AutoMapper.Mapper.CreateMap<OrderDetail, OrderDetailModel>()
                .ForMember(d => d.ProductName, o => o.MapFrom(s => s.Product.Name));
            AutoMapper.Mapper.CreateMap<OrderDetailModel, OrderDetail>();

            AutoMapper.Mapper.CreateMap<Category, CategoryModel>();
            AutoMapper.Mapper.CreateMap<CategoryModel, Category>();
            AutoMapper.Mapper.CreateMap<SubCategory, SubCategoryModel>()
                .ForMember(d => d.IsInUse, o => o.MapFrom(s => s.Products.Count > 0));
            AutoMapper.Mapper.CreateMap<SubCategoryModel, SubCategory>();
        }
    }
}