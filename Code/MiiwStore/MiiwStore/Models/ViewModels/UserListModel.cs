using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiiwStore.Models.ViewModels
{
    public class UserListModel
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public bool IsActive { get; set; }
        public string Username { get; set; }
        public bool HasOrder { get; set; }
    }
}