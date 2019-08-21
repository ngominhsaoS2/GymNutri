using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using GymNutri.Data.Interfaces;
using GymNutri.Infrastructure.SharedKernel;

namespace GymNutri.Data.Entities
{
    [Table("Slides")]
    public class Slide : DomainEntity<int>, ITracking, ISoftDelete
    {
        [StringLength(255)]
        [Required]
        public string Name { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        [StringLength(1000)]
        [Required]
        public string Image { get; set; }

        [StringLength(1000)]
        public string Url { get; set; }

        public int? DisplayOrder { get; set; }
        
        public string Content { get; set; }

        [StringLength(25)]
        [Required]
        public string GroupAlias { get; set; }

        [StringLength(255)]
        public string UserCreated { get; set; }

        [StringLength(255)]
        public string UserModified { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }
        
    }
}
