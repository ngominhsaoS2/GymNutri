using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymNutri.Authorization;
using GymNutri.Business.Interfaces;
using GymNutri.Data.Entities;
using GymNutri.Extensions;
using GymNutri.Utilities.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymNutri.Areas.Admin.Controllers
{
    public class TemplateMenuController : BaseController
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly ITemplateMenuService _templateMenuService;

        public TemplateMenuController(IAuthorizationService authorizationService,
            ITemplateMenuService templateMenuService)
        {
            _authorizationService = authorizationService;
            _templateMenuService = templateMenuService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _authorizationService.AuthorizeAsync(User, "TemplateMenu", Operations.Read);
            if (result.Succeeded == false)
                return new RedirectResult("/Admin/Home/NoPermission");
            return View();
        }

        [HttpGet]
        public IActionResult GetAllPaging(string keyword, string sortBy, int page, int pageSize)
        {
            var model = _templateMenuService.GetAllPaging(keyword, sortBy, page, pageSize, out bool result, out string message);
            return new OkObjectResult(new GenericResult(result, message, model));
        }

        [HttpGet]
        public IActionResult GetById(int id)
        {
            var setOfFood = _templateMenuService.GetById(id, out bool result, out string message);
            return new OkObjectResult(new GenericResult(result, message, setOfFood));
        }

        [HttpPost]
        public IActionResult SaveEntity(TemplateMenu entity)
        {
            bool result = true;
            string message = string.Empty;
            var userEmail = User.GetSpecificClaim("Email");

            if (ModelState.IsValid)
            {
                entity.UserModified = userEmail;
                if (entity.Id == 0)
                {
                    entity.UserCreated = userEmail;
                    _templateMenuService.Add(entity, out result, out message);
                }
                else
                    _templateMenuService.UpdateChangedProperties(entity, out result, out message);

                return new OkObjectResult(new GenericResult(result, message));
            }
            else
            {
                List<string> listErrors = ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage).ToList();
                string errorString = String.Join("\n", listErrors.ToArray());
                return new OkObjectResult(new GenericResult(false, errorString));
            }
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _templateMenuService.SoftDelete(id, out bool result, out string message);
            return new OkObjectResult(new GenericResult(result, message));
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var data = _templateMenuService.GetAll(out bool result, out string message);
            return new OkObjectResult(new GenericResult(result, message, data));
        }

    }
}