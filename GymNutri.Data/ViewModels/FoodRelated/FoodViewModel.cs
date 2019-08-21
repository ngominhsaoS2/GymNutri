using GymNutri.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymNutri.Data.ViewModels.FoodRelated
{
    public class FoodViewModel
    {
        public long Id { get; set; }

        public bool Active { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string Unit { get; set; }

        public int FoodCategoryId { get; set; }

        public FoodCategoryViewModel FoodCategory { set; get; }

        public string Description { get; set; }

        public string Ingredient { get; set; }

        public string Recipe { get; set; }

        public int CookingDuration { get; set; } //Minutes

        public string CookingGuideLink { get; set; }

        public string Image { get; set; }

        public string Tags { get; set; }

        public decimal FatPerUnit { get; set; }

        public decimal SaturatedFatPerUnit { get; set; }

        public decimal CarbPerUnit { get; set; }

        public decimal ProteinPerUnit { get; set; }

        public decimal KcalPerUnit { get; set; }

        public ICollection<FoodTagViewModel> FoodTags { set; get; }

        public ICollection<FoodPriceViewModel> FoodPrices { set; get; }

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
