using AutoMapper.QueryableExtensions;
using GymNutri.Business.Interfaces;
using GymNutri.Data.Entities;
using GymNutri.Data.ViewModels.Template;
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
    public class TemplateMenuService : ITemplateMenuService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<TemplateMenu, int> _templateMenuRepository;
        private readonly IRepository<TemplateMenuSet, int> _templateMenuSetRepository;
        private readonly IRepository<TemplateMenuSetDetail, int> _templateMenuSetDetailRepository;
        private readonly IRepository<TemplateMenuForBodyClassification, int> _templateMenuForBodyClassification;
        private readonly IRepository<SetOfFood, int> _setOfFoodRepository;

        public TemplateMenuService(IUnitOfWork unitOfWork,
            IRepository<TemplateMenu, int> templateMenuRepository,
            IRepository<TemplateMenuSet, int> templateMenuSetRepository,
            IRepository<TemplateMenuSetDetail, int> templateMenuSetDetailRepository,
            IRepository<TemplateMenuForBodyClassification, int> templateMenuForBodyClassification,
            IRepository<SetOfFood, int> setOfFoodRepository)
        {
            _unitOfWork = unitOfWork;
            _templateMenuRepository = templateMenuRepository;
            _templateMenuSetRepository = templateMenuSetRepository;
            _templateMenuSetDetailRepository = templateMenuSetDetailRepository;
            _templateMenuForBodyClassification = templateMenuForBodyClassification;
            _setOfFoodRepository = setOfFoodRepository;
        }

        public void Add(TemplateMenu templateMenu, out bool result, out string message)
        {
            templateMenu = CalculateNutrition(templateMenu);
            _templateMenuRepository.Add(templateMenu, out result, out message);
            if (result)
                SaveChanges();
            else
                Dispose();
        }

        public void UpdateChangedProperties(TemplateMenu templateMenu, out bool result, out string message)
        {
            // Header
            templateMenu = CalculateNutrition(templateMenu);
            _templateMenuRepository.UpdateChangedProperties(templateMenu.Id, templateMenu, out result, out message);

            // TemplateMenuForBodyClassifications
            var existedBodies = _templateMenuForBodyClassification.FindAll(x => x.TemplateMenuId == templateMenu.Id &&
                                                                                x.InsertedSource.Contains(CommonConstants.InsertedResource.TemplateMenu));
            _templateMenuForBodyClassification.RemoveMultiple(existedBodies.ToList(), out result, out message);
            if (templateMenu.TemplateMenuForBodyClassifications != null)
            {
                var addedBodies = templateMenu.TemplateMenuForBodyClassifications.Where(x => x.Id == 0).ToList();
                templateMenu.TemplateMenuForBodyClassifications.Clear();
                foreach (var detail in addedBodies)
                {
                    detail.TemplateMenuId = templateMenu.Id;
                    _templateMenuForBodyClassification.Add(detail, out result, out message);
                }
            }
               
            // TemplateMenuSets
            var addedSets = templateMenu.TemplateMenuSets.Where(x => x.Id == 0).ToList(); // new details added
            var updatedSets = templateMenu.TemplateMenuSets.Where(x => x.Id != 0).ToList(); // get updated details
            var existedSets = _templateMenuSetRepository.FindAll(x => x.TemplateMenuId == templateMenu.Id); // existed details
            templateMenu.TemplateMenuSets.Clear();

            foreach (var set in updatedSets)
            {
                var templateMenuSetDetails = _templateMenuSetDetailRepository.FindAll(x => x.TemplateMenuSetId == set.Id).ToList();
                _templateMenuSetDetailRepository.RemoveMultiple(templateMenuSetDetails, out result, out message);
                _templateMenuSetRepository.Update(set, out result, out message);
            }
            
            foreach (var set in addedSets)
            {
                set.TemplateMenuId = templateMenu.Id;
                _templateMenuSetRepository.Add(set, out result, out message);
            }

            var delete = existedSets.Except(updatedSets).ToList();
            foreach (var set in delete)
            {
                var templateMenuSetDetails = _templateMenuSetDetailRepository.FindAll(x => x.TemplateMenuSetId == set.Id).ToList();
                _templateMenuSetDetailRepository.RemoveMultiple(templateMenuSetDetails, out result, out message);
                _templateMenuSetRepository.Remove(set, out result, out message);
            }
            
            if (result)
                SaveChanges();
            else
                Dispose();
        }

        public void SoftDelete(int id, out bool result, out string message)
        {
            _templateMenuRepository.SoftRemove(id, out result, out message);
            var templateMenuSets = _templateMenuSetRepository.FindAll(x => x.Active == true && x.TemplateMenuId == id);
            foreach (var set in templateMenuSets)
            {
                // Remove TemplateMenuSetDetail
                var templateMenuSetDetails = _templateMenuSetDetailRepository.FindAll(x => x.Active == true && x.TemplateMenuSetId == set.Id);
                foreach (var detail in templateMenuSetDetails)
                {
                    _templateMenuSetDetailRepository.SoftRemove(detail, out result, out message);
                    if (!result)
                        break;
                }
                // Remove TemplateMenuSet
                if (result)
                    _templateMenuSetRepository.SoftRemove(set, out result, out message);
            }
            
            if (result)
                SaveChanges();
            else
                Dispose();
        }

        public void Delete(int id, out bool result, out string message)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public PagedResult<TemplateMenu> GetAllPaging(string keyword, string sortBy, int page, int pageSize, out bool result, out string message)
        {
            PagedResult<TemplateMenu> paginationSet = new PagedResult<TemplateMenu>()
            {
                CurrentPage = page,
                PageSize = pageSize,
                Results = null,
                RowCount = 0
            };

            try
            {
                var query = _templateMenuRepository.FindAll(x => x.Active == true);

                if (!string.IsNullOrEmpty(keyword))
                    query = query.Where(x => x.Id.ToString().Contains(keyword) || x.Code.Contains(keyword) || x.Name.Contains(keyword) || x.Description.Contains(keyword));

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

        public TemplateMenuViewModel GetById(int id, out bool result, out string message)
        {
            try
            {
                result = true;
                message = CommonConstants.Message.Success;
                var query = _templateMenuRepository.FindAll(x => x.Active == true && x.Id == id).ProjectTo<TemplateMenuViewModel>().ToList().SingleOrDefault(); // Sửa lỗi Reference Circular
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

        public IEnumerable<TemplateMenu> GetAll(out bool result, out string message)
        {
            try
            {
                result = true;
                message = CommonConstants.Message.Success;
                return _templateMenuRepository.FindAll(x => x.Active == true).OrderBy(x => x.Name).ToList();
            }
            catch (Exception ex)
            {
                result = false;
                message = ex.Message;
                return null;
            }
        }

        public TemplateMenu CalculateNutrition(TemplateMenu templateMenu)
        {
            if (templateMenu.TemplateMenuSets.Any())
            {
                foreach (var set in templateMenu.TemplateMenuSets)
                {
                    foreach (var detail in set.TemplateMenuSetDetails)
                    {
                        var setOfFood = _setOfFoodRepository.FindById(detail.SetOfFoodId);
                        set.Fat += setOfFood.Fat;
                        set.SaturatedFat += setOfFood.SaturatedFat;
                        set.Carb += setOfFood.Carb;
                        set.Protein += setOfFood.Protein;
                        set.Kcal += setOfFood.Kcal;
                    }

                    templateMenu.FatAverage += set.Fat;
                    templateMenu.SaturatedFatAverage += set.SaturatedFat;
                    templateMenu.CarbAverage += set.Carb;
                    templateMenu.ProteinAverage += set.Protein;
                    templateMenu.KcalAverage += set.Kcal;
                }

                int countSet = templateMenu.TemplateMenuSets.Count();
                templateMenu.FatAverage = templateMenu.FatAverage / countSet;
                templateMenu.SaturatedFatAverage = templateMenu.SaturatedFatAverage / countSet;
                templateMenu.CarbAverage = templateMenu.CarbAverage / countSet;
                templateMenu.ProteinAverage = templateMenu.ProteinAverage / countSet;
                templateMenu.KcalAverage = templateMenu.KcalAverage / countSet;
            }

            return templateMenu;
        }
    }
}
