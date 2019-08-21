using GymNutri.Data.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymNutri.Data.ViewModels.FoodRelated
{
    public class FoodTagViewModel
    {
        public int Id { get; set; }

        public bool Active { get; set; }

        public long FoodId { get; set; }

        public string TagId { set; get; }

        public FoodViewModel Food { set; get; }

        public TagViewModel Tag { set; get; }

        public string UserCreated { get; set; }

        public string UserModified { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

    }
}
