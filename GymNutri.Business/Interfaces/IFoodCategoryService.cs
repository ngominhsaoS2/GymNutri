using GymNutri.Data.Entities;
using GymNutri.Data.ViewModels.FoodRelated;
using GymNutri.Utilities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymNutri.Business.Interfaces
{
    public interface IFoodCategoryService : IDisposable
    {
        void Add(FoodCategory foodCategory, out bool result, out string message);

        void UpdateChangedProperties(FoodCategory foodCategory, out bool result, out string message);

        void Delete(int id, out bool result, out string message);

        void SoftDelete(int id, out bool result, out string message);

        void SaveChanges();

        FoodCategoryViewModel GetById(int id, out bool result, out string message);

        IEnumerable<FoodCategoryViewModel> GetAll(out bool result, out string message);

        PagedResult<FoodCategory> GetAllPaging(string keyword, string sortBy, int page, int pageSize, out bool result, out string message);

        void AddImages(int foodCategoryId, string[] images, out bool result, out string message);

        IEnumerable<FoodCategoryImage> GetImages(int foodCategoryId);

    }
}
