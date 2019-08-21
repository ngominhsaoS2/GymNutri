using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using GymNutri.Data.Interfaces;
using GymNutri.Infrastructure.SharedKernel;

namespace GymNutri.Data.Entities
{
    [Table("TemplateMenus")]
    public class TemplateMenu : DomainEntity<int>, IHasSeoMetaData, ITracking, ISoftDelete
    {
        [Column(TypeName = "varchar(255)")]
        [StringLength(255)]
        [Required]
        public string Code { get; set; }

        [StringLength(255)]
        [Required]
        public string Name { get; set; }
                
        [StringLength(255)]
        public string Description { get; set; }

        [Column(TypeName = "decimal(18,5)")]
        public decimal FatAverage { get; set; }

        [Column(TypeName = "decimal(18,5)")]
        public decimal SaturatedFatAverage { get; set; }

        [Column(TypeName = "decimal(18,5)")]
        public decimal CarbAverage { get; set; }

        [Column(TypeName = "decimal(18,5)")]
        public decimal ProteinAverage { get; set; }

        [Column(TypeName = "decimal(18,5)")]
        public decimal KcalAverage { get; set; }

        [StringLength(500)]
        public string Meals { get; set; }
        
        [StringLength(500)]
        public string BodyClassifications { get; set; }
        
        public virtual ICollection<TemplateMenuSet> TemplateMenuSets { set; get; }

        public virtual ICollection<TemplateMenuForBodyClassification> TemplateMenuForBodyClassifications { set; get; }

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