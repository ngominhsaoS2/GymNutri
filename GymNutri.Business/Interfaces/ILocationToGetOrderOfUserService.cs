using GymNutri.Data.Entities;
using GymNutri.Data.ViewModels.Customer;
using GymNutri.Utilities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymNutri.Business.Interfaces
{
    public interface ILocationToGetOrderOfUserService : IDisposable
    {
        void Add(LocationToGetOrderOfUser locationToGetOrderOfUser, out bool result, out string message);

        void UpdateChangedProperties(LocationToGetOrderOfUser locationToGetOrderOfUser, out bool result, out string message);

        void Delete(int id, out bool result, out string message);

        void SoftDelete(int id, out bool result, out string message);

        void SaveChanges();

        LocationToGetOrderOfUserViewModel GetById(int id, out bool result, out string message);

        PagedResult<LocationToGetOrderOfUserViewModel> GetAllPaging(string userId, string keyword, string sortBy, int page, int pageSize, out bool result, out string message);
    }
}
