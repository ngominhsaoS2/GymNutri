using GymNutri.Data.Entities;
using GymNutri.Data.ViewModels.Selling;
using GymNutri.Utilities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymNutri.Business.Interfaces
{
    public interface IMemberCardService : IDisposable
    {
        void Add(MemberCard memberCard, out bool result, out string message);

        void UpdateChangedProperties(MemberCard memberCard, out bool result, out string message);

        void Delete(int id, out bool result, out string message);

        void SoftDelete(int id, out bool result, out string message);

        void SaveChanges();

        MemberCardViewModel GetById(int id, out bool result, out string message);

        PagedResult<MemberCardViewModel> GetAllPaging(string userId, DateTime? startDate, DateTime? endDate, string memberTypeCode,
            string keyword, string sortBy, int page, int pageSize, out bool result, out string message);

    }
}
