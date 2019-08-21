using AutoMapper;
using AutoMapper.QueryableExtensions;
using GymNutri.Business.Interfaces;
using GymNutri.Data.ViewModels.Common;
using GymNutri.Data.Entities;
using GymNutri.Infrastructure.Interfaces;
using GymNutri.Utilities.Constants;
using GymNutri.Utilities.Dtos;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GymNutri.Business.Implementation
{
    public class CommonCategoryService : ICommonCategoryService
    {
        private readonly IRepository<CommonCategory, int>  _commonCategoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CommonCategoryService(IRepository<CommonCategory, int> commonCategoryRepository,
            IUnitOfWork unitOfWork)
        {
            _commonCategoryRepository = commonCategoryRepository;
            _unitOfWork = unitOfWork;
        }

        public void Add(CommonCategory commonCategory, out bool result, out string message)
        {
            var existed = _commonCategoryRepository.CheckExist(commonCategory, out int foundId, out result, out message,
                x => x.GroupCode == commonCategory.GroupCode && x.Code == commonCategory.Code && x.Active == true);
            if (!existed)
            {
                _commonCategoryRepository.Add(commonCategory, out result, out message);
                if (result)
                    SaveChanges();
                else
                    Dispose();
            }
            else
            {
                result = false;
                message = CommonConstants.Message.Existed;
            }
        }

        public void Delete(int id, out bool result, out string message)
        {
            _commonCategoryRepository.Remove(id, out result, out message);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        
        public PagedResult<CommonCategoryViewModel> GetAllPaging(string groupCode, string keyword, string sortBy, int page, int pageSize, out bool result, out string message)
        {
            PagedResult<CommonCategoryViewModel> paginationSet = new PagedResult<CommonCategoryViewModel>()
            {
                CurrentPage = page,
                PageSize = pageSize,
                Results = null,
                RowCount = 0
            };

            try
            {
                var query = _commonCategoryRepository.FindAll(x => x.Active == true);

                if (!string.IsNullOrEmpty(keyword))
                    query = query.Where(x => x.Id.ToString().Contains(keyword) || x.Code.Contains(keyword) || x.Name.Contains(keyword)
                    || x.GroupCode.Contains(keyword) || x.Description.Contains(keyword));

                if (!string.IsNullOrEmpty(groupCode))
                    query = query.Where(x => x.GroupCode == groupCode);

                if (query.Any())
                {
                    if (!string.IsNullOrEmpty(sortBy))
                    {
                        var sortProperty = query.First().GetType().GetProperty(sortBy, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                        if (sortProperty != null)
                            query = query.OrderBy(e => sortProperty.GetValue(e, null));
                    }
                    else
                        query = query.OrderBy(x => x.GroupCode).ThenBy(x => x.OrderNo).ThenBy(x => x.Id);
                }
                    
                int totalRow = query.Count();

                query = query.Skip((page - 1) * pageSize).Take(pageSize);
                var data = query.ProjectTo<CommonCategoryViewModel>().ToList();

                paginationSet.Results = data;
                paginationSet.RowCount = totalRow;

                result = true;
                message = CommonConstants.Message.Success;
            }
            catch (Exception ex)
            {
                result = false;
                message = ex.Message;
            }

            return paginationSet;
        }
        
        public CommonCategoryViewModel GetById(int id, out bool result, out string message)
        {
            try
            {
                result = true;
                message = CommonConstants.Message.Success;
                return Mapper.Map<CommonCategory, CommonCategoryViewModel>(_commonCategoryRepository.FindById(id));
            }
            catch (Exception ex)
            {
                result = false;
                message = ex.Message;
                return null;
            }
        }

        public int ImportExcel(string userName, string filePath, out bool result, out string message)
        {
            result = false;
            message = string.Empty;
            int count = 0;

            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                ExcelWorksheet workSheet = package.Workbook.Worksheets[1];
                int startIndex = workSheet.Dimension.Start.Row;
                int endIndex = workSheet.Dimension.End.Row;

                while (string.IsNullOrEmpty(workSheet.Cells[endIndex, 1].Value.ToString()))
                {
                    endIndex = endIndex - 1;
                }
                
                CommonCategory row;
                for (int i = startIndex + 1; i <= endIndex; i++)
                {
                    row = new CommonCategory();
                    row.GroupCode = workSheet.Cells[i, 1].Value != null ? workSheet.Cells[i, 1].Value.ToString() : "";
                    row.Code = workSheet.Cells[i, 2].Value != null ? workSheet.Cells[i, 2].Value.ToString() : "";
                    row.Name = workSheet.Cells[i, 3].Value != null ? workSheet.Cells[i, 3].Value.ToString() : "";
                    int.TryParse(workSheet.Cells[i, 4].Value != null ? workSheet.Cells[i, 4].Value.ToString() : "0", out var orderNo);
                    row.OrderNo = orderNo;
                    row.Description = workSheet.Cells[i, 5].Value != null ? workSheet.Cells[i, 5].Value.ToString() : "";
                    int.TryParse(workSheet.Cells[i, 6].Value != null ? workSheet.Cells[i, 6].Value.ToString() : "0", out var active);
                    row.Active = active == 1 ? true : false;
                    row.UserCreated = row.UserModified = userName;

                    if (_commonCategoryRepository.CheckExist(row, out int foundId, out result, out message, x => x.GroupCode == row.GroupCode && x.Code == row.Code && x.Active == true))
                        _commonCategoryRepository.UpdateChangedProperties(foundId, row, out result, out message);
                    else
                        _commonCategoryRepository.Add(row, out result, out message);

                    if (result == true)
                        count += 1;
                    else
                    {
                        count = 0;
                        break;
                    }  
                }

                if (result)
                    SaveChanges();
                else
                    Dispose();

                return count;
            }
        }

        public void SaveChanges()
        {
            _unitOfWork.SaveChanges();
        }

        public void SoftDelete(int id, out bool result, out string message)
        {
            _commonCategoryRepository.SoftRemove(id, out result, out message);
            if (result)
                SaveChanges();
            else
                Dispose();
        }

        public void UpdateChangedProperties(CommonCategory commonCategory, out bool result, out string message)
        {
            _commonCategoryRepository.UpdateChangedProperties(commonCategory.Id, commonCategory, out result, out message);
            if (result)
                SaveChanges();
            else
                Dispose();
        }

        public IEnumerable<CommonCategoryViewModel> GetAllGroupCode(out bool result, out string message)
        {
            try
            {
                var query = _commonCategoryRepository.FindAll(x => x.Active == true);
                var listGroupCode = query.GroupBy(g => new { g.GroupCode })
                                      .Select(g => g.First())
                                      .ProjectTo<CommonCategoryViewModel>()
                                      .ToList();
                result = true;
                message = CommonConstants.Message.Success;
                return listGroupCode;
            }
            catch (Exception ex)
            {
                result = false;
                message = ex.Message;
                return null;
            }
        }

        public IEnumerable<CommonCategoryViewModel> GetByGroupCode(string groupCode, out bool result, out string message)
        {
            try
            {
                var data = _commonCategoryRepository
                    .FindAll(x => x.Active == true && x.GroupCode == groupCode)
                    .ProjectTo<CommonCategoryViewModel>().ToList();
                result = true;
                message = CommonConstants.Message.Success;
                return data;
            }
            catch (Exception ex)
            {
                result = false;
                message = ex.Message;
                return null;
            }
        }


    }
}
