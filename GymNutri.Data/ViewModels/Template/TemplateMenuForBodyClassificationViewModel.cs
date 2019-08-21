using GymNutri.Data.ViewModels.Customer;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymNutri.Data.ViewModels.Template
{
    public class TemplateMenuForBodyClassificationViewModel
    {
        public int Id { get; set; }

        public bool Active { get; set; }

        public int TemplateMenuId { get; set; }

        public TemplateMenuViewModel TemplateMenu { set; get; }

        public int BodyClassificationId { get; set; }

        public BodyClassificationViewModel BodyClassification { set; get; }

        public string InsertedSource { get; set; }

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
