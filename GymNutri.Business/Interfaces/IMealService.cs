using GymNutri.Data.Entities;
using GymNutri.Data.ViewModels.FoodRelated;
using GymNutri.Utilities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymNutri.Business.Interfaces
{
    public interface IMealService : IDisposable
    {
        void Add(Meal meal, out bool result, out string message);

        void UpdateChangedProperties(Meal meal, out bool result, out string message);

        void Delete(int id, out bool result, out string message);

        void SoftDelete(int id, out bool result, out string message);

        void SaveChanges();

        MealViewModel GetById(int id, out bool result, out string message);

        PagedResult<Meal> GetAllPaging(string groupCode, string keyword, string sortBy, int page, int pageSize, out bool result, out string message);

        IEnumerable<Meal> GetAllGroupCode(out bool result, out string message);

        IEnumerable<MealViewModel> GetAll(out bool result, out string message);

        IEnumerable<MealViewModel> GetByGroupCode(string groupCode, out bool result, out string message);
    }
}
