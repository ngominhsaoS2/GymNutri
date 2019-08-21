using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using GymNutri.Data.Interfaces;
using GymNutri.Infrastructure.SharedKernel;

namespace GymNutri.Data.Entities
{
    [Table("UserFavoriteTastes")]
    public class UserFavoriteTaste : DomainEntity<int>, IHasSeoMetaData, ITracking, ISoftDelete
    {
        [Required]
        public Guid UserId { get; set; }

        [StringLength(255)]
        [Required]
        public string FavoriteTasteTypeCode { get; set; } //Lấy trong bảng CommonCategory với GroupCode = 

        [StringLength(255)]
        [Required]
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