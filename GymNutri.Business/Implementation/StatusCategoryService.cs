using AutoMapper;
using AutoMapper.QueryableExtensions;
using GymNutri.Business.Interfaces;
using GymNutri.Data.ViewModels.Common;
using GymNutri.Data.Entities;

using GymNutri.Infrastructure.Interfaces;
using GymNutri.Utilities.Constants;
using GymNutri.Utilities.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GymNutri.Business.Implementation
{
    public class StatusCategoryService : IStatusCategoryService
    {
        private readonly IRepository<StatusCategory, int> _statusCategoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public StatusCategoryService(IRepository<StatusCategory, int> statusCategoryRepository,
            IUnitOfWork unitOfWork)
        {
            _statusCategoryRepository = statusCategoryRepository;
            _unitOfWork = unitOfWork;
        }

        public Task<List<StatusCategoryViewModel>> GetAll(string filter, out bool result, out string message)
        {
            try
            {
                var query = _statusCategoryRepository.FindAll(x => x.Active == true);
                if (!string.IsNullOrEmpty(filter))
                    query = query.Where(x => x.Name.Contains(filter));
                result = true;
                message = CommonConstants.Message.Success;
                return query.OrderBy(x => x.Id).ProjectTo<StatusCategoryViewModel>().ToListAsync();
            }
            catch(Exception ex)
            {
                result = false;
                message = ex.Message;
                return null;
            }
        }

        public StatusCategoryViewModel GetById(int id, out bool result, out string message)
        {
            try
            {
                result = true;
                message = CommonConstants.Message.Success;
                return Mapper.Map<StatusCategory, StatusCategoryViewModel>(_statusCategoryRepository.FindById(id));
            }
            catch(Exception ex)
            {
                result = false;
                message = ex.Message;
                return null;
            }
        }

        public void Add(StatusCategory statusCategory, out bool result, out string message)
        {
            _statusCategoryRepository.Add(statusCategory, out result, out message);
            if (result)
                SaveChanges();
            else
                Dispose();
        }

        public void UpdateChangedProperties(StatusCategory statusCategory, out bool result, out string message)
        {
            _statusCategoryRepository.UpdateChangedProperties(statusCategory.Id, statusCategory, out result, out message);
            if (result)
                SaveChanges();
            else
                Dispose();
        }

        public void SoftDelete(int id, out bool result, out string message)
        {
            _statusCategoryRepository.SoftRemove(id, out result, out message);
            if (result)
                SaveChanges();
            else
                Dispose();
        }
        
        public void Delete(int id, out bool result, out string message)
        {
            _statusCategoryRepository.Remove(id, out result, out message);
        }

        public void SaveChanges()
        {
            _unitOfWork.SaveChanges();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public PagedResult<StatusCategoryViewModel> GetAllPaging(string table, string keyword, string sortBy, int page, int pageSize, out bool result, out string message)
        {
            PagedResult<StatusCategoryViewModel> paginationSet = new PagedResult<StatusCategoryViewModel>()
            {
                CurrentPage = page,
                PageSize = pageSize,
                Results = null,
                RowCount = 0
            };

            try
            {
                var query = _statusCategoryRepository.FindAll(x => x.Active == true);

                if (!string.IsNullOrEmpty(keyword))
                    query = query.Where(x => x.Id.ToString().Contains(keyword) || x.Code.Contains(keyword) || x.Name.Contains(keyword)
                    || x.Table.Contains(keyword) || x.Description.Contains(keyword));

                if (!string.IsNullOrEmpty(table))
                    query = query.Where(x => x.Table == table);

                if (query.Any())
                {
                    if (!string.IsNullOrEmpty(sortBy))
                    {
                        var sortProperty = query.First().GetType().GetProperty(sortBy, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                        if (sortProperty != null)
                            query = query.OrderBy(e => sortProperty.GetValue(e, null));
                    }
                    else
                        query = query.OrderBy(x => x.Table).ThenBy(x => x.OrderNo).ThenBy(x => x.Id);
                }

                int totalRow = query.Count();

                query = query.Skip((page - 1) * pageSize).Take(pageSize);
                var data = query.ProjectTo<StatusCategoryViewModel>().ToList();
                
                paginationSet.Results = data;
                paginationSet.RowCount = totalRow;

                result = true;
                message = CommonConstants.Message.Success;
            }
            catch(Exception ex)
            {
                result = false;
                message = ex.Message;
            }

            return paginationSet;
        }

        public List<StatusCategoryViewModel> GetAllTable(out bool result, out string message)
        {
            try
            {
                var query = _statusCategoryRepository.FindAll(x => x.Active == true);
                var listTables = query.GroupBy(g => new { g.Table })
                                      .Select(g => g.First())
                                      .ProjectTo<StatusCategoryViewModel>()
                                      .ToList();
                result = true;
                message = CommonConstants.Message.Success;
                return listTables;
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
