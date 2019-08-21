using GymNutri.Data.Entities;
using GymNutri.Data.ViewModels.FoodRelated;
using GymNutri.Utilities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymNutri.Business.Interfaces
{
    public interface ISetOfFoodService : IDisposable
    {
        void Add(SetOfFood setOfFood, out bool result, out string message);

        void UpdateChangedProperties(SetOfFood setOfFood, out bool result, out string message);

        void Delete(int id, out bool result, out string message);

        void SoftDelete(int id, out bool result, out string message);

        void SaveChanges();

        SetOfFoodViewModel GetById(int id, out bool result, out string message);

        PagedResult<SetOfFood> GetAllPaging(string keyword, string sortBy, int page, int pageSize, out bool result, out string message);

        SetOfFood CalculateNutrition(SetOfFood setOfFood);
    }
}
