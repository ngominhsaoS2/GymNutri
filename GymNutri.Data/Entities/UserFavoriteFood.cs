using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using GymNutri.Data.Interfaces;
using GymNutri.Infrastructure.SharedKernel;

namespace GymNutri.Data.Entities
{
    [Table("UserFavoriteFoods")]
    public class UserFavoriteFood : DomainEntity<int>, ITracking, ISoftDelete
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public long FoodId { get; set; }

        [ForeignKey("FoodId")]
        public virtual Food Food { set; get; }

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