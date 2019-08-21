using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using GymNutri.Authorization;
using GymNutri.Business.Interfaces;
using GymNutri.Data.Entities;
using GymNutri.Data.ViewModels.Common;
using GymNutri.Extensions;
using GymNutri.Utilities.Constants;
using GymNutri.Utilities.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace GymNutri.Areas.Admin.Controllers
{
    public class CommonCategoryController : BaseController
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IAuthorizationService _authorizationService;
        private readonly ICommonCategoryService _commonCategoryService;

        public CommonCategoryController(IHostingEnvironment hostingEnvironment,
            IAuthorizationService authorizationService,
            ICommonCategoryService commonCategoryService)
        {
            _hostingEnvironment = hostingEnvironment;
            _authorizationService = authorizationService;
            _commonCategoryService = commonCategoryService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _authorizationService.AuthorizeAsync(User, "CommonCategory", Operations.Read);
            if (result.Succeeded == false)
                return new RedirectResult("/Admin/Home/NoPermission");
            return View();
        }

        [HttpGet]
        public IActionResult GetAllPaging(string groupCode, string keyword, string sortBy, int page, int pageSize)
        {
            var model = _commonCategoryService.GetAllPaging(groupCode, keyword, sortBy, page, pageSize, out bool result, out string message);
            return new OkObjectResult(new GenericResult(result, message, model));
        }

        [HttpGet]
        public IActionResult GetAllGroupCode()
        {
            var model = _commonCategoryService.GetAllGroupCode(out bool result, out string message);
            return new OkObjectResult(new GenericResult(result, message, model));
        }

        [HttpGet]
        public IActionResult GetByGroupCode(string groupCode)
        {
            var model = _commonCategoryService.GetByGroupCode(groupCode, out bool result, out string message);
            return new OkObjectResult(new GenericResult(result, message, model));
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _commonCategoryService.SoftDelete(id, out bool result, out string message);
            return new OkObjectResult(new GenericResult(result, message));
        }

        [HttpGet]
        public IActionResult GetById(int id)
        {
            var model = _commonCategoryService.GetById(id, out bool result, out string message);
            return new OkObjectResult(new GenericResult(result, message, model));
        }

        [HttpPost]
        public IActionResult SaveEntity(CommonCategory entity)
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
                    _commonCategoryService.Add(entity, out result, out message);
                }
                else
                    _commonCategoryService.UpdateChangedProperties(entity, out result, out message);

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
        public IActionResult ImportExcel(IList<IFormFile> files)
        {
            var userEmail = User.GetSpecificClaim("Email");

            if (files != null && files.Count > 0)
            {
                var file = files[0];
                var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                string extension = Path.GetExtension(fileName);

                if (extension == ".xlsx")
                {
                    string folder = _hostingEnvironment.WebRootPath + $@"\uploaded\excel";
                    if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
                    
                    string filePath = Path.Combine(folder, fileName);
                    if (System.IO.File.Exists(filePath))
                    {
                        fileName = Path.GetFileNameWithoutExtension(fileName) + "_" + DateTime.Now.ToString("yyyyMMdd_hhmmss") + extension;
                        filePath = Path.Combine(folder, fileName);
                    }

                    using (FileStream fs = System.IO.File.Create(filePath))
                    {
                        file.CopyTo(fs);
                        fs.Flush();
                    }
                    
                    int count = _commonCategoryService.ImportExcel(userEmail, filePath, out bool result, out string message);

                    //SaoNM 08/01/2018 Xóa file khi import không thành công
                    if (result == false)
                        System.IO.File.Delete(filePath);

                    return new OkObjectResult(new GenericResult(result, message, count));
                }
                else
                {
                    return new OkObjectResult(new GenericResult(false, CommonConstants.Message.WrongExcelExtension));
                }
            }
            else
            {
                return new OkObjectResult(new GenericResult(false, CommonConstants.Message.NoData));
            }
        }

    }
}