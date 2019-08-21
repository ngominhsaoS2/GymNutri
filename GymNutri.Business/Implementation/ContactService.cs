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
    public class ContactService : IContactService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Contact, string> _contactRepository;
        
        public ContactService(IUnitOfWork unitOfWork,
            IRepository<Contact, string> contactRepository)
        {
            _unitOfWork = unitOfWork;
            _contactRepository = contactRepository;
        }

        public void Add(ContactViewModel pageVm, out bool result, out string message)
        {
            var page = Mapper.Map<ContactViewModel, Contact>(pageVm);
            _contactRepository.Add(page, out result, out message);
        }

        public void Delete(string id, out bool result, out string message)
        {
            _contactRepository.Remove(id, out result, out message);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public List<ContactViewModel> GetAll()
        {
            return _contactRepository.FindAll().ProjectTo<ContactViewModel>().ToList();
        }

        public PagedResult<ContactViewModel> GetAllPaging(string keyword, int page, int pageSize)
        {
            var query = _contactRepository.FindAll();
            if (!string.IsNullOrEmpty(keyword))
                query = query.Where(x => x.Name.Contains(keyword));

            int totalRow = query.Count();
            var data = query.OrderByDescending(x => x.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize);

            var paginationSet = new PagedResult<ContactViewModel>()
            {
                Results = data.ProjectTo<ContactViewModel>().ToList(),
                CurrentPage = page,
                RowCount = totalRow,
                PageSize = pageSize
            };

            return paginationSet;
        }

        public ContactViewModel GetById(string id)
        {
            return Mapper.Map<Contact, ContactViewModel>(_contactRepository.FindById(id));
        }

        public void SaveChanges()
        {
            _unitOfWork.SaveChanges();
        }

        public void Update(ContactViewModel pageVm, out bool result, out string message)
        {
            var page = Mapper.Map<ContactViewModel, Contact>(pageVm);
            _contactRepository.Update(page, out result, out message);
        }
    }
}