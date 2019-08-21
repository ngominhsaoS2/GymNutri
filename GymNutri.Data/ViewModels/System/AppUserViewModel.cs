using GymNutri.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GymNutri.Data.ViewModels.System
{
    public class AppUserViewModel
    {
        public AppUserViewModel()
        {
            Roles = new List<string>();
        }

        public Guid? Id { set; get; }

        [StringLength(255)]
        public string FullName { set; get; }

        public string Birthday { set; get; }

        public string Email { set; get; }

        public string Password { set; get; }

        public string UserName { set; get; }

        public string Address { get; set; }

        public string PhoneNumber { set; get; }

        public string Avatar { get; set; }

        public bool Active { get; set; }

        public string Gender { get; set; }

        public string UserCreated { get; set; }

        public string UserModified { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public List<string> Roles { get; set; }
    }
}
