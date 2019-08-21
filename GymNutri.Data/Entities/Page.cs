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
    [Table("Pages")]
    public class Page : DomainEntity<int>, ISoftDelete
    {
        public Page() { }

        public Page(int id, string name, string alias, string content, bool active)
        {
            Id = id;
            Name = name;
            Alias = alias;
            Content = content;
            Active = active;
        }
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [MaxLength(255)]
        [Required]
        public string Alias { get; set; }

        public string Content { get; set; }
        
    }
}
