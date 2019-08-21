using GymNutri.Data.Entities;
using GymNutri.Data.ViewModels.System;
using GymNutri.Data.ViewModels.Template;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymNutri.Data.ViewModels.Selling
{
    public class MemberCardViewModel
    {
        public int Id { get; set; }

        public bool Active { get; set; }

        public Guid UserId { get; set; }

        public AppUserViewModel AppUser { get; set; }

        public string MemberTypeCode { get; set; } //Lấy trong bảng CommonCategories với GroupCode = MemberType

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int MonthDuration { get; set; }

        public int TemplateMenuId { get; set; }

        public TemplateMenu TemplateMenu { get; set; } // Tạm thời dùng TemplateMenu thay vì TemplateMenuViewModel. Sẽ tìm hiểu lý do kỹ càng hơn.

        public decimal Cost { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public decimal PromotionPrice { get; set; }
        
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
