using GymNutri.Data.ViewModels.System;
using GymNutri.Utilities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GymNutri.Business.Interfaces
{
    public interface IRoleService
    {
        Task<bool> AddAsync(AppRoleViewModel userVm);

        Task UpdateAsync(AppRoleViewModel userVm);

        Task DeleteAsync(Guid id);

        Task<List<AppRoleViewModel>> GetAllAsync();

        PagedResult<AppRoleViewModel> GetAllPaging(string keyword, string sortBy, int page, int pageSize, out bool result, out string message);

        Task<AppRoleViewModel> GetById(Guid id);

        Task<AppRoleViewModel> GetByName(string name);

        List<PermissionViewModel> GetListFunctionWithRole(Guid roleId);

        void SavePermission(List<PermissionViewModel> permissions, Guid roleId, out bool result, out string message);

        Task<bool> CheckPermission(string functionId, string action, string[] roles);
    }
}
