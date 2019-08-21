using GymNutri.Authorization;
using GymNutri.Business.Interfaces;
using GymNutri.Data.Entities;
using GymNutri.Data.ViewModels.Common;
using GymNutri.Extensions;
using GymNutri.Utilities.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymNutri.Areas.Admin.Controllers
{
    public class StatusCategoryController : BaseController
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IStatusCategoryService _statusCategoryService;

        public StatusCategoryController(IAuthorizationService authorizationService,
            IStatusCategoryService statusCategoryService)
        {
            _authorizationService = authorizationService;
            _statusCategoryService = statusCategoryService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _authorizationService.AuthorizeAsync(User, "StatusCategory", Operations.Read);
            if (result.Succeeded == false)
                return new RedirectResult("/Admin/Home/NoPermission");
            return View();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var model = _statusCategoryService.GetAll(string.Empty, out bool result, out string message);
            return new OkObjectResult(new GenericResult(result, message, model));
        }

        [HttpGet]
        public IActionResult GetAllPaging(string table, string keyword, string sortBy, int page, int pageSize)
        {
            var model = _statusCategoryService.GetAllPaging(table, keyword, sortBy, page, pageSize, out bool result, out string message);
            return new OkObjectResult(new GenericResult(result, message, model));
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _statusCategoryService.SoftDelete(id, out bool result, out string message);
            return new OkObjectResult(new GenericResult(result, message));
        }

        [HttpGet]
        public IActionResult GetAllTable()
        {
            var model = _statusCategoryService.GetAllTable(out bool result, out string message);
            return new OkObjectResult(new GenericResult(result, message, model));
        }

        [HttpGet]
        public IActionResult GetById(int id)
        {
            var model = _statusCategoryService.GetById(id, out bool result, out string message);
            return new OkObjectResult(new GenericResult(result, message, model));
        }

        [HttpPost]
        public IActionResult SaveEntity(StatusCategory entity)
        {
            bool result = true;
            string message = string.Empty;
            var userEmail = User.GetSpecificClaim("Email");

            if (ModelState.IsValid)
            {
                entity.UserModified = userEmail;

                if (entity.Id == 0)
                {
                    entity.UserCreated = User.GetSpecificClaim("Email");
                    _statusCategoryService.Add(entity, out result, out message);
                }
                else
                    _statusCategoryService.UpdateChangedProperties(entity, out result, out message);

                return new OkObjectResult(new GenericResult(result, message));
            }
            else
            {
                List<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors).ToList();
                return new OkObjectResult(new GenericResult(false, allErrors));
            }
        }
    }
}
