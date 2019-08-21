using GymNutri.Data.Entities;
using GymNutri.Data.ViewModels.Customer;
using GymNutri.Utilities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymNutri.Business.Interfaces
{
    public interface IBodyClassificationService : IDisposable
    {
        void Add(BodyClassification bodyClassification, out bool result, out string message);

        void UpdateChangedProperties(BodyClassification bodyClassification, out bool result, out string message);

        void Delete(int id, out bool result, out string message);

        void SoftDelete(int id, out bool result, out string message);

        void SaveChanges();
        
        BodyClassificationViewModel GetById(int id, out bool result, out string message);
        
        PagedResult<BodyClassification> GetAllPaging(string groupCode, string keyword, string sortBy, int page, int pageSize, out bool result, out string message);

        IEnumerable<BodyClassificationViewModel> GetAllGroupCode(out bool result, out string message);

        IEnumerable<BodyClassificationViewModel> GetByGroupCode(string groupCode, out bool result, out string message);

        IEnumerable<BodyClassificationViewModel> GetAll(out bool result, out string message);
    }
}
