using System;
using System.Collections.Generic;
using System.Text;

namespace GymNutri.Data.ViewModels.FoodRelated
{
    public class SetOfFoodViewModel
    {
        public int Id { get; set; }

        public bool Active { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ListFoodNames { get; set; }

        public string ListMealIds { get; set; }

        public string ListMealNames { get; set; }

        public decimal Fat { get; set; }

        public decimal SaturatedFat { get; set; }

        public decimal Carb { get; set; }

        public decimal Protein { get; set; }

        public decimal Kcal { get; set; }

        public ICollection<FoodsInSetViewModel> FoodsInSets { set; get; }

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
