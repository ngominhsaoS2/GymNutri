using AutoMapper;
using AutoMapper.QueryableExtensions;
using GymNutri.Business.Interfaces;
using GymNutri.Data.ViewModels.System;
using GymNutri.Data.Entities;
using GymNutri.Utilities.Constants;
using GymNutri.Utilities.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GymNutri.Data.EF;
using System.Globalization;

namespace GymNutri.Business.Implementation
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _appDbContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public UserService(AppDbContext appDbContext,
            UserManager<AppUser> userManager,
            RoleManager<AppRole> roleManager)
        {
            _appDbContext = appDbContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<bool> AddAsync(AppUserViewModel userVm)
        {
            var user = new AppUser()
            {
                UserName = userVm.UserName,
                Avatar = userVm.Avatar,
                Email = userVm.Email,
                FullName = userVm.FullName,
                DateCreated = DateTime.Now,
                PhoneNumber = userVm.PhoneNumber,
                UserCreated = userVm.UserCreated,
                UserModified = userVm.UserModified,
                Active = userVm.Active
            };

            if (!string.IsNullOrEmpty(userVm.Birthday))
                user.Birthday = DateTime.ParseExact(userVm.Birthday, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            var result = await _userManager.CreateAsync(user, userVm.Password);
            if (result.Succeeded && userVm.Roles.Count > 0)
            {
                var appUser = await _userManager.FindByNameAsync(user.UserName);
                if (appUser != null)
                    await _userManager.AddToRolesAsync(appUser, userVm.Roles);
            }
            return true;
        }

        public async Task UpdateAsync(AppUserViewModel userVm)
        {
            var user = await _userManager.FindByIdAsync(userVm.Id.ToString());

            //Add new roles
            var currentRoles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.AddToRolesAsync(user, userVm.Roles.Except(currentRoles).ToArray());

            if (result.Succeeded)
            {
                //Remove current roles in db
                string[] needRemoveRoles = currentRoles.Except(userVm.Roles).ToArray();
                await RemoveRolesFromUser(user.Id.ToString(), needRemoveRoles);

                //Update user detail
                user.FullName = userVm.FullName;
                user.Active = userVm.Active;
                user.Email = userVm.Email;
                user.PhoneNumber = userVm.PhoneNumber;
                user.Avatar = userVm.Avatar;
                user.Gender = userVm.Gender;
                if (!string.IsNullOrEmpty(userVm.Birthday))
                    user.Birthday = DateTime.ParseExact(userVm.Birthday, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                else
                    user.Birthday = null;

                await _userManager.UpdateAsync(user);
            }

        }

        public async Task DeleteAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            await _userManager.DeleteAsync(user);
        }

        public async Task<List<AppUserViewModel>> GetAllAsync()
        {
            return await _userManager.Users.ProjectTo<AppUserViewModel>().ToListAsync();
        }

        public async Task<PagedResult<AppUserViewModel>> GetAllPaging(string roleName, string keyword, string sortBy, int page, int pageSize)
        {
            PagedResult<AppUserViewModel> paginationSet = new PagedResult<AppUserViewModel>()
            {
                CurrentPage = page,
                PageSize = pageSize,
                Results = null,
                RowCount = 0
            };

            //IEnumerable<AppUser> query;

            //if (!string.IsNullOrEmpty(roleName))
            //    query = await _userManager.GetUsersInRoleAsync(roleName) as IEnumerable<AppUser>;
            //else
            //    query = _userManager.Users; // Đoạn này đang có vấn đề, tạm hời lọc theo Role không hoạt động

            var query = _userManager.Users;

            if (!string.IsNullOrEmpty(keyword))
                query = query.Where(x => x.FullName.Contains(keyword) || x.UserName.Contains(keyword) || x.Email.Contains(keyword) || x.PhoneNumber.Contains(keyword));

            int totalRow = query.Count();
            query = query.Skip((page - 1) * pageSize).Take(pageSize);

            if (query.Any())
            {
                if (!string.IsNullOrEmpty(sortBy))
                {
                    var sortProperty = query.First().GetType().GetProperty(sortBy, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                    if (sortProperty != null)
                        query = query.OrderBy(e => sortProperty.GetValue(e, null));
                }
                else
                    query = query.OrderBy(x => x.Email).ThenBy(x => x.UserName);
            }
            
            var data = query.Select(x => new AppUserViewModel()
            {
                UserName = x.UserName,
                Avatar = x.Avatar,
                Birthday = x.Birthday.ToString(),
                Email = x.Email,
                FullName = x.FullName,
                Id = x.Id,
                PhoneNumber = x.PhoneNumber,
                Active = x.Active,
                DateCreated = x.DateCreated
            }).ToList();

            paginationSet.Results = data;
            paginationSet.RowCount = totalRow;

            return paginationSet;
        }

        public async Task<AppUserViewModel> GetById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var roles = await _userManager.GetRolesAsync(user);
            var userVm = Mapper.Map<AppUser, AppUserViewModel>(user);
            userVm.Roles = roles.ToList();
            return userVm;
        }
        
        public async Task RemoveRolesFromUser(string userId, string[] roles)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var roleIds = _roleManager.Roles.Where(x => roles.Contains(x.Name)).Select(x => x.Id).ToList();
            List<IdentityUserRole<Guid>> userRoles = new List<IdentityUserRole<Guid>>();
            foreach (var roleId in roleIds)
            {
                userRoles.Add(new IdentityUserRole<Guid> { RoleId = roleId, UserId = user.Id });
            }
            _appDbContext.UserRoles.RemoveRange(userRoles);
            await _appDbContext.SaveChangesAsync();
        }



    }
}
