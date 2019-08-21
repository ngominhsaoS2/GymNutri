using GymNutri.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymNutri.Data.ViewModels.Template
{
    public class TemplateMenuViewModel
    {
        public int Id { get; set; }

        public bool Active { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
        
        public decimal FatAverage { get; set; }

        public decimal SaturatedFatAverage { get; set; }

        public decimal CarbAverage { get; set; }

        public decimal ProteinAverage { get; set; }

        public decimal KcalAverage { get; set; }

        public string Meals { get; set; }

        public string BodyClassifications { get; set; }
        
        public ICollection<TemplateMenuSetViewModel> TemplateMenuSets { set; get; }

        public ICollection<TemplateMenuForBodyClassificationViewModel> TemplateMenuForBodyClassifications { set; get; }

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
