using GymNutri.Data.Entities;
using GymNutri.Data.ViewModels.Template;
using GymNutri.Utilities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymNutri.Business.Interfaces
{
    public interface ITemplateMenuService : IDisposable
    {
        void Add(TemplateMenu templateMenu, out bool result, out string message);

        void UpdateChangedProperties(TemplateMenu templateMenu, out bool result, out string message);

        void Delete(int id, out bool result, out string message);

        void SoftDelete(int id, out bool result, out string message);

        void SaveChanges();

        TemplateMenuViewModel GetById(int id, out bool result, out string message);

        PagedResult<TemplateMenu> GetAllPaging(string keyword, string sortBy, int page, int pageSize, out bool result, out string message);

        IEnumerable<TemplateMenu> GetAll(out bool result, out string message);

        TemplateMenu CalculateNutrition(TemplateMenu templateMenu);
    }
}
