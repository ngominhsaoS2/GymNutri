using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymNutri.Authorization;
using GymNutri.Business.Interfaces;
using GymNutri.Data.Entities;
using GymNutri.Data.ViewModels.FoodRelated;
using GymNutri.Extensions;
using GymNutri.Utilities.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace GymNutri.Areas.Admin.Controllers
{
    public class FoodCategoryController : BaseController
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IFoodCategoryService _foodCategoryService;

        public FoodCategoryController(IAuthorizationService authorizationService,
            IFoodCategoryService foodCategoryService)
        {
            _authorizationService = authorizationService;
            _foodCategoryService = foodCategoryService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _authorizationService.AuthorizeAsync(User, "FoodCategory", Operations.Read);
            if (result.Succeeded == false)
                return new RedirectResult("/Admin/Home/NoPermission");
            return View();
        }

        [HttpGet]
        public IActionResult GetAllPaging(string keyword, string sortBy, int page, int pageSize)
        {
            var model = _foodCategoryService.GetAllPaging(keyword, sortBy, page, pageSize, out bool result, out string message);
            return new OkObjectResult(new GenericResult(result, message, model));
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var model = _foodCategoryService.GetAll(out bool result, out string message);
            return new OkObjectResult(new GenericResult(result, message, model));
        }

        [HttpGet]
        public IActionResult GetById(int id)
        {
            var model = _foodCategoryService.GetById(id, out bool result, out string message);
            return new OkObjectResult(new GenericResult(result, message, model));
        }

        [HttpPost]
        public IActionResult SaveEntity(FoodCategory entity)
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
                    _foodCategoryService.Add(entity, out result, out message);
                }
                else
                    _foodCategoryService.UpdateChangedProperties(entity, out result, out message);

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
            _foodCategoryService.SoftDelete(id, out bool result, out string message);
            return new OkObjectResult(new GenericResult(result, message));
        }

        [HttpPost]
        public IActionResult SaveImages(int foodCategoryId, string[] images)
        {
            _foodCategoryService.AddImages(foodCategoryId, images, out bool result, out string message);
            return new OkObjectResult(new GenericResult(result, message));
        }

        [HttpGet]
        public IActionResult GetImages(int foodCategoryId)
        {
            var images = _foodCategoryService.GetImages(foodCategoryId);
            return new OkObjectResult(new GenericResult(true, images));
        }



    }
}