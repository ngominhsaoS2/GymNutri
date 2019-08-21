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
    public class SetOfFoodService : ISetOfFoodService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<SetOfFood, int> _setOfFoodRepository;
        private readonly IRepository<FoodsInSet, long> _foodsInSetRepository;
        private readonly IRepository<Food, long> _foodRepository;

        public SetOfFoodService(IUnitOfWork unitOfWork,
            IRepository<SetOfFood, int> setOfFoodRepository,
            IRepository<FoodsInSet, long> foodsInSetRepository,
            IRepository<Food, long> foodRepository)
        {
            _unitOfWork = unitOfWork;
            _setOfFoodRepository = setOfFoodRepository;
            _foodsInSetRepository = foodsInSetRepository;
            _foodRepository = foodRepository;
        }

        public void Add(SetOfFood setOfFood, out bool result, out string message)
        {
            setOfFood = CalculateNutrition(setOfFood);
            _setOfFoodRepository.Add(setOfFood, out result, out message);
            if (result)
                SaveChanges();
            else
                Dispose();
        }
        
        public void UpdateChangedProperties(SetOfFood setOfFood, out bool result, out string message)
        {
            // Header
            setOfFood = CalculateNutrition(setOfFood);
            _setOfFoodRepository.UpdateChangedProperties(setOfFood.Id, setOfFood, out result, out message);

            // Detail
            var addedDetails = setOfFood.FoodsInSets.Where(x => x.Id == 0).ToList(); // new details added
            var updatedDetails = setOfFood.FoodsInSets.Where(x => x.Id != 0).ToList(); // get updated details
            var existedDetails = _foodsInSetRepository.FindAll(x => x.SetOfFoodId == setOfFood.Id); // existed details
            setOfFood.FoodsInSets.Clear();

            foreach (var detail in updatedDetails)
            {
                _foodsInSetRepository.UpdateChangedProperties(detail.Id, detail, out result, out message);
            }

            foreach (var detail in addedDetails)
            {
                detail.SetOfFoodId = setOfFood.Id;
                _foodsInSetRepository.Add(detail, out result, out message);
            }

            _foodsInSetRepository.RemoveMultiple(existedDetails.Except(updatedDetails).ToList(), out result, out message);

            if (result)
                SaveChanges();
            else
                Dispose();
        }
        
        public void SoftDelete(int id, out bool result, out string message)
        {
            _setOfFoodRepository.SoftRemove(id, out result, out message);

            var foodsInSet = _foodsInSetRepository.FindAll(x => x.Active == true && x.SetOfFoodId == id);
            foreach (var detail in foodsInSet)
            {
                _foodsInSetRepository.SoftRemove(detail, out result, out message);
                if (!result)
                    break;
            }

            if (result)
                SaveChanges();
            else
                Dispose();
        }

        public void Delete(int id, out bool result, out string message)
        {
            _setOfFoodRepository.Remove(id, out result, out message);

            var foodsInSet = _foodsInSetRepository.FindAll(x => x.Active == true && x.SetOfFoodId == id);
            foreach (var detail in foodsInSet)
            {
                _foodsInSetRepository.Remove(detail, out result, out message);
                if (!result)
                    break;
            }

            if (result)
                SaveChanges();
            else
                Dispose();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        
        public PagedResult<SetOfFood> GetAllPaging(string keyword, string sortBy, int page, int pageSize, out bool result, out string message)
        {
            PagedResult<SetOfFood> paginationSet = new PagedResult<SetOfFood>()
            {
                CurrentPage = page,
                PageSize = pageSize,
                Results = null,
                RowCount = 0
            };

            try
            {
                var query = _setOfFoodRepository.FindAll(x => x.Active == true);

                if (!string.IsNullOrEmpty(keyword))
                    query = query.Where(x => x.Id.ToString().Contains(keyword) || x.Code.Contains(keyword) || x.Name.Contains(keyword) || x.Description.Contains(keyword)
                    || x.ListFoodNames.Contains(keyword) || x.ListMealIds.Contains(keyword) || x.ListMealNames.Contains(keyword));

                if (query.Any())
                {
                    if (!string.IsNullOrEmpty(sortBy))
                    {
                        var sortProperty = query.First().GetType().GetProperty(sortBy, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                        if (sortProperty != null)
                            query = query.OrderBy(e => sortProperty.GetValue(e, null));
                    }
                    else
                        query = query.OrderByDescending(x => x.DateCreated).OrderBy(x => x.Name).ThenBy(x => x.Code).ThenBy(x => x.Id);
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

        public SetOfFoodViewModel GetById(int id, out bool result, out string message)
        {
            try
            {
                result = true;
                message = CommonConstants.Message.Success;
                var query = _setOfFoodRepository.FindAll(x => x.Active == true && x.Id == id).ProjectTo<SetOfFoodViewModel>().ToList().SingleOrDefault(); // Sửa lỗi Reference Circular
                return query;
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
        
        public SetOfFood CalculateNutrition(SetOfFood setOfFood)
        {
            if (setOfFood.FoodsInSets.Any())
            {
                foreach (var foodInSet in setOfFood.FoodsInSets)
                {
                    var food = _foodRepository.FindById(foodInSet.FoodId);
                    setOfFood.Fat += food.FatPerUnit * foodInSet.Quantity;
                    setOfFood.SaturatedFat += food.SaturatedFatPerUnit * foodInSet.Quantity;
                    setOfFood.Carb += food.CarbPerUnit * foodInSet.Quantity;
                    setOfFood.Protein += food.ProteinPerUnit * foodInSet.Quantity;
                    setOfFood.Kcal += food.KcalPerUnit * foodInSet.Quantity;
                }
            }

            return setOfFood;
        }
    }
}
