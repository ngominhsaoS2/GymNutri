using GymNutri.Data.Interfaces;
using GymNutri.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using GymNutri.Data.Enums;

namespace GymNutri.Data.Entities
{
    [Table("Languages")]
    public class Language : DomainEntity<string>, ISoftDelete
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public bool IsDefault { get; set; }

        public string Resources { get; set; }
    }
}
