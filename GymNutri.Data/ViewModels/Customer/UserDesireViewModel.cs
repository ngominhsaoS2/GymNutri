using System;
using System.Collections.Generic;
using System.Text;

namespace GymNutri.Data.ViewModels.Customer
{
    public class UserDesireViewModel
    {
        public int Id { get; set; }

        public bool Active { get; set; }

        public Guid UserId { get; set; }

        public string DesireTypeCode { get; set; } //Lấy trong bảng CommonCategory với GroupCode = DesireType

        public string ChangingSpeed { get; set; }

        public string PracticeIntensive { get; set; }

        public string Description { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string Detail { get; set; }

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
