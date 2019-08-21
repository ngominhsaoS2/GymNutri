using GymNutri.Data.Interfaces;
using GymNutri.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GymNutri.Data.Entities
{
    public class FoodCategoryTag : DomainEntity<int>, ITracking
    {
        public int FoodCategoryId { get; set; }

        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string TagId { get; set; }

        [ForeignKey("FoodCategoryId")]
        public virtual FoodCategory FoodCategory { get; set; }

        [ForeignKey("TagId")]
        public virtual Tag Tag { get; set; }

        [StringLength(255)]
        public string UserCreated { get; set; }

        [StringLength(255)]
        public string UserModified { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

    }
}
