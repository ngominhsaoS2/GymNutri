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
    public class BodyClassificationController : BaseController
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IBodyClassificationService _bodyClassificationService;

        public BodyClassificationController(IAuthorizationService authorizationService,
            IBodyClassificationService bodyClassificationService)
        {
            _authorizationService = authorizationService;
            _bodyClassificationService = bodyClassificationService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _authorizationService.AuthorizeAsync(User, "BodyClassification", Operations.Read);
            if (result.Succeeded == false)
                return new RedirectResult("/Admin/Home/NoPermission");
            return View();
        }

        [HttpGet]
        public IActionResult GetAllPaging(string groupCode, string keyword, string sortBy, int page, int pageSize)
        {
            var model = _bodyClassificationService.GetAllPaging(groupCode, keyword, sortBy, page, pageSize, out bool result, out string message);
            return new OkObjectResult(new GenericResult(result, message, model));
        }

        [HttpGet]
        public IActionResult GetAllGroupCode()
        {
            var model = _bodyClassificationService.GetAllGroupCode(out bool result, out string message);
            return new OkObjectResult(new GenericResult(result, message, model));
        }

        [HttpGet]
        public IActionResult GetById(int id)
        {
            var model = _bodyClassificationService.GetById(id, out bool result, out string message);
            return new OkObjectResult(new GenericResult(result, message, model));
        }

        [HttpPost]
        public IActionResult SaveEntity(BodyClassification entity)
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
                    _bodyClassificationService.Add(entity, out result, out message);
                }
                else
                    _bodyClassificationService.UpdateChangedProperties(entity, out result, out message);

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
            _bodyClassificationService.SoftDelete(id, out bool result, out string message);
            return new OkObjectResult(new GenericResult(result, message));
        }

        [HttpGet]
        public IActionResult GetAllToGroupedData()
        {
            GroupedData groupedData;
            var data = new List<GroupedData>();
            var groups = _bodyClassificationService.GetAllGroupCode(out bool result, out string message);

            foreach (var group in groups)
            {
                groupedData = new GroupedData();
                groupedData.text = group.GroupCode.ToUpper();

                var children = _bodyClassificationService.GetByGroupCode(group.GroupCode, out result, out message);
                foreach (var child in children)
                {
                    groupedData.children.Add(new ChildItem() { id = child.Id.ToString(), text = child.Name });
                }

                data.Add(groupedData);
            }

            return new OkObjectResult(new GenericResult(result, message, data));
        }

    }
}