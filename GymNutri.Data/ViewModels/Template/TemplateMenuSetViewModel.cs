using System;
using System.Collections.Generic;
using System.Text;

namespace GymNutri.Data.ViewModels.Template
{
    public class TemplateMenuSetViewModel
    {
        public int Id { get; set; }

        public bool Active { get; set; }

        public int TemplateMenuId { get; set; }

        public virtual TemplateMenuViewModel TemplateMenu { get; set; }

        public ICollection<TemplateMenuSetDetailViewModel> TemplateMenuSetDetails { set; get; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Fat { get; set; }

        public decimal SaturatedFat { get; set; }

        public decimal Carb { get; set; }

        public decimal Protein { get; set; }

        public decimal Kcal { get; set; }

        public string UserCreated { get; set; }

        public string UserModified { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

    }
}
