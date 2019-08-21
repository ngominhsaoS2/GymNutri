using GymNutri.Data.Entities;
using GymNutri.Data.ViewModels.FoodRelated;
using GymNutri.Utilities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymNutri.Business.Interfaces
{
    public interface IFoodService : IDisposable
    {
        void Add(Food food, out bool result, out string message);

        void UpdateChangedProperties(Food food, out bool result, out string message);

        void Delete(long id, out bool result, out string message);

        void SoftDelete(long id, out bool result, out string message);

        void SaveChanges();

        FoodViewModel GetById(long id, out bool result, out string message);

        IEnumerable<FoodViewModel> GetAll(out bool result, out string message);

        IEnumerable<FoodViewModel> GetTop(int max, out bool result, out string message);

        PagedResult<Food> GetAllPaging(int? categoryId, string keyword, string sortBy, int page, int pageSize, out bool result, out string message);

        void AddImages(long foodId, string[] images, out bool result, out string message);

        IEnumerable<FoodImage> GetImages(long foodId);
    }
}
