using GymNutri.Data.Enums;
using GymNutri.Data.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GymNutri.Data.Entities
{
    [Table("AppUsers")]
    public class AppUser : IdentityUser<Guid>, ITracking, ISoftDelete
    {
        public AppUser()
        {

        }

        public AppUser(Guid id, string fullName, string userName,
            string email, string phoneNumber, string avatar, bool active)
        {
            Id = id;
            FullName = fullName;
            UserName = userName;
            Email = email;
            PhoneNumber = phoneNumber;
            Avatar = avatar;
            Active = active;
        }

        [StringLength(255)]
        public string FullName { get; set; }

        public DateTime? Birthday { get; set; }

        public decimal Balance { get; set; }

        [StringLength(1000)]
        public string Avatar { get; set; }

        [StringLength(32)]
        public string Gender { get; set; }

        [StringLength(255)]
        public string UserCreated { get; set; }

        [StringLength(255)]
        public string UserModified { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public bool Active { get; set; }


    }
}
