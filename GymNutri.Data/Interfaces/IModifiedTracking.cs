using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GymNutri.Data.Interfaces
{
    public interface IModifiedTracking
    {
        [StringLength(255)]
        string UserModified { get; set; }

        DateTime DateModified { get; set; }
    }
}
