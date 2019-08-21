using AutoMapper;
using AutoMapper.QueryableExtensions;
using GymNutri.Business.Interfaces;
using GymNutri.Data.Entities;
using GymNutri.Data.ViewModels.Customer;
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
    public class BodyClassificationService : IBodyClassificationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<BodyClassification, int> _bodyClassificationRepository;
        private readonly IRepository<TemplateMenuForBodyClassification, int> _templateMenuForBodyClassification;

        public BodyClassificationService(IUnitOfWork unitOfWork,
            IRepository<BodyClassification, int> bodyClassificationRepository,
            IRepository<TemplateMenuForBodyClassification, int> templateMenuForBodyClassification)
        {
            _unitOfWork = unitOfWork;
            _bodyClassificationRepository = bodyClassificationRepository;
            _templateMenuForBodyClassification = templateMenuForBodyClassification;
        }

        public void Add(BodyClassification bodyClassification, out bool result, out string message)
        {
            var existed = _bodyClassificationRepository.CheckExist(bodyClassification, out int foundId, out result, out message,
                x => x.GroupCode == bodyClassification.GroupCode && x.Code == bodyClassification.Code && x.Active == true);
            if (!existed)
            {
                _bodyClassificationRepository.Add(bodyClassification, out result, out message);
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
            _bodyClassificationRepository.Remove(id, out result, out message);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public IEnumerable<BodyClassificationViewModel> GetAllGroupCode(out bool result, out string message)
        {
            try
            {
                var query = _bodyClassificationRepository.FindAll(x => x.Active == true);
                var listGroupCode = query.GroupBy(g => new { g.GroupCode })
                                      .Select(g => g.First())
                                      .ProjectTo<BodyClassificationViewModel>()
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

        public IEnumerable<BodyClassificationViewModel> GetAll(out bool result, out string message)
        {
            try
            {
                result = true;
                message = CommonConstants.Message.Success;
                return _bodyClassificationRepository.FindAll(x => x.Active == true).OrderBy(x => x.GroupCode).ThenBy(x => x.Code).ProjectTo<BodyClassificationViewModel>();
            }
            catch (Exception ex)
            {
                result = false;
                message = ex.Message;
                return null;
            }
        }

        public PagedResult<BodyClassification> GetAllPaging(string groupCode, string keyword, string sortBy, int page, int pageSize, out bool result, out string message)
        {
            PagedResult<BodyClassification> paginationSet = new PagedResult<BodyClassification>()
            {
                CurrentPage = page,
                PageSize = pageSize,
                Results = null,
                RowCount = 0
            };

            try
            {
                var query = _bodyClassificationRepository.FindAll(x => x.Active == true);

                if (!string.IsNullOrEmpty(groupCode))
                    query = query.Where(x => x.GroupCode == groupCode);

                if (!string.IsNullOrEmpty(keyword))
                    query = query.Where(x => x.Id.ToString().Contains(keyword) || x.Code.Contains(keyword) || x.Name.Contains(keyword)
                    || x.GroupCode.Contains(keyword) || x.Description.Contains(keyword) || x.Detail.Contains(keyword));

                if (query.Any())
                {
                    if (!string.IsNullOrEmpty(sortBy))
                    {
                        var sortProperty = query.First().GetType().GetProperty(sortBy, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                        if (sortProperty != null)
                            query = query.OrderBy(e => sortProperty.GetValue(e, null));
                    }
                    else
                        query = query.OrderBy(x => x.GroupCode).ThenBy(x => x.Name).ThenBy(x => x.Name).ThenBy(x => x.Id);
                }

                int totalRow = query.Count();

                var data = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();

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

        public IEnumerable<BodyClassificationViewModel> GetByGroupCode(string groupCode, out bool result, out string message)
        {
            try
            {
                var data = _bodyClassificationRepository
                    .FindAll(x => x.Active == true && x.GroupCode == groupCode)
                    .ProjectTo<BodyClassificationViewModel>().ToList();
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

        public BodyClassificationViewModel GetById(int id, out bool result, out string message)
        {
            try
            {
                result = true;
                message = CommonConstants.Message.Success;
                var query = _bodyClassificationRepository.FindAll(x => x.Id == id).ProjectTo<BodyClassificationViewModel>().ToList().SingleOrDefault(); // Sửa lỗi Reference Circular
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

        public void SoftDelete(int id, out bool result, out string message)
        {
            _bodyClassificationRepository.SoftRemove(id, out result, out message);
            if (result)
                SaveChanges();
            else
                Dispose();
        }

        public void UpdateChangedProperties(BodyClassification bodyClassification, out bool result, out string message)
        {
            // Header
            _bodyClassificationRepository.UpdateChangedProperties(bodyClassification.Id, bodyClassification, out result, out message);

            // Detail
            var existedDetails = _templateMenuForBodyClassification.FindAll(x => x.BodyClassificationId == bodyClassification.Id &&
                                                                                x.InsertedSource.Contains(CommonConstants.InsertedResource.BodyClassification)); // existed details
            _templateMenuForBodyClassification.RemoveMultiple(existedDetails.ToList(), out result, out message);

            if (bodyClassification.TemplateMenuForBodyClassifications != null)
            {
                var addedDetails = bodyClassification.TemplateMenuForBodyClassifications.Where(x => x.Id == 0).ToList(); // new details added
                bodyClassification.TemplateMenuForBodyClassifications.Clear();

                foreach (var detail in addedDetails)
                {
                    detail.BodyClassificationId = bodyClassification.Id;
                    _templateMenuForBodyClassification.Add(detail, out result, out message);
                }
            }

            if (result)
                SaveChanges();
            else
                Dispose();
        }
    }
}
