using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GymNutri.Data.Interfaces
{
    public interface ICreatedTracking
    {
        [StringLength(255)]
        string UserCreated { get; set; }

        DateTime DateCreated { get; set; }
    }
}
