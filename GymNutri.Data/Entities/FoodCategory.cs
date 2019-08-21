using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using GymNutri.Data.Enums;
using GymNutri.Data.Interfaces;
using GymNutri.Infrastructure.SharedKernel;

namespace GymNutri.Data.Entities
{
    [Table("FoodCategories")]
    public class FoodCategory : DomainEntity<int>, IHasSeoMetaData, ISortable, ITracking, ISoftDelete
    {
        public FoodCategory()
        {

        }

        public FoodCategory(bool active, int? parentId, string code, string name, string foodTypeCode, string description, int orderNo,
            string image, string tags, string seoPageTitle, string seoAlias, string seoKeywords, string seoDescription)
        {
            Active = active;
            ParentId = parentId;
            Code = code;
            Name = name;
            FoodTypeCode = foodTypeCode;
            Description = description;
            OrderNo = orderNo;
            Image = image;
            Tags = tags;
            SeoPageTitle = seoPageTitle;
            SeoAlias = seoAlias;
            SeoKeywords = seoKeywords;
            SeoDescription = seoDescription;
        }

        public FoodCategory(int id, bool active, int? parentId, string code, string name, string foodTypeCode, string description, int orderNo,
            string image, string tags, string seoPageTitle, string seoAlias, string seoKeywords, string seoDescription)
        {
            Id = id;
            Active = active;
            ParentId = parentId;
            Code = code;
            Name = name;
            FoodTypeCode = foodTypeCode;
            Description = description;
            OrderNo = orderNo;
            Image = image;
            Tags = tags;
            SeoPageTitle = seoPageTitle;
            SeoAlias = seoAlias;
            SeoKeywords = seoKeywords;
            SeoDescription = seoDescription;
        }

        public int? ParentId { get; set; }

        [Column(TypeName = "varchar(255)")]
        [StringLength(255)]
        [Required]
        public string Code { get; set; }

        [StringLength(255)]
        [Required]
        public string Name { get; set; }

        [StringLength(255)]
        public string FoodTypeCode { get; set; } //Lấy trong bảng CommonCategory với GroupCode = "FoodType"

        [StringLength(255)]
        public string Description { get; set; }

        [Required]
        public int OrderNo { get; set; }

        [StringLength(255)]
        public string Image { get; set; }

        [StringLength(255)]
        public string Tags { get; set; }

        public virtual ICollection<FoodCategoryTag> FoodCategoryTags { get; set; }

        public virtual ICollection<Food> Foods { set; get; }

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