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
    public class FoodController : BaseController
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IFoodService _foodService;

        public FoodController(IAuthorizationService authorizationService,
            IFoodService foodService)
        {
            _authorizationService = authorizationService;
            _foodService = foodService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _authorizationService.AuthorizeAsync(User, "Food", Operations.Read);
            if (result.Succeeded == false)
                return new RedirectResult("/Admin/Home/NoPermission");
            return View();
        }

        [HttpGet]
        public IActionResult GetAllPaging(int? categoryId, string keyword, string sortBy, int page, int pageSize)
        {
            var model = _foodService.GetAllPaging(categoryId, keyword, sortBy, page, pageSize, out bool result, out string message);
            return new OkObjectResult(new GenericResult(result, message, model));
        }

        [HttpGet]
        public IActionResult GetById(int id)
        {
            var model = _foodService.GetById(id, out bool result, out string message);
            return new OkObjectResult(new GenericResult(result, message, model));
        }

        [HttpPost]
        public IActionResult SaveEntity(Food entity)
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
                    _foodService.Add(entity, out result, out message);
                }
                else
                    _foodService.UpdateChangedProperties(entity, out result, out message);

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
            _foodService.SoftDelete(id, out bool result, out string message);
            return new OkObjectResult(new GenericResult(result, message));
        }

        [HttpPost]
        public IActionResult SaveImages(long foodId, string[] images)
        {
            _foodService.AddImages(foodId, images, out bool result, out string message);
            return new OkObjectResult(new GenericResult(result, message));
        }

        [HttpGet]
        public IActionResult GetImages(long foodId)
        {
            var images = _foodService.GetImages(foodId);
            return new OkObjectResult(new GenericResult(true, images));
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var model = _foodService.GetAll(out bool result, out string message);
            return new OkObjectResult(new GenericResult(result, message, model));
        }

        [HttpGet]
        public IActionResult GetTop(int max)
        {
            var model = _foodService.GetTop(max, out bool result, out string message);
            return new OkObjectResult(new GenericResult(result, message, model));
        }

    }
}