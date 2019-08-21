using GymNutri.Data.Entities;
using GymNutri.Data.ViewModels.Common;
using GymNutri.Utilities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GymNutri.Business.Interfaces
{
    public interface ICommonCategoryService : IDisposable
    {
        void Add(CommonCategory commonCategory, out bool result, out string message);

        void UpdateChangedProperties(CommonCategory commonCategory, out bool result, out string message);

        void Delete(int id, out bool result, out string message);

        void SoftDelete(int id, out bool result, out string message);

        void SaveChanges();

        int ImportExcel(string userName, string filePath, out bool result, out string message);
        
        CommonCategoryViewModel GetById(int id, out bool result, out string message);

        PagedResult<CommonCategoryViewModel> GetAllPaging(string groupCode, string keyword, string sortBy, int page, int pageSize, out bool result, out string message);

        IEnumerable<CommonCategoryViewModel> GetAllGroupCode(out bool result, out string message);

        IEnumerable<CommonCategoryViewModel> GetByGroupCode(string groupCode, out bool result, out string message);

    }
}
