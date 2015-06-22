using MiiwStore.DAL;
using MiiwStore.Models;
using MiiwStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiiwStore.Services
{
    public class UserService : IDisposable
    {
        private readonly StoreContext db;
        public UserService(){
            db = new StoreContext();
        }

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

        public IEnumerable<UserListModel> GetUsers()
        {
            List<User> users = db.Users.ToList();
            if (users.Count == 0)
            {
                return null;
            }

            return users.Select(s => AutoMapper.Mapper.Map<UserListModel>(s));
        }

        public UserModel GetById(int id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return null;
            }

            return AutoMapper.Mapper.Map<UserModel>(user);
        }

        public IEnumerable<UserListModel> Search(string name = "", bool? hasOrder = null, bool? isActive = null)
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
                return null;
            }
            return users.Select(u => AutoMapper.Mapper.Map<UserListModel>(u));
        }

        public UserListModel Update(UserModel model, ref string errorMessage)
        {
            if (!IsValid(model, ref errorMessage, false))
            {
                return null;
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

                return AutoMapper.Mapper.Map<UserListModel>(user);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public UserListModel Update(UserModel model)
        {
            string errorMessage = string.Empty;
            return Update(model, ref errorMessage);
        }

        public UserListModel Create(UserModel model, ref string errorMessage)
        {
            if (!IsValid(model, ref errorMessage))
            {
                return null;
            }

            try
            {
                model.IsActive = true;
                User user = db.Users.Add(AutoMapper.Mapper.Map<User>(model));
                db.SaveChanges();

                return AutoMapper.Mapper.Map<UserListModel>(user);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public UserListModel Create(UserModel model)
        {
            string errorMessage = string.Empty;
            return Create(model, ref errorMessage);
        }

        public UserListModel Delete(int id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return null;
            }

            db.Users.Remove(user);
            db.SaveChanges();

            return AutoMapper.Mapper.Map<UserListModel>(user);
        }

        public void Dispose()
        {
            db.Dispose();
        }

    }
}
