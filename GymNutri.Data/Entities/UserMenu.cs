using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using GymNutri.Data.Interfaces;
using GymNutri.Infrastructure.SharedKernel;

namespace GymNutri.Data.Entities
{
    [Table("UserMenus")]
    public class UserMenu : DomainEntity<int>, ITracking, ISoftDelete
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public int TemplateMenuId { get; set; }

        [ForeignKey("TemplateMenuId")]
        public virtual TemplateMenu TemplateMenu { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        [StringLength(255)]
        public string UserCreated { get; set; }

        [StringLength(255)]
        public string UserModified { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }
        
    }
}