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
    public class LocationToGetOrderOfUserService : ILocationToGetOrderOfUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<LocationToGetOrderOfUser, int> _locationToGetOrderOfUserRepository;

        public LocationToGetOrderOfUserService(IUnitOfWork unitOfWork,
            IRepository<LocationToGetOrderOfUser, int> locationToGetOrderOfUserRepository)
        {
            _unitOfWork = unitOfWork;
            _locationToGetOrderOfUserRepository = locationToGetOrderOfUserRepository;
        }

        public void Add(LocationToGetOrderOfUser locationToGetOrderOfUser, out bool result, out string message)
        {
            _locationToGetOrderOfUserRepository.Add(locationToGetOrderOfUser, out result, out message);
            if (result)
                SaveChanges();
            else
                Dispose();
        }

        public void Delete(int id, out bool result, out string message)
        {
            _locationToGetOrderOfUserRepository.Remove(id, out result, out message);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public PagedResult<LocationToGetOrderOfUserViewModel> GetAllPaging(string userId, string keyword, string sortBy, int page, int pageSize, out bool result, out string message)
        {
            PagedResult<LocationToGetOrderOfUserViewModel> paginationSet = new PagedResult<LocationToGetOrderOfUserViewModel>()
            {
                CurrentPage = page,
                PageSize = pageSize,
                Results = null,
                RowCount = 0
            };

            try
            {
                var query = _locationToGetOrderOfUserRepository.FindAll(x => x.UserId.ToString() == userId && x.Active == true);

                if (!string.IsNullOrEmpty(keyword) && query.Any())
                    query = query.Where(x => x.Address.Contains(keyword) || x.Description.Contains(keyword));

                if (query.Any())
                {
                    if (!string.IsNullOrEmpty(sortBy))
                    {
                        var sortProperty = query.First().GetType().GetProperty(sortBy, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                        if (sortProperty != null)
                            query = query.OrderBy(e => sortProperty.GetValue(e, null));
                    }
                    else
                        query = query.OrderByDescending(x => x.Address).ThenByDescending(x => x.Id);
                }

                int totalRow = query.Count();

                query = query.Skip((page - 1) * pageSize).Take(pageSize);
                var data = query.ProjectTo<LocationToGetOrderOfUserViewModel>().ToList();

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

        public LocationToGetOrderOfUserViewModel GetById(int id, out bool result, out string message)
        {
            try
            {
                result = true;
                message = CommonConstants.Message.Success;
                return Mapper.Map<LocationToGetOrderOfUser, LocationToGetOrderOfUserViewModel>(_locationToGetOrderOfUserRepository.FindById(id));
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
            _locationToGetOrderOfUserRepository.SoftRemove(id, out result, out message);
            if (result)
                SaveChanges();
            else
                Dispose();
        }

        public void UpdateChangedProperties(LocationToGetOrderOfUser locationToGetOrderOfUser, out bool result, out string message)
        {
            _locationToGetOrderOfUserRepository.UpdateChangedProperties(locationToGetOrderOfUser.Id, locationToGetOrderOfUser, out result, out message);
            if (result)
                SaveChanges();
            else
                Dispose();
        }
    }
}
