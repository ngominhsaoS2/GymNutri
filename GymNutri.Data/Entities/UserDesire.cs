using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using GymNutri.Data.Interfaces;
using GymNutri.Infrastructure.SharedKernel;

namespace GymNutri.Data.Entities
{
    [Table("UserDesires")]
    public class UserDesire : DomainEntity<int>, IHasSeoMetaData, ITracking, ISoftDelete
    {
        [Required]
        public Guid UserId { get; set; }

        [StringLength(255)]
        [Required]
        public string DesireTypeCode { get; set; } //Lấy trong bảng CommonCategory với GroupCode = DesireType

        [StringLength(255)]
        public string ChangingSpeed { get; set; }

        [StringLength(255)]
        public string PracticeIntensive { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [StringLength(4000)]
        public string Detail { get; set; }

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