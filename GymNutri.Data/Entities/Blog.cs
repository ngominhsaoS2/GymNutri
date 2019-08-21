using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GymNutri.Data.Enums;
using GymNutri.Data.Interfaces;
using GymNutri.Infrastructure.SharedKernel;

namespace GymNutri.Data.Entities
{
    [Table("Blogs")]
    public class Blog : DomainEntity<int>, IHasSeoMetaData, ITracking, ISoftDelete
    {
        public Blog() { }

        public Blog(string name,string thumbnailImage,
           string description, string content, bool? homeFlag, bool? hotFlag,
           string tags, string seoPageTitle,
           string seoAlias, string seoMetaKeyword,
           string seoMetaDescription, bool active)
        {
            Name = name;
            Image = thumbnailImage;
            Description = description;
            Content = content;
            HomeFlag = homeFlag;
            HotFlag = hotFlag;
            Tags = tags;
            SeoPageTitle = seoPageTitle;
            SeoAlias = seoAlias;
            SeoKeywords = seoMetaKeyword;
            SeoDescription = seoMetaDescription;
            Active = active;
        }

        public Blog(int id, string name,string thumbnailImage,
             string description, string content, bool? homeFlag, bool? hotFlag,
             string tags, string seoPageTitle,
             string seoAlias, string seoMetaKeyword,
             string seoMetaDescription, bool active)
        {
            Id = id;
            Name = name;
            Image = thumbnailImage;
            Description = description;
            Content = content;
            HomeFlag = homeFlag;
            HotFlag = hotFlag;
            Tags = tags;
            SeoPageTitle = seoPageTitle;
            SeoAlias = seoAlias;
            SeoKeywords = seoMetaKeyword;
            SeoDescription = seoMetaDescription;
            Active = active;
        }
        [Required]
        [MaxLength(256)]
        public string Name { get; set; }


        [MaxLength(256)]
        public string Image { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public string Content { get; set; }

        public bool? HomeFlag { get; set; }

        public bool? HotFlag { get; set; }

        public int? ViewCount { get; set; }

        [StringLength(255)]
        public string Tags { get; set; }

        public virtual ICollection<BlogTag> BlogTags { get; set; }

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