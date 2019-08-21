using GymNutri.Data.ViewModels.System;
using GymNutri.Utilities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GymNutri.Business.Interfaces
{
    public interface IUserService
    {
        Task<bool> AddAsync(AppUserViewModel userVm);

        Task DeleteAsync(string id);

        Task<List<AppUserViewModel>> GetAllAsync();

        Task<PagedResult<AppUserViewModel>> GetAllPaging(string roleName, string keyword, string sortBy, int page, int pageSize);

        Task<AppUserViewModel> GetById(string id);
        
        Task UpdateAsync(AppUserViewModel userVm);
    }
}
