using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using GymNutri.Data.Interfaces;
using GymNutri.Infrastructure.SharedKernel;

namespace GymNutri.Data.Entities
{
    [Table("LocationToGetOrderOfUsers")]
    public class LocationToGetOrderOfUser : DomainEntity<int>, ITracking, ISoftDelete
    {
        [Required]
        public Guid UserId { get; set; }

        [StringLength(500)]
        [Required]
        public string Address { get; set; }

        public TimeSpan FirstFrom { get; set; }

        public TimeSpan FirstTo { get; set; }

        public TimeSpan? SecondFrom { get; set; }

        public TimeSpan? SecondTo { get; set; }

        public TimeSpan? ThirdFrom { get; set; }

        public TimeSpan? ThirdTo { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        [StringLength(255)]
        public string UserCreated { get; set; }

        [StringLength(255)]
        public string UserModified { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }
        
    }
}