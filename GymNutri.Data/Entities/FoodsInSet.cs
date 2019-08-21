using GymNutri.Data.Interfaces;
using GymNutri.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GymNutri.Data.Entities
{
    [Table("FoodsInSets")]
    public class FoodsInSet : DomainEntity<long>, IHasSeoMetaData, ITracking, ISoftDelete
    {
        [Required]
        public int SetOfFoodId { get; set; }

        [ForeignKey("SetOfFoodId")]
        public virtual SetOfFood SetOfFood { get; set; }

        [Required]
        public long FoodId { get; set; }

        [ForeignKey("FoodId")]
        public virtual Food Food { get; set; }

        public decimal Quantity { get; set; }

        [StringLength(500)]
        public string SeoPageTitle { get; set; }

        [StringLength(500)]
        public string SeoAlias { get; set; }

        [StringLength(500)]
        public string SeoKeywords { get; set; }

        [StringLength(2000)]
        public string SeoDescription { get; set; }

        [StringLength(255)]
        public string UserCreated { get; set; }

        [StringLength(255)]
        public string UserModified { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }
    }
}
