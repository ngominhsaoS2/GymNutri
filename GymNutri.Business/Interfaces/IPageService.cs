using System;
using System.Collections.Generic;
using System.Text;
using GymNutri.Data.ViewModels.Blog;
using GymNutri.Utilities.Dtos;

namespace GymNutri.Business.Interfaces
{
    public interface IPageService : IDisposable
    {
        void Add(PageViewModel pageVm, out bool result, out string message);

        void Update(PageViewModel pageVm, out bool result, out string message);

        void Delete(int id, out bool result, out string message);

        void SaveChanges();

        List<PageViewModel> GetAll();

        PagedResult<PageViewModel> GetAllPaging(string keyword, int page, int pageSize);

        PageViewModel GetByAlias(string alias);

        PageViewModel GetById(int id);
        
    }
}