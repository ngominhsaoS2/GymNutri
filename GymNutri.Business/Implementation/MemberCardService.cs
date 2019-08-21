using AutoMapper.QueryableExtensions;
using GymNutri.Business.Interfaces;
using GymNutri.Data.Entities;
using GymNutri.Data.ViewModels.Selling;
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
    public class MemberCardService : IMemberCardService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<MemberCard, int> _memberCardRepository;

        public MemberCardService(IUnitOfWork unitOfWork,
            IRepository<MemberCard, int> memberCardRepository)
        {
            _unitOfWork = unitOfWork;
            _memberCardRepository = memberCardRepository;
        }

        public void Add(MemberCard memberCard, out bool result, out string message)
        {
            _memberCardRepository.Add(memberCard, out result, out message);
            if (result)
                SaveChanges();
            else
                Dispose();
        }

        public void Delete(int id, out bool result, out string message)
        {
            _memberCardRepository.Remove(id, out result, out message);
        }

        public PagedResult<MemberCardViewModel> GetAllPaging(string userId, DateTime? startDate, DateTime? endDate, string memberTypeCode,
            string keyword, string sortBy, int page, int pageSize, out bool result, out string message)
        {
            PagedResult<MemberCardViewModel> paginationSet = new PagedResult<MemberCardViewModel>()
            {
                CurrentPage = page,
                PageSize = pageSize,
                Results = null,
                RowCount = 0
            };

            try
            {
                var query = _memberCardRepository.FindAll(x => x.Active == true);

                if (!string.IsNullOrEmpty(userId))
                    query = query.Where(x => x.UserId.ToString() == userId);

                if (!string.IsNullOrEmpty(memberTypeCode))
                    query = query.Where(x => x.MemberTypeCode == memberTypeCode);

                if (startDate != null)
                    query = query.Where(x => x.StartDate >= startDate.Value.Date);

                if (endDate != null)
                    query = query.Where(x => x.EndDate <= endDate.Value.Date);

                if (!string.IsNullOrEmpty(keyword))
                    query = query.Where(x => x.Id.ToString().Contains(keyword) || x.MemberTypeCode.Contains(keyword) ||
                        x.StartDate.ToString("dd/MM/yyyy").Contains(keyword) || x.EndDate.ToString("dd/MM/yyyy").Contains(keyword));

                if (query.Any())
                {
                    if (!string.IsNullOrEmpty(sortBy))
                    {
                        var sortProperty = query.First().GetType().GetProperty(sortBy, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                        if (sortProperty != null)
                            query = query.OrderBy(e => sortProperty.GetValue(e, null));
                    }
                    else
                        query = query.OrderByDescending(x => x.DateCreated).OrderByDescending(x => x.StartDate).ThenByDescending(x => x.DateCreated);
                }

                int totalRow = query.Count();
                query = query.Skip((page - 1) * pageSize).Take(pageSize);
                var data = query.ProjectTo<MemberCardViewModel>().ToList();

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

        public MemberCardViewModel GetById(int id, out bool result, out string message)
        {
            try
            {
                result = true;
                message = CommonConstants.Message.Success;
                var query = _memberCardRepository.FindAll(x => x.Active == true && x.Id == id).ProjectTo<MemberCardViewModel>().ToList().SingleOrDefault(); // Sửa lỗi Reference Circular
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

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public void SoftDelete(int id, out bool result, out string message)
        {
            _memberCardRepository.SoftRemove(id, out result, out message);
            if (result)
                SaveChanges();
            else
                Dispose();
        }

        public void UpdateChangedProperties(MemberCard memberCard, out bool result, out string message)
        {
            _memberCardRepository.UpdateChangedProperties(memberCard.Id, memberCard, out result, out message);
            if (result)
                SaveChanges();
            else
                Dispose();
        }
    }
}
