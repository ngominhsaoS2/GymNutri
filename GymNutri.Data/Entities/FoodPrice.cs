using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using GymNutri.Data.Interfaces;
using GymNutri.Infrastructure.SharedKernel;

namespace GymNutri.Data.Entities
{
    [Table("FoodPrices")]
    public class FoodPrice : DomainEntity<long>, ITracking, ISoftDelete
    {
        [Required]
        public DateTime Date { get; set; }

        [Required]
        public long FoodId { get; set; }

        [ForeignKey("FoodId")]
        public virtual Food Food { set; get; }

        [Required]
        [DefaultValue(0)]
        public decimal Cost { get; set; }

        [Required]
        [DefaultValue(0)]
        public decimal Price { get; set; }

        [DefaultValue(0)]
        public decimal PromotionPrice { get; set; }

        [StringLength(255)]
        public string UserCreated { get; set; }

        [StringLength(255)]
        public string UserModified { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }
    }
}