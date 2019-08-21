using GymNutri.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GymNutri.Data.ViewModels.System
{
    public class FunctionViewModel
    {
        [Column(TypeName = "varchar(255)")]
        [StringLength(255)]
        public string Id { get; set; }

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

        public bool Active { get; set; }
    }
}
