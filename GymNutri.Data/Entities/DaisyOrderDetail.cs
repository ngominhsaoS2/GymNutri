using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using GymNutri.Data.Interfaces;
using GymNutri.Infrastructure.SharedKernel;

namespace GymNutri.Data.Entities
{
    [Table("DaisyOrderDetails")]
    public class DaisyOrderDetail : DomainEntity<long>, ITracking, ISoftDelete
    {
        [Required]
        public long DaisyOrderId { get; set; }

        [ForeignKey("DaisyOrderId")]
        public virtual DaisyOrder DaisyOrder { set; get; }
        
        public int MealId { get; set; }

        [ForeignKey("MealId")]
        public virtual Meal Meal { set; get; }

        [Required]
        public int SetOfFoodId { get; set; }

        [ForeignKey("SetOfFoodId")]
        public virtual SetOfFood SetOfFood { set; get; }

        [StringLength(255)]
        public string Description { get; set; }

        public TimeSpan RealEatTime { get; set; }

        [StringLength(500)]
        public string UserFeedback { get; set; }

        [StringLength(255)]
        public string UserCreated { get; set; }

        [StringLength(255)]
        public string UserModified { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }
    }
}