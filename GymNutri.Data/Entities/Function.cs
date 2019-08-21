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
    [Table("Functions")]
    public class Function : DomainEntity<string>, ISortable, ISoftDelete
    {
        public Function()
        {

        }

        public Function(string name, string url, string parentId, string iconCss, int orderNo, bool active)
        {
            Name = name;
            Url = url;
            ParentId = parentId;
            IconCss = iconCss;
            OrderNo = orderNo;
            Active = active;
        }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [StringLength(1000)]
        public string Url { get; set; }
        
        [StringLength(255)]
        public string ParentId { get; set; }

        [StringLength(255)]
        public string IconCss { get; set; }

        public int OrderNo { get; set; }
    }
}
