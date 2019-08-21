using GymNutri.Data.ViewModels.Template;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymNutri.Data.ViewModels.Customer
{
    public class BodyClassificationViewModel
    {
        public int Id { get; set; }

        public bool Active { get; set; }

        public string GroupCode { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ListTemplateMenuIds { get; set; }

        public ICollection<TemplateMenuForBodyClassificationViewModel> TemplateMenuForBodyClassifications { set; get; }

        public string Detail { get; set; }

        public string Criterion { get; set; }

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
