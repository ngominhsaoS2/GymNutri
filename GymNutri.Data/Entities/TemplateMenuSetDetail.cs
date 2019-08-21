using GymNutri.Data.Interfaces;
using GymNutri.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GymNutri.Data.Entities
{

    [Table("TemplateMenuSetDetails")]
    public class TemplateMenuSetDetail : DomainEntity<int>, ITracking, ISoftDelete
    {
        [Required]
        public int TemplateMenuSetId { get; set; }

        [ForeignKey("TemplateMenuSetId")]
        public virtual TemplateMenuSet TemplateMenuSet { get; set; }

        [Required]
        public int MealId { get; set; }

        [ForeignKey("MealId")]
        public virtual Meal Meal { get; set; }

        [Required]
        public int SetOfFoodId { get; set; }

        [ForeignKey("SetOfFoodId")]
        public virtual SetOfFood SetOfFood { get; set; }

        [StringLength(255)]
        public string UserCreated { get; set; }

        [StringLength(255)]
        public string UserModified { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }
    }
}
