using GymNutri.Data.ViewModels.System;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GymNutri.Business.Interfaces
{
    public interface IFunctionService : IDisposable
    {
        void Add(FunctionViewModel function, out bool result, out string message);

        void Update(FunctionViewModel function, out bool result, out string message);

        void Delete(string id, out bool result, out string message);

        void SaveChanges();

        Task<List<FunctionViewModel>> GetAll(string filter);

        IEnumerable<FunctionViewModel> GetAllWithParentId(string parentId);

        FunctionViewModel GetById(string id);
        
        bool CheckExistedId(string id);

        void UpdateParentId(string sourceId, string targetId, Dictionary<string, int> items);

        void ReOrder(string sourceId, string targetId);

    }
}
