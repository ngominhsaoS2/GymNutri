using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using GymNutri.Data.Interfaces;
using GymNutri.Infrastructure.SharedKernel;

namespace GymNutri.Data.Entities
{
    [Table("FoodTags")]
    public class FoodTag : DomainEntity<int>, ITracking
    {
        [Required]
        public long FoodId { get; set; }

        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        [Required]
        public string TagId { set; get; }

        [ForeignKey("FoodId")]
        public virtual Food Food { set; get; }

        [ForeignKey("TagId")]
        public virtual Tag Tag { set; get; }

        [StringLength(255)]
        public string UserCreated { get; set; }

        [StringLength(255)]
        public string UserModified { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }
    }
}