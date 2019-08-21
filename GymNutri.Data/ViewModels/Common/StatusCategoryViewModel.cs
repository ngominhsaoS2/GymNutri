using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GymNutri.Data.ViewModels.Common
{
    public class StatusCategoryViewModel
    {
        public int Id { get; set; }

        public bool Active { get; set; }

        [StringLength(255)]
        public string Code { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        public int OrderNo { get; set; }
        
        [StringLength(255)]
        public string Table { get; set; }

        [StringLength(255)]
        public string Description { get; set; }
        
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
