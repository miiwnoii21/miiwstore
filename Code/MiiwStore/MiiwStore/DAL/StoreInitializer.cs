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
                new Product { ProductName = "เตียง", PicUrl = "http://www.ikea.com/ms/media/seorange/20144/20144_beca01a_foam_latex_mattresses_PH030651.jpg", ProductDetails = new List<ProductDetail>() },
                new Product { ProductName = "ที่เก็บของ", PicUrl="http://www.ikea.com/th/th/images/products/phaks-tu-seux-pha__0312602_PE405357_S4.JPG", ProductDetails = new List<ProductDetail>() },
                new Product { ProductName = "หลอดไฟและโคมไฟ", PicUrl="http://www.ikea.com/th/th/images/products/ni-mex-poa-khom-da__0275911_PE414015_S4.JPG", ProductDetails = new List<ProductDetail>()},
                new Product { ProductName = "กระจกเงา", PicUrl="http://www.ikea.com/th/th/images/products/hexnefxs-krack-ngea__0386408_PH123967_S4.JPG", ProductDetails = new List<ProductDetail>() }
            };


            List<ProductDetail> prodDetailList = new List<ProductDetail> { 
                new ProductDetail {  ProductDetailDesc = "ที่นอน", Price = 2000, Quantity = 10, PicUrl = "http://www.ikea.com/ms/media/seorange/20144/20144_beca01a_foam_latex_mattresses_PH030651.jpg" },
                new ProductDetail {  ProductDetailDesc = "ผ้าปูที่นอน", Price = 2000, Quantity = 24, PicUrl = "http://www.ikea.com/th/th/images/products/kespa-pha-pu-thi-nxn-rad-mum-si-thexrkhwxys__0257005_PE401203_S4.JPG" },
                new ProductDetail {  ProductDetailDesc = "หมอน", Price = 2000, Quantity = 27, PicUrl = "http://www.ikea.com/ms/media/seorange/20151/20151_txca04a_pillows_PH121085.jpg" },
                new ProductDetail {  ProductDetailDesc = "ผ้านวม", Price = 2000, Quantity = 12, PicUrl = "http://www.ikea.com/ms/media/seorange/20151/20151_txca05a_quilts_PH121061.jpg" }
            };

            prodDetailList.ForEach(pd => productList[0].ProductDetails.Add(pd));

            List<ProductDetail> prodDetailList1 = new List<ProductDetail>
            {
                new ProductDetail{ ProductDetailDesc="ตู้เสื้อผ้า", Price=31320, Quantity= 2, PicUrl="http://www.ikea.com/th/th/images/products/phaks-tu-seux-pha__0312602_PE405357_S4.JPG"},
                new ProductDetail{ ProductDetailDesc="ตู้ลิ้นชัก", Price=6490, Quantity= 24, PicUrl="http://www.ikea.com/th/th/images/products/malm-tu-lin-chak__0246140_PE385290_S4.JPG"},
                new ProductDetail{ProductDetailDesc="โต๊ะข้างเตียง", Price=1690, Quantity= 20, PicUrl="http://www.ikea.com/th/th/images/products/tharfwa-toa-khang-teiyng__0143743_PE303246_S4.JPG"},
                new ProductDetail{ProductDetailDesc="หัวเตียง", Price=4990, Quantity= 16, PicUrl="http://www.ikea.com/th/th/images/products/brimnes-haw-teiyng-phrxm-chxng-keb-khxng-khaw__0107521_PE257203_S4.JPG"}
            };

            prodDetailList1.ForEach(pd => productList[1].ProductDetails.Add(pd));

            List<ProductDetail> prodDetailList2 = new List<ProductDetail>
            {
                new ProductDetail{ ProductDetailDesc="โป๊ะโคม", Price=790, Quantity= 27, PicUrl="http://www.ikea.com/th/th/images/products/ni-mex-poa-khom-da__0275911_PE414015_S4.JPG"},
                new ProductDetail{ ProductDetailDesc="โคมไฟผนัง", Price=2290, Quantity= 20, PicUrl="http://www.ikea.com/th/th/images/products/malm-tu-lin-chak__0246140_PE385290_S4.JPG"},
                new ProductDetail{ProductDetailDesc="โคมไฟติดเพดาน", Price=3990, Quantity= 14, PicUrl="http://www.ikea.com/th/th/images/products/xikeiy-phi-xes-maskhrus-khom-kh-wn-phedan__0091262_PE226703_S4.JPG"},
                new ProductDetail{ProductDetailDesc="โคมไฟตั้งพื้น", Price=2590, Quantity= 28, PicUrl="http://www.ikea.com/th/th/images/products/hekthar-khom-fi-tang-phun__0149974_PE308131_S4.JPG"}
            };

            prodDetailList2.ForEach(pd => productList[2].ProductDetails.Add(pd));

            List<ProductDetail> prodDetailList3 = new List<ProductDetail>
            {
                new ProductDetail{ ProductDetailDesc="กระจกติดผนัง", Price=399, Quantity= 20, PicUrl="http://www.ikea.com/th/th/images/products/hexnefxs-krack-ngea__0386408_PH123967_S4.JPG"},
                new ProductDetail{ ProductDetailDesc="กระจกตั้งพื้น", Price=2290, Quantity= 7, PicUrl="http://www.ikea.com/th/th/images/products/khnapper-krack-tang-phun-khaw__0384015_PH122694_S4.JPG"},
                new ProductDetail{ProductDetailDesc="ตู้กระจกเก็บของ", Price=2690, Quantity= 18, PicUrl="http://www.ikea.com/th/th/images/products/brimnes-tu-krack-keb-khxng-khaw__0173223_PE327307_S4.JPG"},
                new ProductDetail{ProductDetailDesc="กระจกเงา", Price=2697, Quantity= 23, PicUrl="http://www.ikea.com/th/th/images/products/stxwe-krack-ngea__0096576_PE237918_S4.JPG"}
            };

            prodDetailList3.ForEach(pd => productList[3].ProductDetails.Add(pd));

            context.Products.AddRange(productList);
            context.SaveChanges();

            //context.ProdDetails.AddRange(prodDetailList);
            //context.SaveChanges();

            List<User> userList = new List<User>{
                new User { FirstName = "Boonyarit", LastName = "Wiriyagulpat", Address = "BKK", BirthDate = DateTime.Parse("1983-02-10"), IsActive = false, Gender = "Male", UserName = "batman", Password = "batman001" },
                new User { FirstName = "Jutamas", LastName = "Kanthong", Address = "BKK", BirthDate = DateTime.Parse("1991-12-21"), IsActive = true, Gender = "Female", UserName = "miiwnoii", Password = "miiwnoii001" }
            };
            context.Users.AddRange(userList);
            context.SaveChanges();

        }
    }
}