using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymNutri.Business.Interfaces;
using GymNutri.Data.ViewModels.System;
using GymNutri.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using GymNutri.Utilities.Dtos;
using GymNutri.Extensions;

namespace GymNutri.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IAuthorizationService _authorizationService;

        public UserController(IUserService userService, IAuthorizationService authorizationService)
        {
            _userService = userService;
            _authorizationService = authorizationService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _authorizationService.AuthorizeAsync(User, "User", Operations.Read);
            if (result.Succeeded == false)
                return new RedirectResult("/Admin/Home/NoPermission");
            return View();
        }
        
        [HttpGet]
        public async Task<IActionResult> GetById(string id)
        {
            var model = await _userService.GetById(id);
            return new OkObjectResult(new GenericResult(true, model));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPaging(string roleName, string keyword, string sortBy, int page, int pageSize)
        {
            var model = await _userService.GetAllPaging(roleName, keyword, sortBy, page, pageSize);
            return new OkObjectResult(new GenericResult(true, model));
        }

        [HttpPost]
        public async Task<IActionResult> SaveEntity(AppUserViewModel userVm)
        {
            if (ModelState.IsValid)
            {
                var userEmail = User.GetSpecificClaim("Email");
                userVm.UserModified = userEmail;

                if (userVm.Id == null)
                {
                    userVm.UserCreated = userEmail;
                    await _userService.AddAsync(userVm);
                }
                else
                {
                    await _userService.UpdateAsync(userVm);
                }
                return new OkObjectResult(userVm);
            }
            else
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return new OkObjectResult(new GenericResult(false, allErrors));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            if (ModelState.IsValid)
            {
                await _userService.DeleteAsync(id);
                return new OkObjectResult(new GenericResult(true));
            }
            else
            {
                List<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors).ToList();
                return new OkObjectResult(new GenericResult(false, allErrors));
            }
        }
    }
}