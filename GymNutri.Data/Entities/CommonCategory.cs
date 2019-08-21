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
    [Table("CommonCategories ")]
    public class CommonCategory : DomainEntity<int>, ISortable, ITracking, ISoftDelete
    {
        public CommonCategory()
        {

        }

        public CommonCategory(string code, string name, string groupCode, int orderNo, string description, bool active)
        {
            Code = code;
            Name = name;
            OrderNo = orderNo;
            GroupCode = groupCode;
            Description = description;
            Active = active;
        }

        public CommonCategory(int id, string code, string name, string groupCode, int orderNo, string description, bool active)
        {
            Id = id;
            Code = code;
            Name = name;
            OrderNo = orderNo;
            GroupCode = groupCode;
            Description = description;
            Active = active;
        }

        [Column(TypeName = "varchar(255)")]
        [StringLength(255)]
        [Required]
        public string Code { get; set; }

        [StringLength(255)]
        [Required]
        public string Name { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        [Column(TypeName = "varchar(255)")]
        [StringLength(255)]
        [Required]
        public string GroupCode { get; set; }

        [Column(TypeName = "varchar(255)")]
        [StringLength(255)]
        public string ClassCode1 { get; set; }

        [Column(TypeName = "varchar(255)")]
        [StringLength(255)]
        public string ClassCode2 { get; set; }

        [Column(TypeName = "varchar(255)")]
        [StringLength(255)]
        public string ClassCode3 { get; set; }

        public int OrderNo { get; set; }

        [StringLength(255)]
        public string UserCreated { get; set; }

        [StringLength(255)]
        public string UserModified { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }
    }
}