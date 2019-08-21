using System;
using System.Collections.Generic;
using System.Text;

namespace GymNutri.Data.ViewModels.Customer
{
    public class UserBodyIndexViewModel
    {
        public int Id { get; set; }

        public bool Active { get; set; }

        public Guid UserId { get; set; }

        public DateTime Date { get; set; }

        public int Age { get; set; }

        public decimal HeightCm { get; set; } //Centimeters

        public decimal HeightM { get; set; } //Meters
        
        public decimal HeightIn { get; set; } //Inches
        
        public decimal HeightFt { get; set; } //Feets
        
        public decimal WeightKg { get; set; } //Kilograms
        
        public decimal WeightLb { get; set; } //Pounds
        
        public decimal ChestCm { get; set; } //Centimeters
        
        public decimal WaistCm { get; set; } //Centimeters
        
        public decimal AssCm { get; set; } //Centimeters
        
        public decimal BellyCm { get; set; }
        
        public decimal BellyIn { get; set; } //Inches
        
        public decimal FatPercent { get; set; }
        
        public decimal MusclePercent { get; set; }

        public decimal IdiWproBmi { get; set; }
        
        public decimal BodyFat { get; set; }

        public decimal Lbm { get; set; }

        public decimal Bmr { get; set; }

        public decimal Tdee { get; set; }

        public string BodyClassifications { get; set; }

        public string SeoPageTitle { get; set; }

        public string SeoAlias { get; set; }

        public string SeoKeywords { get; set; }

        public string SeoDescription { get; set; }

        public string UserCreated { get; set; }

        public string UserModified { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }
    }
}
