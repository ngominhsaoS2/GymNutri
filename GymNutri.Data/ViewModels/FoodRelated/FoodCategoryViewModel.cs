using GymNutri.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GymNutri.Data.ViewModels.FoodRelated
{
    public class FoodCategoryViewModel
    {
        public int Id { get; set; }

        public bool Active { get; set; }

        public int? ParentId { get; set; }

        [StringLength(255)]
        public string Code { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(255)]
        public string FoodTypeCode { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        public int OrderNo { get; set; }

        [StringLength(255)]
        public string Image { get; set; }

        [StringLength(255)]
        public string Tags { get; set; }

        public ICollection<FoodViewModel> Foods { set; get; }

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
