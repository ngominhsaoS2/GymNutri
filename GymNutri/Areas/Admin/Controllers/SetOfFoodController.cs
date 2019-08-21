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
    public class SetOfFoodController : BaseController
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly ISetOfFoodService _setOfFoodService;

        public SetOfFoodController(IAuthorizationService authorizationService,
            ISetOfFoodService setOfFoodService)
        {
            _authorizationService = authorizationService;
            _setOfFoodService = setOfFoodService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _authorizationService.AuthorizeAsync(User, "SetOfFood", Operations.Read);
            if (result.Succeeded == false)
                return new RedirectResult("/Admin/Home/NoPermission");
            return View();
        }

        [HttpGet]
        public IActionResult GetAllPaging(string keyword, string sortBy, int page, int pageSize)
        {
            var model = _setOfFoodService.GetAllPaging(keyword, sortBy, page, pageSize, out bool result, out string message);
            return new OkObjectResult(new GenericResult(result, message, model));
        }

        [HttpGet]
        public IActionResult GetById(int id)
        {
            var setOfFood = _setOfFoodService.GetById(id, out bool result, out string message);
            return new OkObjectResult(new GenericResult(result, message, setOfFood));
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _setOfFoodService.SoftDelete(id, out bool result, out string message);
            return new OkObjectResult(new GenericResult(result, message));
        }

        [HttpPost]
        public IActionResult SaveEntity(SetOfFood entity, long[] listFoodIds, int[] listQuantities)
        {
            bool result = true;
            string message = string.Empty;
            var userEmail = User.GetSpecificClaim("Email");

            if (ModelState.IsValid)
            {
                entity.UserModified = userEmail;

                if(listFoodIds.Count() > 0)
                {
                    int i = 0;
                    foreach(var foodId in listFoodIds)
                    {
                        var foodInSet = new FoodsInSet() { FoodId = foodId, Quantity = listQuantities[i], Active = true, UserCreated = userEmail, UserModified = userEmail };
                        entity.FoodsInSets.Add(foodInSet);
                        i++;
                    }
                }

                if (entity.Id == 0)
                {
                    entity.UserCreated = userEmail;
                    _setOfFoodService.Add(entity, out result, out message);
                }
                else
                    _setOfFoodService.UpdateChangedProperties(entity, out result, out message);

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