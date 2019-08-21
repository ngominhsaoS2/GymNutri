using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GymNutri.Data.Interfaces
{
    public interface ITracking
    {
        [StringLength(255)]
        string UserCreated { get; set; }

        [StringLength(255)]
        string UserModified { get; set; }

        DateTime DateCreated { get; set; }

        DateTime DateModified { get; set; }

    }
}
