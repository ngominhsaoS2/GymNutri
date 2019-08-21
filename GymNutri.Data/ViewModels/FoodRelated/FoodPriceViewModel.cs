using System;
using System.Collections.Generic;
using System.Text;

namespace GymNutri.Data.ViewModels.FoodRelated
{
    public class FoodPriceViewModel
    {
        public long Id { get; set; }

        public bool Active { get; set; }

        public DateTime Date { get; set; }

        public long FoodId { get; set; }

        public FoodViewModel Food { set; get; }

        public decimal Cost { get; set; }

        public decimal Price { get; set; }

        public decimal PromotionPrice { get; set; }

        public string UserCreated { get; set; }

        public string UserModified { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }
    }
}
