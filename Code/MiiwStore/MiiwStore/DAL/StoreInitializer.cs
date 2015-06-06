using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using MiiwStore.Models;

namespace MiiwStore.DAL
{
    public class StoreInitializer : DropCreateDatabaseAlways<StoreContext>
    {
        protected override void Seed(StoreContext context)
        {
            base.Seed(context);

            List<Category> categoryList = new List<Category>
            {
               new Category { Name="ห้องนอน", 
                   SubCategories= new List<SubCategory> { 
                        new SubCategory{Name="กระจก"},
                        new SubCategory{Name="ตู้เสื้อผ้า"},
                        new SubCategory{Name="โต๊ะเครื่องแป้ง"},
                        new SubCategory{Name="สตูล"}
                    }},
               new Category{Name="ครัวและห้องอาหาร",
                   SubCategories=  new List<SubCategory>{
                        new SubCategory{Name="เก้าอี้ทานอาหาร"},
                        new SubCategory{Name="โต๊ะทานอาหาร"}
                    }},
               new Category{Name="ห้องนั่งเล่น",
                   SubCategories=  new List<SubCategory>{
                        new SubCategory{Name="ตู้แขวน"},
                        new SubCategory{Name="ชั้นแขวน"},
                        new SubCategory{Name="ตู้สูง"},
                        new SubCategory{Name="ตู้เตี้ย"},
                        new SubCategory{Name="ตู้วางทีวี"},
                        new SubCategory{Name="โซฟา"}
                    }},
               new Category{Name="ห้องน้ำ",
                   SubCategories=  new List<SubCategory>{
                        new SubCategory{Name="กระจก"},
                        new SubCategory{Name="ตู้เสื้อผ้า"},
                        new SubCategory{Name="โต๊ะเครื่องแป้ง"},
                        new SubCategory{Name="สตูล"}
                    }},
               new Category{Name="ของตกแต่งบ้าน",
                   SubCategories=  new List<SubCategory>{
                        new SubCategory{Name="กรอบรูป"},
                        new SubCategory{Name="โคมไฟ"},
                        new SubCategory{Name="นาฬิกา"},
                        new SubCategory{Name="ภาพแขวน"},
                        new SubCategory{Name="สตูล"},
                        new SubCategory{Name="หมอน"},
                        new SubCategory{Name="อุปกรณ์การจัดเก็บ"},
                        new SubCategory{Name="อุปกรณ์ตกแต่งสวน"}

                    }},
            };

            context.Categories.AddRange(categoryList);
            context.SaveChanges();

            List<Product> productList = new List<Product> { 
                new Product{ Name="ตู้วางทีวี: Ralphs", Image="http://sbmedia.sbdesignsquare.com/cyber/images/mat/19101643_NMI0-IT-Koncept-Sideboard-Ralphs-Contemporary_A_A2.jpg", Price=8000, Discount=30, 
                    SubCategoryID=categoryList.Single(e=>e.Name=="ห้องนั่งเล่น").SubCategories.Single(e=>e.Name=="ตู้วางทีวี").ID, 
                    Description="ไซด์บอร์ดราฟส์ 120 ซม.โดดเด่นด้วยลายไม้ธรรมชาติสีใหม่ \"ออทัมน์บราวน์\"", Size="120x55x60 cm.", Quantity=10},
                new Product{ Name="ตู้สูง: Ralphs", Image="http://sbmedia.sbdesignsquare.com/cyber/images/mat/19101643_NMI0-IT-Koncept-Sideboard-Ralphs-Contemporary_A_A2.jpg", Price=8000, Discount=30, 
                    SubCategoryID=categoryList.Single(e=>e.Name=="ห้องนั่งเล่น").SubCategories.Single(e=>e.Name=="ตู้สูง").ID, 
                    Description="ตู้สูงราฟส์ 60 ซม. สไตล์คอนเทมโพรารี่ โดดเด่นด้วยลายไม้ธรรมชาติสีใหม่ \"ออทัมน์บราวน์\" ตัดกับสีเทาดำ", Size="60x35x185 cm.", Quantity=20},
                new Product{ Name="อุปกรณ์การจัดเก็บ: Maximus", Image="http://sbmedia.sbdesignsquare.com/cyber/images/mat/19045723_NMI300094-IT-KC-Livingroom-Maximus-WC030DHO-Modernl_A_A2.jpg", Price=2600, Discount=30, 
                    SubCategoryID=categoryList.Single(e=>e.Name=="ของตกแต่งบ้าน").SubCategories.Single(e=>e.Name=="อุปกรณ์การจัดเก็บ").ID, 
                    Description="ตู้แขวนบานเปิด ขนาด 30 cm พ่นสี PHG ไฮ-กรอส ทันสมัยเข้ากับไลฟ์สไตล์ของคนรุ่นใหม่", Size="60x35x185 cm.", Quantity=20},
                new Product{ Name="ตู้วางทีวี: Maximus", Image="http://sbmedia.sbdesignsquare.com/cyber/images/mat/19101643_NMI0-IT-Koncept-Sideboard-Ralphs-Contemporary_A_A2.jpg", Price=7900, Discount=30, 
                    SubCategoryID=categoryList.Single(e=>e.Name=="ห้องนั่งเล่น").SubCategories.Single(e=>e.Name=="ตู้วางทีวี").ID, 
                    Description="ไซด์บอร์ด 180 cm พร้อมลิ้นชักตัดสีด้วย PHG ไฮ-กรอสผสมผสานกับลายไม้ธรรมชาติ ทันสมัยเข้ากับไลฟ์สไตล์ของคนรุ่นใหม่", Size="180x60x45 cm.", Quantity=20}
            };


            context.Products.AddRange(productList);
            context.SaveChanges();


            //var parent = new
            //{
            //    id = 1,
            //    name = "parent",
            //    child = new[]{
            //    new{
            //        id=1,
            //        name="child1"

            //    },
            //    new{
            //        id=2,
            //        name="child2"
            //    },
            //    new{
            //        id=3,
            //        name="child3"
            //    }
            //}
            //};


            List<User> userList = new List<User>
            {
                new User { FirstName = "Boonyarit", LastName = "Wiriyagulpat", Address = "BKK", BirthDate = DateTime.Parse("1983-02-10"), IsActive = false, Gender = "Male", UserName = "batman", Password = "batman001" },
                new User { FirstName = "Jutamas", LastName = "Kanthong", Address = "BKK", BirthDate = DateTime.Parse("1991-12-21"), IsActive = true, Gender = "Female", UserName = "miiwnoii", Password = "miiwnoii001" }
            };
            context.Users.AddRange(userList);
            context.SaveChanges();



        }
    }
}