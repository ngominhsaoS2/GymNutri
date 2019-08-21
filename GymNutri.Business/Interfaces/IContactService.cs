using System;
using System.Collections.Generic;
using System.Text;
using GymNutri.Data.ViewModels.Common;
using GymNutri.Utilities.Dtos;

namespace GymNutri.Business.Interfaces
{
    public interface IContactService
    {
        void Add(ContactViewModel contactVm, out bool result, out string message);

        void Update(ContactViewModel contactVm, out bool result, out string message);

        void Delete(string id, out bool result, out string message);

        void SaveChanges();

        List<ContactViewModel> GetAll();

        PagedResult<ContactViewModel> GetAllPaging(string keyword, int page, int pageSize);

        ContactViewModel GetById(string id);
        
    }
}