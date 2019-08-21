using GymNutri.Data.Interfaces;
using GymNutri.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GymNutri.Data.Entities
{
    [Table("SetOfFoods")]
    public class SetOfFood : DomainEntity<int>, IHasSeoMetaData, ITracking, ISoftDelete, IHasNutrionProperties
    {
        public SetOfFood()
        {
            FoodsInSets = new List<FoodsInSet>();
        }

        [Column(TypeName = "varchar(255)")]
        [StringLength(255)]
        [Required]
        public string Code { get; set; }

        [StringLength(255)]
        [Required]
        public string Name { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        [StringLength(500)]
        public string ListFoodNames { get; set; }

        [StringLength(500)]
        public string ListMealIds { get; set; }

        [StringLength(500)]
        public string ListMealNames { get; set; }

        [Column(TypeName = "decimal(18,5)")]
        public decimal Fat { get; set; }

        [Column(TypeName = "decimal(18,5)")]
        public decimal SaturatedFat { get; set; }

        [Column(TypeName = "decimal(18,5)")]
        public decimal Carb { get; set; }

        [Column(TypeName = "decimal(18,5)")]
        public decimal Protein { get; set; }

        [Column(TypeName = "decimal(18,5)")]
        public decimal Kcal { get; set; }

        public virtual ICollection<FoodsInSet> FoodsInSets { set; get; }

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
