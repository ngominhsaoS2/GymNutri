using GymNutri.Business.Interfaces;
using GymNutri.Data.ViewModels.System;
using GymNutri.Extensions;
using GymNutri.Utilities.Constants;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GymNutri.Areas.Admin.Components
{
    public class SideBarViewComponent : ViewComponent
    {
        private IFunctionService _functionService;
        private IRoleService _roleService;

        public SideBarViewComponent(IFunctionService functionService, IRoleService roleService)
        {
            _functionService = functionService;
            _roleService = roleService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var roles = ((ClaimsPrincipal)User).GetSpecificClaim("Roles");
            List<FunctionViewModel> functions = new List<FunctionViewModel>();

            if (roles.Split(";").Contains(CommonConstants.AppRole.AdminRole))
            {
                functions = await _functionService.GetAll(string.Empty);
            }
            else
            {
                foreach (var item in roles.Split(";"))
                {
                    var role = await _roleService.GetByName(item);
                    if (role != null)
                    {
                        var permissions = _roleService.GetListFunctionWithRole((Guid)role.Id);
                        foreach (var per in permissions)
                        {
                            if (!functions.Any(x => x.Id == per.FunctionId))
                            {
                                functions.Add(_functionService.GetById(per.FunctionId));
                            }
                        }
                    }
                }
            }

            return View(functions);
        }

    }
}