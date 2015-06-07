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
    [RoutePrefix("api/users")]
    public class UsersController : ApiController
    {
        private StoreContext db = new StoreContext();

        private bool IsValid(UserModel model, ref string errorMessage, bool isCreate = true)
        {
            List<string> failList = new List<string>();


            if (model != null)
            {

                if (!isCreate && db.Users.Find(model.ID) == null)
                {
                    failList.Add("Invalid user id");
                }
                if (isCreate && db.Users.Find(model.ID) != null)
                {
                    failList.Add("Duplicate user id");
                }
                if (string.IsNullOrEmpty(model.FirstName))
                {
                    failList.Add("Please insert first name");
                }
                if (string.IsNullOrEmpty(model.LastName))
                {
                    failList.Add("Please insert last name");
                }
                if (string.IsNullOrEmpty(model.Address))
                {
                    failList.Add("Please Insert address");
                }
                if (string.IsNullOrEmpty(model.Username))
                {
                    failList.Add("Please Insert username");
                }
                if (string.IsNullOrEmpty(model.Password))
                {
                    failList.Add("Please Insert password");
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
        // GET: api/Users
        public IHttpActionResult GetUsers()
        {
            List<User> users = db.Users.ToList();
            if (users.Count == 0)
            {
                return NotFound();
            }

            return Ok(users.Select(s => AutoMapper.Mapper.Map<UserListModel>(s)));
        }

        // GET: api/Users/5
        [ResponseType(typeof(UserListModel))]
        public IHttpActionResult GetUser(int id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(AutoMapper.Mapper.Map<UserListModel>(user));
        }

        // GET: api/Users/search
        [Route("search")]
        [ResponseType(typeof(UserListModel))]
        public IHttpActionResult GetUser(string name = "", bool? hasOrder = null, bool? isActive = null)
        {
            IEnumerable<User> users = db.Users.ToList().Where(u =>
            {
                return ((string.IsNullOrEmpty(name) || string.Format("{0} {1}", u.FirstName, u.LastName).ToLowerInvariant().Contains(name))
                && (!hasOrder.HasValue || u.Orders.Any(ord => ord.ShipDate > DateTime.Now.Date))
                && (!isActive.HasValue || u.IsActive == isActive.Value)
                );
            });

            if (users.Count() == 0)
            {
                return NotFound();
            }
            return Ok(users.Select(u => AutoMapper.Mapper.Map<UserListModel>(u)));
        }

        // PUT: api/Users
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUser(UserModel model)
        {
            string errorMessage = string.Empty;
            if (!IsValid(model, ref errorMessage, false))
            {
                return BadRequest(errorMessage);
            }

            try
            {
                User user = db.Users.Find(model.ID);
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.BirthDate = model.BirthDate;
                user.Address = model.Address;
                user.Gender = (GenderType)model.Gender;
                user.IsActive = model.IsActive;
                user.Username = model.Username;
                user.Password = model.Password;
                db.SaveChanges();

                return Ok(AutoMapper.Mapper.Map<UserListModel>(user));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

        // POST: api/Users
        [ResponseType(typeof(User))]
        public IHttpActionResult PostUser(UserModel model)
        {
            string errorMessage = string.Empty;
            if (!IsValid(model, ref errorMessage))
            {
                return BadRequest(errorMessage);
            }

            try
            {
                model.IsActive = true;
                User user = db.Users.Add(AutoMapper.Mapper.Map<User>(model));
                db.SaveChanges();

                return CreatedAtRoute("DefaultApi", new { id = model.ID }, AutoMapper.Mapper.Map<UserListModel>(user));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }


        }

        // DELETE: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult DeleteUser(int id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            db.Users.Remove(user);
            db.SaveChanges();

            return Ok(AutoMapper.Mapper.Map<UserListModel>(user));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(int id)
        {
            return db.Users.Count(e => e.ID == id) > 0;
        }
    }
}