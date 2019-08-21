using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GymNutri.Business.Interfaces;
using GymNutri.Data.ViewModels.Common;
using GymNutri.Data.Entities;
using GymNutri.Infrastructure.Interfaces;
using GymNutri.Utilities.Dtos;

namespace GymNutri.Business.Implementation
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Feedback, long> _feedbackRepository;
        
        public FeedbackService(IUnitOfWork unitOfWork,
            IRepository<Feedback, long> feedbackRepository)
        {
            _unitOfWork = unitOfWork;
            _feedbackRepository = feedbackRepository;
        }

        public void Add(FeedbackViewModel feedbackVm, out bool result, out string message)
        {
            var page = Mapper.Map<FeedbackViewModel, Feedback>(feedbackVm);
            _feedbackRepository.Add(page, out result, out message);
        }

        public void Delete(int id, out bool result, out string message)
        {
            _feedbackRepository.Remove(id, out result, out message);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public List<FeedbackViewModel> GetAll()
        {
            return _feedbackRepository.FindAll().ProjectTo<FeedbackViewModel>().ToList();
        }

        public PagedResult<FeedbackViewModel> GetAllPaging(string keyword, int page, int pageSize)
        {
            var query = _feedbackRepository.FindAll();
            if (!string.IsNullOrEmpty(keyword))
                query = query.Where(x => x.Name.Contains(keyword));

            int totalRow = query.Count();
            var data = query.OrderByDescending(x => x.DateCreated)
                .Skip((page - 1) * pageSize)
                .Take(pageSize);

            var paginationSet = new PagedResult<FeedbackViewModel>()
            {
                Results = data.ProjectTo<FeedbackViewModel>().ToList(),
                CurrentPage = page,
                RowCount = totalRow,
                PageSize = pageSize
            };

            return paginationSet;
        }

        public FeedbackViewModel GetById(int id)
        {
            return Mapper.Map<Feedback, FeedbackViewModel>(_feedbackRepository.FindById(id));
        }

        public void SaveChanges()
        {
            _unitOfWork.SaveChanges();
        }

        public void Update(FeedbackViewModel feedbackVm, out bool result, out string message)
        {
            var page = Mapper.Map<FeedbackViewModel, Feedback>(feedbackVm);
            _feedbackRepository.Update(page, out result, out message);
        }
    }
}