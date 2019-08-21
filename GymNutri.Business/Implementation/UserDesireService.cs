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
    public class UserDesireService : IUserDesireService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<UserDesire, int> _userDesireRepository;

        public UserDesireService(IUnitOfWork unitOfWork,
            IRepository<UserDesire, int> userDesireRepository)
        {
            _unitOfWork = unitOfWork;
            _userDesireRepository = userDesireRepository;
        }

        public void Add(UserDesire userDesire, out bool result, out string message)
        {
            _userDesireRepository.Add(userDesire, out result, out message);
            if (result)
                SaveChanges();
            else
                Dispose();
        }

        public void Delete(int id, out bool result, out string message)
        {
            _userDesireRepository.Remove(id, out result, out message);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public PagedResult<UserDesireViewModel> GetAllPaging(string userId, string keyword, string sortBy, int page, int pageSize, out bool result, out string message)
        {
            PagedResult<UserDesireViewModel> paginationSet = new PagedResult<UserDesireViewModel>()
            {
                CurrentPage = page,
                PageSize = pageSize,
                Results = null,
                RowCount = 0
            };

            try
            {
                var query = _userDesireRepository.FindAll(x => x.UserId.ToString() == userId && x.Active == true);

                if (!string.IsNullOrEmpty(keyword) && query.Any())
                    query = query.Where(x => x.DesireTypeCode.Contains(keyword) || x.Description.Contains(keyword) || x.Detail.Contains(keyword));

                if (query.Any())
                {
                    if (!string.IsNullOrEmpty(sortBy))
                    {
                        var sortProperty = query.First().GetType().GetProperty(sortBy, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                        if (sortProperty != null)
                            query = query.OrderBy(e => sortProperty.GetValue(e, null));
                    }
                    else
                        query = query.OrderByDescending(x => x.StartDate);
                }

                int totalRow = query.Count();

                query = query.Skip((page - 1) * pageSize).Take(pageSize);
                var data = query.ProjectTo<UserDesireViewModel>().ToList();

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

        public UserDesireViewModel GetById(int id, out bool result, out string message)
        {
            try
            {
                result = true;
                message = CommonConstants.Message.Success;
                return Mapper.Map<UserDesire, UserDesireViewModel>(_userDesireRepository.FindById(id));
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
            _userDesireRepository.SoftRemove(id, out result, out message);
            if (result)
                SaveChanges();
            else
                Dispose();
        }

        public void UpdateChangedProperties(UserDesire userDesire, out bool result, out string message)
        {
            _userDesireRepository.UpdateChangedProperties(userDesire.Id, userDesire, out result, out message);
            if (result)
                SaveChanges();
            else
                Dispose();
        }
    }
}
