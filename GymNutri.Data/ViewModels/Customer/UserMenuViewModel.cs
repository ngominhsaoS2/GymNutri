using GymNutri.Data.ViewModels.Template;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymNutri.Data.ViewModels.Customer
{
    public class UserMenuViewModel
    {
        public int Id { get; set; }

        public bool Active { get; set; }

        public Guid UserId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int TemplateMenuId { get; set; }

        public TemplateMenuViewModel TemplateMenu { get; set; }

        public string Description { get; set; }

        public string UserCreated { get; set; }

        public string UserModified { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

    }
}
