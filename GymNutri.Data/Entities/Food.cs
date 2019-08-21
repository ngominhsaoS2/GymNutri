using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GymNutri.Data.Interfaces;
using GymNutri.Infrastructure.SharedKernel;

namespace GymNutri.Data.Entities
{
    [Table("Foods")]
    public class Food : DomainEntity<long>, IHasSeoMetaData, ITracking, ISoftDelete
    {
        [Column(TypeName = "varchar(255)")]
        [StringLength(255)]
        [Required]
        public string Code { get; set; }

        [StringLength(255)]
        [Required]
        public string Name { get; set; }

        [StringLength(255)]
        [Required]
        public string Unit { get; set; }

        [Required]
        public int FoodCategoryId { get; set; }

        [ForeignKey("FoodCategoryId")]
        public virtual FoodCategory FoodCategory { set; get; }

        [StringLength(255)]
        [Required]
        public string Description { get; set; }

        [StringLength(1000)]
        public string Ingredient { get; set; }

        [StringLength(4000)]
        public string Recipe { get; set; }

        public int CookingDuration { get; set; } //Minutes

        [StringLength(1000)]
        public string CookingGuideLink { get; set; }

        [StringLength(1000)]
        public string Image { get; set; }

        [StringLength(255)]
        public string Tags { get; set; }

        [Column(TypeName = "decimal(18,5)")]
        public decimal FatPerUnit { get; set; }

        [Column(TypeName = "decimal(18,5)")]
        public decimal SaturatedFatPerUnit { get; set; }

        [Column(TypeName = "decimal(18,5)")]
        public decimal CarbPerUnit { get; set; }

        [Column(TypeName = "decimal(18,5)")]
        public decimal ProteinPerUnit { get; set; }

        [Column(TypeName = "decimal(18,5)")]
        public decimal KcalPerUnit { get; set; }

        public virtual ICollection<FoodTag> FoodTags { set; get; }

        public virtual ICollection<FoodPrice> FoodPrices { set; get; }

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