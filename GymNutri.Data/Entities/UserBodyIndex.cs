using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using GymNutri.Data.Interfaces;
using GymNutri.Infrastructure.SharedKernel;

namespace GymNutri.Data.Entities
{
    [Table("UserBodyIndexes")]
    public class UserBodyIndex : DomainEntity<int>, IHasSeoMetaData, ITracking, ISoftDelete
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public int Age { get; set; }

        [Range(typeof(decimal), "0", "300")]
        public decimal HeightCm { get; set; } //Centimeters

        [Range(typeof(decimal), "0", "3")]
        public decimal HeightM { get; set; } //Meters

        [Range(typeof(decimal), "0", "150")]
        public decimal HeightIn { get; set; } //Inches

        [Range(typeof(decimal), "0", "10")]
        public decimal HeightFt { get; set; } //Feets

        [Range(typeof(decimal), "0", "1000")]
        public decimal WeightKg { get; set; } //Kilograms

        [Range(typeof(decimal), "0", "2000")]
        public decimal WeightLb { get; set; } //Pounds

        [Range(typeof(decimal), "0", "1000")]
        public decimal ChestCm { get; set; } //Centimeters

        [Range(typeof(decimal), "0", "1000")]
        public decimal WaistCm { get; set; } //Centimeters

        [Range(typeof(decimal), "0", "1000")]
        public decimal AssCm { get; set; } //Centimeters

        [Range(typeof(decimal), "0", "1000")]
        public decimal BellyCm { get; set; }

        [Range(typeof(decimal), "0", "500")]
        public decimal BellyIn { get; set; } //Inches

        [Range(typeof(decimal), "0", "100")]
        public decimal FatPercent { get; set; }

        [Range(typeof(decimal), "0", "100")]
        public decimal MusclePercent { get; set; }

        public decimal IdiWproBmi { get; set; }

        [Range(typeof(decimal), "0", "100")]
        public decimal BodyFat { get; set; }

        public decimal Lbm { get; set; }

        public decimal Bmr { get; set; }

        public decimal Tdee { get; set; }

        [StringLength(500)]
        public string BodyClassifications { get; set; }

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