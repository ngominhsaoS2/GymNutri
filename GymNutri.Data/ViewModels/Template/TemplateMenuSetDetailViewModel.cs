using GymNutri.Data.Entities;
using GymNutri.Data.ViewModels.FoodRelated;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymNutri.Data.ViewModels.Template
{
    public class TemplateMenuSetDetailViewModel
    {
        public int Id { get; set; }

        public bool Active { get; set; }

        public int TemplateMenuSetId { get; set; }

        public virtual TemplateMenuSetViewModel TemplateMenuSet { get; set; }

        public int MealId { get; set; }

        public virtual MealViewModel Meal { get; set; }

        public int SetOfFoodId { get; set; }

        public virtual SetOfFoodViewModel SetOfFood { get; set; }

        public string UserCreated { get; set; }

        public string UserModified { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }
    }
}
