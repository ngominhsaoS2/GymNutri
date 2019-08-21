using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using GymNutri.Data.Enums;

namespace GymNutri.Data.ViewModels.Blog
{
    public class PageViewModel
    {
        public int Id { set; get; }

        [Required]
        [MaxLength(256)]
        public string Name { set; get; }

        [MaxLength(256)]
        [Required]
        public string Alias { set; get; }

        public string Content { set; get; }

        public bool Active { get; set; }
    }
}
