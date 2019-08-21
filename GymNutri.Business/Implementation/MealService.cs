
using AutoMapper;
using AutoMapper.QueryableExtensions;
using GymNutri.Business.Interfaces;
using GymNutri.Data.Entities;
using GymNutri.Data.ViewModels.FoodRelated;
using GymNutri.Infrastructure.Interfaces;
using GymNutri.Utilities.Constants;
using GymNutri.Utilities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace GymNutri.Business.Implementation
{
    public class MealService : IMealService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Meal, int> _mealRepository;

        public MealService(IRepository<Meal, int> mealRepository,
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mealRepository = mealRepository;
        }

        public void Add(Meal meal, out bool result, out string message)
        {
            var existed = _mealRepository.CheckExist(meal, out int foundId, out result, out message, x => x.GroupCode == meal.GroupCode && x.Code == meal.Code && x.Active == true);
            if (!existed)
            {
                _mealRepository.Add(meal, out result, out message);
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
            _mealRepository.Remove(id, out result, out message);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public IEnumerable<MealViewModel> GetAll(out bool result, out string message)
        {
            try
            {
                result = true;
                message = CommonConstants.Message.Success;
                return _mealRepository.FindAll(x => x.Active == true).OrderBy(x => x.GroupCode).ThenBy(x => x.OrderNo).ProjectTo<MealViewModel>();
            }
            catch (Exception ex)
            {
                result = false;
                message = ex.Message;
                return null;
            }
        }

        public IEnumerable<MealViewModel> GetByGroupCode(string groupCode, out bool result, out string message)
        {
            try
            {
                result = true;
                message = CommonConstants.Message.Success;
                return _mealRepository.FindAll(x => x.Active == true && x.GroupCode == groupCode).OrderBy(x => x.GroupCode).ThenBy(x => x.OrderNo).ProjectTo<MealViewModel>();
            }
            catch (Exception ex)
            {
                result = false;
                message = ex.Message;
                return null;
            }
        }

        public IEnumerable<Meal> GetAllGroupCode(out bool result, out string message)
        {
            try
            {
                var query = _mealRepository.FindAll(x => x.Active == true);
                var listGroupCode = query.GroupBy(g => new { g.GroupCode })
                                      .Select(g => g.First())
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

        public PagedResult<Meal> GetAllPaging(string groupCode, string keyword, string sortBy, int page, int pageSize, out bool result, out string message)
        {
            PagedResult<Meal> paginationSet = new PagedResult<Meal>()
            {
                CurrentPage = page,
                PageSize = pageSize,
                Results = null,
                RowCount = 0
            };

            try
            {
                var query = _mealRepository.FindAll(x => x.Active == true);

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
                var data = query.ToList();

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

        public MealViewModel GetById(int id, out bool result, out string message)
        {
            try
            {
                result = true;
                message = CommonConstants.Message.Success;
                return Mapper.Map<Meal, MealViewModel>(_mealRepository.FindById(id));
            }
            catch (Exception ex)
            {
                result = false;
                message = ex.Message;
                return null;
            }
        }

        public void SaveChanges()
        {
            _unitOfWork.SaveChanges();
        }

        public void SoftDelete(int id, out bool result, out string message)
        {
            _mealRepository.SoftRemove(id, out result, out message);
            if (result)
                SaveChanges();
            else
                Dispose();
        }

        public void UpdateChangedProperties(Meal meal, out bool result, out string message)
        {
            _mealRepository.UpdateChangedProperties(meal.Id, meal, out result, out message);
            if (result)
                SaveChanges();
            else
                Dispose();
        }
    }
}
