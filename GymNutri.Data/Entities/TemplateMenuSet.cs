using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using GymNutri.Data.Interfaces;
using GymNutri.Infrastructure.SharedKernel;

namespace GymNutri.Data.Entities
{
    [Table("TemplateMenuSets")]
    public class TemplateMenuSet : DomainEntity<int>, ITracking, ISoftDelete, IHasNutrionProperties
    {
        [Required]
        public int TemplateMenuId { get; set; }

        [ForeignKey("TemplateMenuId")]
        public virtual TemplateMenu TemplateMenu { get; set; }

        public virtual ICollection<TemplateMenuSetDetail> TemplateMenuSetDetails { set; get; }

        [Column(TypeName = "varchar(255)")]
        [StringLength(255)]
        public string Code { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        [Column(TypeName = "decimal(18,5)")]
        public decimal Fat { get; set; }

        [Column(TypeName = "decimal(18,5)")]
        public decimal SaturatedFat { get; set; }

        [Column(TypeName = "decimal(18,5)")]
        public decimal Carb { get; set; }

        [Column(TypeName = "decimal(18,5)")]
        public decimal Protein { get; set; }

        [Column(TypeName = "decimal(18,5)")]
        public decimal Kcal { get; set; }

        [StringLength(255)]
        public string UserCreated { get; set; }

        [StringLength(255)]
        public string UserModified { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }
        
    }
}