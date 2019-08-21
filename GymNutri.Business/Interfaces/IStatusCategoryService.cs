using GymNutri.Data.ViewModels.Common;
using GymNutri.Data.Entities;
using GymNutri.Utilities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GymNutri.Business.Interfaces
{
    public interface IStatusCategoryService : IDisposable
    {
        void Add(StatusCategory statusCategory, out bool result, out string message);

        void UpdateChangedProperties(StatusCategory statusCategory, out bool result, out string message);

        void Delete(int id, out bool result, out string message);

        void SoftDelete(int id, out bool result, out string message);

        void SaveChanges();

        Task<List<StatusCategoryViewModel>> GetAll(string filter, out bool result, out string message);

        StatusCategoryViewModel GetById(int id, out bool result, out string message);

        PagedResult<StatusCategoryViewModel> GetAllPaging(string table, string keyword, string sortBy, int page, int pageSize, out bool result, out string message);

        List<StatusCategoryViewModel> GetAllTable(out bool result, out string message);

    }
}
