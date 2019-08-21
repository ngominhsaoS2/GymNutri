using AutoMapper;
using AutoMapper.QueryableExtensions;
using GymNutri.Business.Interfaces;
using GymNutri.Data.Entities;
using GymNutri.Data.ViewModels.Customer;
using GymNutri.Infrastructure.Interfaces;
using GymNutri.Utilities.Constants;
using GymNutri.Utilities.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymNutri.Business.InstantCalculation;

namespace GymNutri.Business.Implementation
{
    public class UserBodyIndexService : IUserBodyIndexService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<UserBodyIndex, int> _userBodyIndexRepository;
        private readonly IRepository<UserDesire, int> _userDesireRepository;
        private readonly IRepository<CommonCategory, int> _commonCategoryRepository;

        public UserBodyIndexService(IUnitOfWork unitOfWork,
            IRepository<UserBodyIndex, int> userBodyIndexRepository,
            IRepository<UserDesire, int> userDesireRepository,
            IRepository<CommonCategory, int> commonCategoryRepository)
        {
            _unitOfWork = unitOfWork;
            _userBodyIndexRepository = userBodyIndexRepository;
            _userDesireRepository = userDesireRepository;
            _commonCategoryRepository = commonCategoryRepository;
        }

        public void Add(UserBodyIndex userBodyIndex, AppUser user, out bool result, out string message)
        {
            userBodyIndex = AutoCalculateIndexes(userBodyIndex, user);
            _userBodyIndexRepository.Add(userBodyIndex, out result, out message);
            if (result)
                SaveChanges();
            else
                Dispose();
        }

        public void UpdateChangedProperties(UserBodyIndex userBodyIndex, AppUser user, out bool result, out string message)
        {
            userBodyIndex = AutoCalculateIndexes(userBodyIndex, user);
            _userBodyIndexRepository.UpdateChangedProperties(userBodyIndex.Id, userBodyIndex, out result, out message);
            if (result)
                SaveChanges();
            else
                Dispose();
        }

        public void Delete(int id, out bool result, out string message)
        {
            _userBodyIndexRepository.Remove(id, out result, out message);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public Task<List<UserBodyIndexViewModel>> GetAll(out bool result, out string message)
        {
            try
            {
                var query = _userBodyIndexRepository.FindAll(x => x.Active == true);
                result = true;
                message = CommonConstants.Message.Success;
                return query.OrderBy(x => x.Id).ProjectTo<UserBodyIndexViewModel>().ToListAsync();
            }
            catch (Exception ex)
            {
                result = false;
                message = ex.Message;
                return null;
            }
        }

        public PagedResult<UserBodyIndexViewModel> GetAllPaging(string userId, DateTime? fromDate, DateTime? toDate, int page, int pageSize, out bool result, out string message)
        {
            PagedResult<UserBodyIndexViewModel> paginationSet = new PagedResult<UserBodyIndexViewModel>()
            {
                CurrentPage = page,
                PageSize = pageSize,
                Results = null,
                RowCount = 0
            };

            try
            {
                var query = _userBodyIndexRepository.FindAll(x => x.UserId.ToString() == userId && x.Active == true);

                if (fromDate != null)
                    query = query.Where(x => x.Date >= fromDate.Value.Date);

                if (toDate != null)
                    query = query.Where(x => x.Date <= toDate.Value.Date);

                if (query.Any())
                    query = query.OrderByDescending(x => x.Date).ThenByDescending(x => x.Id);

                int totalRow = query.Count();

                query = query.Skip((page - 1) * pageSize).Take(pageSize);
                var data = query.ProjectTo<UserBodyIndexViewModel>().ToList();

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

        public UserBodyIndexViewModel GetById(int id, out bool result, out string message)
        {
            try
            {
                result = true;
                message = CommonConstants.Message.Success;
                return Mapper.Map<UserBodyIndex, UserBodyIndexViewModel>(_userBodyIndexRepository.FindById(id));
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
            _userBodyIndexRepository.SoftRemove(id, out result, out message);
            if (result)
                SaveChanges();
            else
                Dispose();
        }

        public UserBodyIndex AutoCalculateIndexes(UserBodyIndex userBodyIndex, AppUser user)
        {
            // Calculate Units
            userBodyIndex.WeightLb = userBodyIndex.WeightKg * CommonConstants.ExchangeUnits.KilogramsToPounds;
            userBodyIndex.BellyIn = userBodyIndex.BellyCm / CommonConstants.ExchangeUnits.InchesToCentimeters;

            // Calculate the IDI WPRO BMI index by having Weight (kg) and Height (m)
            userBodyIndex.IdiWproBmi = BodyIndexCalculation.GetIdiWproBmi(userBodyIndex.WeightKg, userBodyIndex.HeightCm / 100);

            // Calculate BodyFat by having Gender, Belly (inch) and Weight (lb)
            userBodyIndex.BodyFat = BodyIndexCalculation.GetBodyFat(user.Gender, userBodyIndex.BellyIn, userBodyIndex.WeightLb);

            // Calculate LBM by having WeightKg, BodyFat
            userBodyIndex.Lbm = BodyIndexCalculation.GetLbm(userBodyIndex.WeightKg, userBodyIndex.BodyFat);

            // Calculate BMR by having LBM
            userBodyIndex.Bmr = BodyIndexCalculation.GetBmr(userBodyIndex.Lbm);

            // Calculate TDEE after get the latest UserDesire of User
            decimal multiple = 0;
            var userDesire = _userDesireRepository.FindAll(x => x.UserId == user.Id).OrderByDescending(x => x.StartDate).FirstOrDefault();
            multiple = userDesire != null ? decimal.Parse(_commonCategoryRepository.FindSingle(x => x.GroupCode == CommonConstants.GroupCode.PracticeIntensive && x.Code == userDesire.PracticeIntensive).Description) : 1;
            userBodyIndex.Tdee = userBodyIndex.Bmr * multiple;

            return userBodyIndex;
        }
    }
}
