using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using MiiwStore.Models;

namespace MiiwStore.DAL
{
    public class StoreInitializer : DropCreateDatabaseIfModelChanges<StoreContext>
    {
        protected override void Seed(StoreContext context)
        {
            base.Seed(context);


            List<Product> productList = new List<Product>
            {
                new Product { ProductName = "Roomset:Davio", PicUrl = "http://sbmedia.sbdesignsquare.com/cyber/images/mat/59003225_ELI300212-RT-KC-Bedroom-Davio+Aubin-Modern_A_A1.jpg", ProductDetails = new List<ProductDetail>() },
                new Product { ProductName = "Roomset:Fabrica", PicUrl="http://sbmedia.sbdesignsquare.com/cyber/images/mat/59006523_ELI0-IT-BedroomFabrica6'Set1EspCLS%20GDOTTI-Bedroom-Fabrica-Modern%20Italian_A_A1.jpg", ProductDetails = new List<ProductDetail>() },
                new Product { ProductName = "Roomset:Econi", PicUrl="http://sbmedia.sbdesignsquare.com/cyber/images/mat/59007251_ELI0-IT-SB%20FURNITURE-Bedroom-Econi-Modern%20Italian_A_A1.jpg", ProductDetails = new List<ProductDetail>()},
                new Product { ProductName = "Roomset:Meudon", PicUrl="http://sbmedia.sbdesignsquare.com/cyber/images/mat/59002497_ELI300188-RT-KC-Bedroom-Meudon3.5&Davio-Contemporary_A_A1.jpg", ProductDetails = new List<ProductDetail>()},
                new Product { ProductName = "Roomset:Arty", PicUrl="http://sbmedia.sbdesignsquare.com/cyber/images/mat/59002497_ELI300188-RT-KC-Bedroom-Meudon3.5&Davio-Contemporary_A_A1.jpg", ProductDetails = new List<ProductDetail>()}
            };


            List<ProductDetail> prodDetailList = new List<ProductDetail> { 
                new ProductDetail {  ProductDetailDesc = "ตู้เสื้อผ้า", Price = 9800, Quantity = 10, PicUrl = "http://sbmedia.sbdesignsquare.com/cyber/images/mat/19060200_ELI45300271-KC-Bedroom-Wardrobe-Davio-Contemporary_A_A1.jpg" },
                new ProductDetail {  ProductDetailDesc = "ชั้นแขวน", Price = 2400, Quantity = 24, PicUrl = "http://sbmedia.sbdesignsquare.com/cyber/images/mat/19067483_PMT300742-IT-KC-Bedroomroom-Aubin-BS30-Contemporary_A_A1.jpg" }
            };

            prodDetailList.ForEach(pd => productList[0].ProductDetails.Add(pd));

            List<ProductDetail> prodDetailList1 = new List<ProductDetail>
            {
                new ProductDetail{ ProductDetailDesc="เตียง", Price=16600, Quantity= 2, PicUrl="http://sbmedia.sbdesignsquare.com/cyber/images/mat/19090727_ELI0-IT-sb-bed-fabica-modern_A_A1.jpg"},
                new ProductDetail{ ProductDetailDesc="ตู้เสื้อผ้า", Price=38000, Quantity= 24, PicUrl="http://sbmedia.sbdesignsquare.com/cyber/images/mat/19090729_ELI0-IT-sb-wardrobe-fabrica-modern_A_A1.jpg"},
                new ProductDetail{ProductDetailDesc="โต๊ะเครื่องแป้ง", Price=11600, Quantity= 20, PicUrl="http://sbmedia.sbdesignsquare.com/cyber/images/mat/19090730_ELI0-IT-sb-dressing%20table-fabrica-modern_A_A1.jpg"}
            };

            prodDetailList1.ForEach(pd => productList[1].ProductDetails.Add(pd));

            List<ProductDetail> prodDetailList2 = new List<ProductDetail>
            {
                new ProductDetail{ ProductDetailDesc="ตู้เสื้อผ้า", Price=35000, Quantity= 27, PicUrl="http://sbmedia.sbdesignsquare.com/cyber/images/mat/19096179_ELI0-IT-koncept-wardrobe-econi-modern_A_A1.jpg"}
            };

            prodDetailList2.ForEach(pd => productList[2].ProductDetails.Add(pd));

            List<ProductDetail> prodDetailList3 = new List<ProductDetail>
            {
                new ProductDetail{ ProductDetailDesc="เตียง", Price=7300, Quantity= 2, PicUrl="http://sbmedia.sbdesignsquare.com/cyber/images/mat/19042859_DEL45300352-KC-Bedroom-Meudon3.5-Bed-Contemporary_C_C1.jpg"},
                new ProductDetail{ ProductDetailDesc="โต๊ะเครื่องแป้ง", Price=5400, Quantity= 24, PicUrl="http://sbmedia.sbdesignsquare.com/cyber/images/mat/19042861_PMT3000243-IT-KC-Bedroom-Dt-Meudon-Contemporary_A_A1.jpg"},
                new ProductDetail{ ProductDetailDesc="ตู้เสื้อผ้า", Price=9800, Quantity= 20, PicUrl="http://sbmedia.sbdesignsquare.com/cyber/images/mat/19060200_ELI45300271-KC-Bedroom-Wardrobe-Davio-Contemporary_A_A1.jpg"}
            };

            prodDetailList3.ForEach(pd => productList[3].ProductDetails.Add(pd));

            List<ProductDetail> prodDetailList4 = new List<ProductDetail>
            {
                new ProductDetail{ ProductDetailDesc="เตียง", Price=8100, Quantity= 21, PicUrl="http://sbmedia.sbdesignsquare.com/cyber/images/mat/19110082_ELI0-IT-Koncept-Bed-Kid-Arty-Modern_A_A1.jpg"},
                new ProductDetail{ ProductDetailDesc="ตู้ข้างเตียง", Price=1900, Quantity= 24, PicUrl="http://sbmedia.sbdesignsquare.com/cyber/images/mat/19110083_ELI0-IT-Koncept-Night%20table-Arty-Modern_A_A1.jpg"},
                new ProductDetail{ ProductDetailDesc="กล่องแขวน", Price=2500, Quantity= 24, PicUrl="http://sbmedia.sbdesignsquare.com/cyber/images/mat/19110084_ELI0-IT-Koncept-Shelf-Arty-Modern_A_A1.jpg"},
                new ProductDetail{ ProductDetailDesc="ชั้นแขวน", Price=700, Quantity= 21, PicUrl="http://sbmedia.sbdesignsquare.com/cyber/images/mat/19110085_ELI0-IT-Koncept-Shelf-Arty-Modern_A_A1.jpg"},
                new ProductDetail{ ProductDetailDesc="อุปกรณ์การจัดเก็บ", Price=2800, Quantity= 24, PicUrl="http://sbmedia.sbdesignsquare.com/cyber/images/mat/19110087_ELI0-IT-Koncept-Book%20Shelf-Arty-Modern_A_A1.jpg"},
                new ProductDetail{ ProductDetailDesc="อุปกรณ์การจัดเก็บ", Price=3850, Quantity= 24, PicUrl="http://sbmedia.sbdesignsquare.com/cyber/images/mat/19110088_ELI0-IT-Koncept-Book%20Shelf-Arty-Modern_A_A1.jpg"},
                new ProductDetail{ ProductDetailDesc="ตู้เสื้อผ้า", Price=8700, Quantity= 20, PicUrl="http://sbmedia.sbdesignsquare.com/cyber/images/mat/19110089_ELI0-IT-Koncept-Wardrobe-Arty-Modern_A_A1.jpg"}
            };

            prodDetailList4.ForEach(pd => productList[4].ProductDetails.Add(pd));

            context.Products.AddRange(productList);

            //context.ProdDetails.AddRange(prodDetailList);
            //context.SaveChanges();

            List<User> userList = new List<User>
            {
                new User { FirstName = "Boonyarit", LastName = "Wiriyagulpat", Address = "BKK", BirthDate = DateTime.Parse("1983-02-10"), IsActive = false, Gender = "Male", UserName = "batman", Password = "batman001" },
                new User { FirstName = "Jutamas", LastName = "Kanthong", Address = "BKK", BirthDate = DateTime.Parse("1991-12-21"), IsActive = true, Gender = "Female", UserName = "miiwnoii", Password = "miiwnoii001" }
            };
            context.Users.AddRange(userList);

            List<Category> categoryList = new List<Category>
            {
               new Category{CategoryName="ห้องนอน"},
               new Category{CategoryName="ห้องครัว"},
               new Category{CategoryName="ครัวและห้องอาหาร"},
               new Category{CategoryName="ห้องนั่งเล่น"},
               new Category{CategoryName="โซฟา"},
               new Category{CategoryName="ของตกแต่งบ้าน"},
               new Category{CategoryName="ห้องเด็ก"},
               new Category{CategoryName="อื่นๆ"}
            };
            context.Categories.AddRange(categoryList);
            context.SaveChanges();

            List<CategoryProductDetail> categoryProductList = new List<CategoryProductDetail> { 
                new CategoryProductDetail{CategoryID=categoryList[0].CategoryID, ProductDetailID=prodDetailList[0].ProductDetailID},
                new CategoryProductDetail{CategoryID=categoryList[0].CategoryID, ProductDetailID=prodDetailList1[0].ProductDetailID},
                new CategoryProductDetail{CategoryID=categoryList[0].CategoryID, ProductDetailID=prodDetailList2[0].ProductDetailID},
                new CategoryProductDetail{CategoryID=categoryList[0].CategoryID, ProductDetailID=prodDetailList3[0].ProductDetailID},
                new CategoryProductDetail{CategoryID=categoryList[0].CategoryID, ProductDetailID=prodDetailList4[0].ProductDetailID}
            };
            context.CategoryProducts.AddRange(categoryProductList);

            context.SaveChanges();

        }
    }
}