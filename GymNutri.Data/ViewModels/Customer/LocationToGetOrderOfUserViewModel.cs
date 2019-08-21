using System;
using System.Collections.Generic;
using System.Text;

namespace GymNutri.Data.ViewModels.Customer
{
    public class LocationToGetOrderOfUserViewModel
    {
        public int Id { get; set; }

        public bool Active { get; set; }

        public Guid UserId { get; set; }

        public string Address { get; set; }

        public TimeSpan FirstFrom { get; set; }

        public TimeSpan FirstTo { get; set; }

        public TimeSpan? SecondFrom { get; set; }

        public TimeSpan? SecondTo { get; set; }

        public TimeSpan? ThirdFrom { get; set; }

        public TimeSpan? ThirdTo { get; set; }

        public string Description { get; set; }

        public string UserCreated { get; set; }

        public string UserModified { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }
    }
}
