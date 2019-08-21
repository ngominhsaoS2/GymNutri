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
    [Table("StatusCategories ")]
    public class StatusCategory : DomainEntity<int>, ISortable, ITracking, ISoftDelete
    {
        public StatusCategory()
        {

        }

        public StatusCategory(string code, string name, int orderNo, string table, string description, string color, bool showInAdmin, bool showInClient, bool active)
        {
            Code = code;
            Name = name;
            OrderNo = orderNo;
            Table = table;
            Description = description;
            Color = color;
            ShowInAdmin = showInAdmin;
            ShowInClient = showInClient;
            Active = active;
        }

        public StatusCategory(int id, string code, string name, int orderNo, string table, string description, string color, bool showInAdmin, bool showInClient, bool active)
        {
            Id = id;
            Code = code;
            Name = name;
            OrderNo = orderNo;
            Table = table;
            Description = description;
            Color = color;
            ShowInAdmin = showInAdmin;
            ShowInClient = showInClient;
            Active = active;
        }


        [Column(TypeName = "varchar(255)")]
        [StringLength(255)]
        [Required]
        public string Code { get; set; }

        [StringLength(255)]
        [Required]
        public string Name { get; set; }

        public int OrderNo { get; set; }

        [Column(TypeName = "varchar(255)")]
        [StringLength(255)]
        [Required]
        public string Table { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        [Column(TypeName = "varchar(255)")]
        [StringLength(255)]
        public string Color { get; set; }

        public bool ShowInAdmin { get; set; }

        public bool ShowInClient { get; set; }

        [StringLength(255)]
        public string UserCreated { get; set; }

        [StringLength(255)]
        public string UserModified { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }
    }
}