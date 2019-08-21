using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using GymNutri.Data.Interfaces;
using GymNutri.Infrastructure.SharedKernel;

namespace GymNutri.Data.Entities
{
    [Table("TemplateMenuForBodyClassifications")]
    public class TemplateMenuForBodyClassification : DomainEntity<int>, IHasSeoMetaData, ITracking, ISoftDelete
    {
        [Required]
        public int TemplateMenuId { get; set; }

        [ForeignKey("TemplateMenuId")]
        public virtual TemplateMenu TemplateMenu { set; get; }

        [Required]
        public int BodyClassificationId { get; set; }

        [ForeignKey("BodyClassificationId")]
        public virtual BodyClassification BodyClassification { set; get; }

        [StringLength(255)]
        public string InsertedSource { get; set; }

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