using System;
using System.Collections.Generic;
using System.Text;
using GymNutri.Data.ViewModels.Common;
using GymNutri.Utilities.Dtos;

namespace GymNutri.Business.Interfaces
{
    public interface IFeedbackService
    {
        void Add(FeedbackViewModel feedbackVm, out bool result, out string message);

        void Update(FeedbackViewModel feedbackVm, out bool result, out string message);

        void Delete(int id, out bool result, out string message);

        void SaveChanges();

        List<FeedbackViewModel> GetAll();

        PagedResult<FeedbackViewModel> GetAllPaging(string keyword, int page, int pageSize);

        FeedbackViewModel GetById(int id);
        
    }
}