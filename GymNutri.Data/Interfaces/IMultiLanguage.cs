using System;
using System.Collections.Generic;
using System.Text;

namespace GymNutri.Data.Interfaces
{
    public interface IMultiLanguage<T>
    {
        T LanguageId { get; set; }
    }
}
