using System;
using System.Collections.Generic;
using System.Text;

namespace GymNutri.Data.ViewModels.FoodRelated
{
    public class MealViewModel
    {
        public int Id { get; set; }

        public bool Active { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string GroupCode { get; set; }

        public int OrderNo { get; set; }

        public TimeSpan MealTime { get; set; }

        public string Description { get; set; }

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
