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
    public class MealController : BaseController
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IMealService _mealService;

        public MealController(IAuthorizationService authorizationService,
            IMealService mealService)
        {
            _authorizationService = authorizationService;
            _mealService = mealService;
        }
        
        public async Task<IActionResult> Index()
        {
            var result = await _authorizationService.AuthorizeAsync(User, "Meal", Operations.Read);
            if (result.Succeeded == false)
                return new RedirectResult("/Admin/Home/NoPermission");
            return View();
        }

        [HttpGet]
        public IActionResult GetAllPaging(string groupCode, string keyword, string sortBy, int page, int pageSize)
        {
            var model = _mealService.GetAllPaging(groupCode, keyword, sortBy, page, pageSize, out bool result, out string message);
            return new OkObjectResult(new GenericResult(result, message, model));
        }
        
        [HttpGet]
        public IActionResult GetAllGroupCode()
        {
            var model = _mealService.GetAllGroupCode(out bool result, out string message);
            return new OkObjectResult(new GenericResult(result, message, model));
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var model = _mealService.GetAll(out bool result, out string message);
            return new OkObjectResult(new GenericResult(result, message, model));
        }

        [HttpGet]
        public IActionResult GetAllToGroupedData()
        {
            GroupedData groupedData;
            var data = new List<GroupedData>();
            var groups = _mealService.GetAllGroupCode(out bool result, out string message);

            foreach(var group in groups)
            {
                groupedData = new GroupedData();
                groupedData.text = group.GroupCode.ToUpper();

                var children = _mealService.GetByGroupCode(group.GroupCode, out result, out message);
                foreach(var child in children)
                {
                    groupedData.children.Add(new ChildItem() { id = child.Id.ToString(), text = child.Name });
                }

                data.Add(groupedData);
            }
            
            return new OkObjectResult(new GenericResult(result, message, data));
        }

        [HttpGet]
        public IActionResult GetById(int id)
        {
            var model = _mealService.GetById(id, out bool result, out string message);
            return new OkObjectResult(new GenericResult(result, message, model));
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _mealService.SoftDelete(id, out bool result, out string message);
            return new OkObjectResult(new GenericResult(result, message));
        }

        [HttpPost]
        public IActionResult SaveEntity(Meal entity)
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
                    _mealService.Add(entity, out result, out message);
                }
                else
                    _mealService.UpdateChangedProperties(entity, out result, out message);

                return new OkObjectResult(new GenericResult(result, message));
            }
            else
            {
                List<string> listErrors = ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage).ToList();
                string errorString = String.Join("\n", listErrors.ToArray());
                return new OkObjectResult(new GenericResult(false, errorString));
            }
        }

    }
}