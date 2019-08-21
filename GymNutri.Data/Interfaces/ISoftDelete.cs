using System;
using System.Collections.Generic;
using System.Text;

namespace GymNutri.Data.Interfaces
{
    public interface ISoftDelete
    {
        bool Active { get; set; }
    }
}
