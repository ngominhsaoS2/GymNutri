using GymNutri.Utilities.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymNutri.Business.InstantCalculation
{
    public static class BodyIndexCalculation
    {
        public static decimal GetIdiWproBmi(decimal weightKg, decimal heightM)
        {
            if (heightM > 0)
                return Math.Round(weightKg / (heightM * heightM), 2);
            else
                return 0;
        }

        public static decimal GetBodyFat(string gender, decimal bellyIn, decimal weightLb)
        {
            if (gender == CommonConstants.Gender.Male)
                return 100 * ((decimal)-98.42 + ((decimal)4.15 * bellyIn) - ((decimal)0.082 * weightLb)) / weightLb;
            else
                return 100 * ((decimal)-76.76 + ((decimal)4.15 * bellyIn) - ((decimal)0.082 * weightLb)) / weightLb;
        }

        public static decimal GetLbm(decimal weightKg, decimal bodyFat)
        {
            return (weightKg * (100 - bodyFat)) / 100;
        }

        public static decimal GetBmr(decimal lbm)
        {
            return 370 + ((decimal)21.6 * lbm);
        }

    }
}
