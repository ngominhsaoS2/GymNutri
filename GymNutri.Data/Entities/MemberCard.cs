using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using GymNutri.Data.Interfaces;
using GymNutri.Infrastructure.SharedKernel;

namespace GymNutri.Data.Entities
{

    [Table("MemberCards")]
    public class MemberCard : DomainEntity<int>, IHasSeoMetaData, ITracking, ISoftDelete
    {
        [Required]
        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual AppUser AppUser { get; set; }

        [Required]
        [StringLength(255)]
        public string MemberTypeCode { get; set; } //Lấy trong bảng CommonCategories với GroupCode = MemberType

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public int MonthDuration { get; set; }

        [Required]
        public int TemplateMenuId { get; set; }

        [ForeignKey("TemplateMenuId")]
        public virtual TemplateMenu TemplateMenu { get; set; }

        [Required]
        [DefaultValue(0)]
        public decimal Cost { get; set; }

        [Required]
        [DefaultValue(0)]
        public decimal Price { get; set; }

        [DefaultValue(0)]
        public decimal PromotionPrice { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

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