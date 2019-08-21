using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using GymNutri.Data.Interfaces;
using GymNutri.Infrastructure.SharedKernel;

namespace GymNutri.Data.Entities
{
    [Table("DaisyOrders")]
    public class DaisyOrder : DomainEntity<long>, IHasSeoMetaData, ITracking, ISoftDelete
    {
        [Required]
        public int MemberCardId { get; set; }

        [ForeignKey("MemberCardId")]
        public virtual MemberCard MemberCard { set; get; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public int OrderInDay { get; set; } 

        [Required]
        public TimeSpan PlanOrderTime { get; set; }

        public TimeSpan RealOrderTime { get; set; }

        [Required]
        public int LocationId { get; set; }

        [ForeignKey("LocationId")]
        public virtual LocationToGetOrderOfUser LocationToGetOrderOfUser { set; get; }

        [StringLength(255)]
        public string AlternativePersonName { get; set; }

        [StringLength(255)]
        public string AlternativePersonPhone { get; set; }

        [StringLength(255)]
        public string AlternativePersonEmail { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        [StringLength(500)]
        public string UserFeedback { get; set; }

        public virtual ICollection<DaisyOrderDetail> DaisyOrderDetails { set; get; }

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