using System;
using System.Collections.Generic;
using System.Text;

namespace GymNutri.Utilities.Constants
{
    public class CommonConstants
    {
        public const string DefaultFooterId = "DefaultFooterId";

        public const string DefaultContactId = "default";

        public const string CartSession = "CartSession";

        public class Message
        {
            public const string Error = "Error";

            public const string Success = "Success";

            public const string NoData = "There is no data";

            public const string Existed = "The record is already existed";

            public const string WrongExcelExtension = "Only file with .xlsx extension accepted";
        }

        public class AppRole
        {
            public const string AdminRole = "Admin";

            public const string StaffRole = "Staff";

            public const string ManagerRole = "Manager";

            public const string CustomerRole = "Customer";
        }

        public class TagType
        {
            public const string ProductTag = "Product";

            public const string BlogTag = "Blog";

            public const string FoodCategoryTag = "FoodCategory";

            public const string FoodTag = "Food";
        }
        
        public class UserClaims
        {
            public const string Roles = "Roles";
        }

        public class InsertedResource
        {
            public const string BodyClassification = "BodyClassification";

            public const string TemplateMenu = "TemplateMenu";
        }
        
        public class ExchangeUnits
        {
            public const decimal InchesToCentimeters = (decimal)2.54;

            public const decimal KilogramsToPounds = (decimal)2.20462262;
        }

        public class Gender
        {
            public const string Male = "male";

            public const string Female = "female";
        }

        public class GroupCode
        {
            public const string PracticeIntensive = "PracticeIntensive";
        }

    }
}
