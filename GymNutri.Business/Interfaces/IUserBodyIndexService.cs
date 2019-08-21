using GymNutri.Data.Entities;
using GymNutri.Data.ViewModels.Customer;
using GymNutri.Utilities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GymNutri.Business.Interfaces
{
    public interface IUserBodyIndexService : IDisposable
    {
        void Add(UserBodyIndex userBodyIndex, AppUser user, out bool result, out string message);

        void UpdateChangedProperties(UserBodyIndex userBodyIndex, AppUser user, out bool result, out string message);

        void Delete(int id, out bool result, out string message);

        void SoftDelete(int id, out bool result, out string message);

        void SaveChanges();

        UserBodyIndexViewModel GetById(int id, out bool result, out string message);

        Task<List<UserBodyIndexViewModel>> GetAll(out bool result, out string message);

        PagedResult<UserBodyIndexViewModel> GetAllPaging(string userId, DateTime? fromDate, DateTime? toDate, int page, int pageSize, out bool result, out string message);

        UserBodyIndex AutoCalculateIndexes(UserBodyIndex userBodyIndex, AppUser user);
    }
}
