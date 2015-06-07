using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using MiiwStore.DAL;
using MiiwStore.Models;
using MiiwStore.Models.ViewModels;

namespace MiiwStore.Controllers.Api
{
    [RoutePrefix("api/orders")]
    public class OrdersController : ApiController
    {
        private StoreContext db = new StoreContext();

        private bool IsValid(OrderModel model, ref string errorMessage, bool isCreate = true)
        {
            List<string> failList = new List<string>();


            if (model != null)
            {

                if (!isCreate && db.Orders.Find(model.ID) == null)
                {
                    failList.Add("Invalid order id");
                }
                if (isCreate && db.Orders.Find(model.ID) != null)
                {
                    failList.Add("Duplicate order id");
                }
                if (db.Users.Find(model.UserID) == null)
                {
                    failList.Add("Invalid user id");
                }
                if (model.ShipDate == null || model.ShipDate == DateTime.MinValue)
                {
                    failList.Add("Please insert ship date");
                }
                if (model.OrderDetails.Count() == 0)
                {
                    failList.Add("Please insert order detail");
                }
                else
                {
                    //IEnumerable<OrderDetailModel> orderDetails = 
                    model.OrderDetails.All(od =>
                    {
                        //if (!isCreate && db.OrderDetails.Find(od.ID) == null)
                        //{
                        //    failList.Add("Invalid order detail id");
                        //    return false;
                        //}
                        //if (isCreate && db.OrderDetails.Find(od.ID) != null)
                        //{
                        //    failList.Add("Duplicate order detail id");
                        //    return false;
                        //}
                        //if (!isCreate && model.ID != od.OrderID)
                        //{
                        //    failList.Add("Order id is not match with parent");
                        //    return false;
                        //}
                        if (db.Products.Find(od.ProductID) == null)
                        {
                            failList.Add("Invalid product id");
                            return false;
                        }
                        if (od.Amount <= 0)
                        {
                            failList.Add("Please insert amount");
                            return false;
                        }
                        return true;

                    });
                }
                if (failList.Count > 0)
                {
                    errorMessage = string.Join(", ", failList.ToArray());
                    return false;
                }

                return true;
            }
            else
            {
                errorMessage = "Invalid model";
                return false;
            }

        }

        // GET: api/Orders
        [ResponseType(typeof(OrderListModel))]
        public IHttpActionResult GetOrders()
        {
            List<Order> orders = db.Orders.ToList();
            if (orders.Count == 0)
            {
                return NotFound();
            }

            return Ok(orders.Select(s => AutoMapper.Mapper.Map<OrderListModel>(s)));
        }

        // GET: api/Orders/5
        [ResponseType(typeof(OrderListModel))]
        public IHttpActionResult GetOrder(int id)
        {
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(AutoMapper.Mapper.Map<OrderListModel>(order));
        }

        //GET: api/Orders
        [Route("search")]
        public IHttpActionResult GetOrder(string customerName = "", string orderDate = "", string shipDate = "", string productName = "")
        {

            DateTime orderDateTry, shipDateTry;
            bool hasOrderDate = DateTime.TryParse(orderDate, out orderDateTry);
            bool hasShipDate = DateTime.TryParse(shipDate, out shipDateTry);

            IEnumerable<Order> orders = db.Orders.ToList().Where(o =>
            {
                return (string.IsNullOrEmpty(customerName) || string.Format("{0} {1}", o.User.FirstName, o.User.LastName).ToLowerInvariant().Contains(customerName.ToLowerInvariant()))
                    //&& (string.IsNullOrEmpty(orderDate) || DateTime.Parse(orderDate, new System.Globalization.CultureInfo("en-US")) == o.OrderDate.Date)
                 && (string.IsNullOrEmpty(orderDate) || orderDateTry == o.OrderDate.Date)
                 && (string.IsNullOrEmpty(shipDate) || shipDateTry == o.OrderDate.Date)
                 && (string.IsNullOrEmpty(productName) || o.OrderDetails.Any(od => od.Product.Name.ToLowerInvariant().Contains(productName.ToLowerInvariant())));

            });

            if (orders.Count() == 0)
            {
                return NotFound();
            }

            return Ok(orders.Select(o => AutoMapper.Mapper.Map<OrderListModel>(o)));
        }

        // PUT: api/Orders/5
        [ResponseType(typeof(OrderModel))]
        public IHttpActionResult PutOrder(OrderModel model)
        {
            string errorMessage = string.Empty;
            if (!IsValid(model, ref errorMessage, false))
            {
                return BadRequest(errorMessage);
            }

            try
            {
                Order order = db.Orders.Find(model.ID);
                order.ShipDate = model.ShipDate;
                order.OrderDetails.Clear();
                order.OrderDetails = new List<OrderDetail>(model.OrderDetails.Select(AutoMapper.Mapper.Map<OrderDetail>));

                //var details = model.OrderDetails.Select(AutoMapper.Mapper.Map<OrderDetail>);
                //details.All(dt =>
                //{
                //    order.OrderDetails.Add(dt);
                //    return true;
                //});


                db.SaveChanges();

                return Ok(AutoMapper.Mapper.Map<OrderModel>(order));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST: api/Orders
        [ResponseType(typeof(Order))]
        public IHttpActionResult PostOrder(Order order)
        {

            db.Orders.Add(order);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = order.ID }, order);
        }

        // DELETE: api/Orders/5
        [ResponseType(typeof(Order))]
        public IHttpActionResult DeleteOrder(int id)
        {
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return NotFound();
            }

            db.Orders.Remove(order);
            db.SaveChanges();

            return Ok(order);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrderExists(int id)
        {
            return db.Orders.Count(e => e.ID == id) > 0;
        }
    }
}