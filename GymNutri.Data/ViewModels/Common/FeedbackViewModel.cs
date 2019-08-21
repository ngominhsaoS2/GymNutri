using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using GymNutri.Data.Enums;

namespace GymNutri.Data.ViewModels.Common
{
    public class FeedbackViewModel
    {
        public int Id { set; get; }

        [StringLength(250)]
        [Required]
        public string Name { set; get; }

        [StringLength(250)]
        public string Email { set; get; }

        [StringLength(500)]
        public string Message { set; get; }

        public bool Active { get; set; }

        public DateTime DateCreated { set; get; }

        public DateTime DateModified { set; get; }
    }
}