using GymNutri.Data.Entities;
using GymNutri.Data.ViewModels.Customer;
using GymNutri.Utilities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymNutri.Business.Interfaces
{
    public interface IUserDesireService : IDisposable
    {
        void Add(UserDesire userDesire, out bool result, out string message);

        void UpdateChangedProperties(UserDesire userDesire, out bool result, out string message);

        void Delete(int id, out bool result, out string message);

        void SoftDelete(int id, out bool result, out string message);

        void SaveChanges();

        UserDesireViewModel GetById(int id, out bool result, out string message);

        PagedResult<UserDesireViewModel> GetAllPaging(string userId, string keyword, string sortBy, int page, int pageSize, out bool result, out string message);
    }
}
