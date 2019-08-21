using System;
using System.Collections.Generic;
using System.Text;

namespace GymNutri.Data.ViewModels.FoodRelated
{
    public class FoodsInSetViewModel
    {
        public long Id { get; set; }

        public bool Active { get; set; }

        public int SetOfFoodId { get; set; }

        public SetOfFoodViewModel SetOfFood { get; set; }

        public long FoodId { get; set; }

        public FoodViewModel Food { get; set; }

        public decimal Quantity { get; set; }

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
