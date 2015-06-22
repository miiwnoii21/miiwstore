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
using MiiwStore.Services;

namespace MiiwStore.Controllers.Api
{
    [RoutePrefix("api/users")]
    public class UsersController : ApiController
    {
        private readonly UserService userService; 

        public UsersController()
        {
            userService = new UserService();
        }

        
        // GET: api/Users
        public IHttpActionResult GetUsers()
        {
            IEnumerable<UserListModel> users = userService.GetUsers();
            if (users.Count() == 0)
            {
                return NotFound();
            }
            return Ok(users);
        }

        // GET: api/Users/5
        [ResponseType(typeof(UserListModel))]
        public IHttpActionResult GetUser(int id)
        {
            UserModel user = userService.GetById(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // GET: api/Users/search
        [Route("search")]
        [ResponseType(typeof(UserListModel))]
        public IHttpActionResult GetUser(string name = "", bool? hasOrder = null, bool? isActive = null)
        {
            IEnumerable<UserListModel> users = userService.Search(name, hasOrder, isActive);

            if (users.Count() == 0)
            {
                return NotFound();
            }
            return Ok(users);
        }

        // PUT: api/Users
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUser(UserModel model)
        {
            
            try
            {
                string errorMessage = string.Empty;
                UserListModel user = userService.Update(model, ref errorMessage);

                if (string.IsNullOrEmpty(errorMessage)) return Ok(user);
                else return BadRequest(errorMessage);

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
            try
            {
                string errorMessage = string.Empty;
                UserListModel user = userService.Create(model, ref errorMessage);
                if (string.IsNullOrEmpty(errorMessage)) return CreatedAtRoute("DefaultApi", new { id = model.ID }, user);
                else return BadRequest(errorMessage);
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
            UserListModel user = userService.Delete(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                userService.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}