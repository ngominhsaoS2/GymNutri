using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GymNutri.Authorization;
using GymNutri.Business.Dapper.Interfaces;
using GymNutri.Business.Interfaces;
using GymNutri.Data.Entities;
using GymNutri.Data.ViewModels.Customer;
using GymNutri.Extensions;
using GymNutri.Utilities.Constants;
using GymNutri.Utilities.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GymNutri.Areas.Admin.Controllers
{
    public class CustomerController : BaseController
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserService _userService;
        private readonly IUserBodyIndexService _userBodyIndexService;
        private readonly ILocationToGetOrderOfUserService _locationToGetOrderOfUserService;
        private readonly IUserDesireService _userDesireService;
        private readonly IFindBodyClassificationService _findBodyClassificationService;
        private readonly IBodyClassificationService _bodyClassificationService;

        public CustomerController(IAuthorizationService authorizationService,
            UserManager<AppUser> userManager,
            IUserService userService,
            IUserBodyIndexService userBodyIndexService,
            ILocationToGetOrderOfUserService locationToGetOrderOfUserService,
            IUserDesireService userDesireService,
            IFindBodyClassificationService findBodyClassificationService,
            IBodyClassificationService bodyClassificationService)
        {
            _authorizationService = authorizationService;
            _userManager = userManager;
            _userService = userService;
            _userBodyIndexService = userBodyIndexService;
            _locationToGetOrderOfUserService = locationToGetOrderOfUserService;
            _userDesireService = userDesireService;
            _findBodyClassificationService = findBodyClassificationService;
            _bodyClassificationService = bodyClassificationService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _authorizationService.AuthorizeAsync(User, "Customer", Operations.Read);
            if (result.Succeeded == false)
                return new RedirectResult("/Admin/Home/NoPermission");
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPaging(string keyword, string sortBy, int page, int pageSize)
        {
            var model = await _userService.GetAllPaging(CommonConstants.AppRole.CustomerRole, keyword, sortBy, page, pageSize);
            return new OkObjectResult(new GenericResult(true, model));
        }

        [HttpGet]
        public IActionResult GetAllPagingBodyIndexOfUser(string userId, string from, string to, int page, int pageSize)
        {
            DateTime? fromDate = null;
            DateTime? toDate = null;

            if (!string.IsNullOrEmpty(from))
                fromDate = DateTime.ParseExact(from, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (!string.IsNullOrEmpty(to))
                toDate = DateTime.ParseExact(to, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            var model = _userBodyIndexService.GetAllPaging(userId, fromDate, toDate, page, pageSize, out bool result, out string message);
            return new OkObjectResult(new GenericResult(result, message, model));
        }

        [HttpGet]
        public IActionResult GetAllPagingLocationOfUser(string userId, string keyword, int page, int pageSize)
        {
            var model = _locationToGetOrderOfUserService.GetAllPaging(userId, keyword, null, page, pageSize, out bool result, out string message);
            return new OkObjectResult(new GenericResult(result, message, model));
        }

        [HttpGet]
        public IActionResult GetAllPagingDesireOfUser(string userId, string keyword, int page, int pageSize)
        {
            var model = _userDesireService.GetAllPaging(userId, keyword, null, page, pageSize, out bool result, out string message);
            return new OkObjectResult(new GenericResult(result, message, model));
        }

        [HttpGet]
        public IActionResult GetBodyIndexById(int id)
        {
            var model = _userBodyIndexService.GetById(id, out bool result, out string message);
            return new OkObjectResult(new GenericResult(result, message, model));
        }

        [HttpGet]
        public IActionResult GetLocationById(int id)
        {
            var model = _locationToGetOrderOfUserService.GetById(id, out bool result, out string message);
            return new OkObjectResult(new GenericResult(result, message, model));
        }

        [HttpGet]
        public IActionResult GetDesireById(int id)
        {
            var model = _userDesireService.GetById(id, out bool result, out string message);
            return new OkObjectResult(new GenericResult(result, message, model));
        }

        [HttpPost]
        public async Task<IActionResult> SaveBodyIndex(UserBodyIndex userBodyIndex)
        {
            bool result = true;
            string message = string.Empty;
            var loggedEmail = User.GetSpecificClaim("Email");
            var customer = await _userManager.FindByIdAsync(userBodyIndex.UserId.ToString());
            
            if (ModelState.IsValid)
            {
                userBodyIndex.UserModified = loggedEmail;

                if (userBodyIndex.Id == 0)
                {
                    userBodyIndex.UserCreated = loggedEmail;
                    _userBodyIndexService.Add(userBodyIndex, customer, out result, out message);
                }
                else
                    _userBodyIndexService.UpdateChangedProperties(userBodyIndex, customer, out result, out message);

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
        public IActionResult SaveLocation(LocationToGetOrderOfUser locationToGetOrderOfUser)
        {
            bool result = true;
            string message = string.Empty;
            var userEmail = User.GetSpecificClaim("Email");

            if (ModelState.IsValid)
            {
                locationToGetOrderOfUser.UserModified = userEmail;

                if (locationToGetOrderOfUser.Id == 0)
                {
                    locationToGetOrderOfUser.UserCreated = userEmail;
                    _locationToGetOrderOfUserService.Add(locationToGetOrderOfUser, out result, out message);
                }
                else
                    _locationToGetOrderOfUserService.UpdateChangedProperties(locationToGetOrderOfUser, out result, out message);

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
        public IActionResult SaveDesire(UserDesire userDesire)
        {
            bool result = true;
            string message = string.Empty;
            var userEmail = User.GetSpecificClaim("Email");

            if (ModelState.IsValid)
            {
                userDesire.UserModified = userEmail;

                if (userDesire.Id == 0)
                {
                    userDesire.UserCreated = userEmail;
                    _userDesireService.Add(userDesire, out result, out message);
                }
                else
                    _userDesireService.UpdateChangedProperties(userDesire, out result, out message);

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
        public IActionResult DeleteBodyIndex(int id)
        {
            _userBodyIndexService.SoftDelete(id, out bool result, out string message);
            return new OkObjectResult(new GenericResult(result, message));
        }

        [HttpPost]
        public IActionResult DeleteLocation(int id)
        {
            _locationToGetOrderOfUserService.SoftDelete(id, out bool result, out string message);
            return new OkObjectResult(new GenericResult(result, message));
        }

        [HttpPost]
        public IActionResult DeleteDesire(int id)
        {
            _userDesireService.SoftDelete(id, out bool result, out string message);
            return new OkObjectResult(new GenericResult(result, message));
        }

        [HttpGet]
        public async Task<IActionResult> CalculateBodyClassification(int userBodyIndexId, string group)
        {
            GenericResult check = new GenericResult();
            string foundIds = string.Empty;

            var bodyClassifications = _bodyClassificationService.GetAll(out bool result, out string message);

            foreach (var bodyClassification in bodyClassifications)
            {
                check = await _findBodyClassificationService.FindBodyClassification(userBodyIndexId, bodyClassification.Id);
                if (check.Success)
                {
                    if ((bool)check.Data)
                        foundIds += ";" + bodyClassification.Id.ToString();
                }
                else
                    break;
            }
            
            if (check.Success)
                foundIds = foundIds.Substring(1); // Loại bỏ dấu ; đầu tiên
            
            return new OkObjectResult(new GenericResult(check.Success, check.Message, foundIds));
        }

    }
}