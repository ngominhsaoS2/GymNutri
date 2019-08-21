using GymNutri.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GymNutri.Data.Entities
{
    [Table("FoodCategoryImages")]
    public class FoodCategoryImage : DomainEntity<int>
    {
        public int FoodCategoryId { get; set; }

        [ForeignKey("FoodCategoryId")]
        public virtual FoodCategory FoodCategory { get; set; }

        [StringLength(255)]
        public string Path { get; set; }

        [StringLength(255)]
        public string Caption { get; set; }


    }
}
