using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GymNutri.Data.Interfaces
{
    public interface IHasNutrionProperties
    {
        [Column(TypeName = "decimal(18,5)")]
        decimal Fat { get; set; }

        [Column(TypeName = "decimal(18,5)")]
        decimal SaturatedFat { get; set; }

        [Column(TypeName = "decimal(18,5)")]
        decimal Carb { get; set; }

        [Column(TypeName = "decimal(18,5)")]
        decimal Protein { get; set; }

        [Column(TypeName = "decimal(18,5)")]
        decimal Kcal { get; set; }
    }
}
