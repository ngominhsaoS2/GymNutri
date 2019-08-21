using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GymNutri.Business.Interfaces;
using GymNutri.Data.ViewModels.Blog;
using GymNutri.Data.ViewModels.Common;
using GymNutri.Data.Entities;
using GymNutri.Infrastructure.Interfaces;
using GymNutri.Utilities.Constants;
using GymNutri.Utilities.Dtos;
using GymNutri.Utilities.Helpers;

namespace GymNutri.Business.Implementation
{
    public class CommonService : ICommonService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Footer, string> _footerRepository;
        private readonly IRepository<SystemConfig, string> _systemConfigRepository;
        private readonly IRepository<Slide, int> _slideRepository;

        public CommonService(IUnitOfWork unitOfWork,
            IRepository<Footer, string> footerRepository,
            IRepository<SystemConfig, string> systemConfigRepository,
            IRepository<Slide, int> slideRepository)
        {
            _footerRepository = footerRepository;
            _unitOfWork = unitOfWork;
            _systemConfigRepository = systemConfigRepository;
            _slideRepository = slideRepository;
        }

        public FooterViewModel GetFooter()
        {
            return Mapper.Map<Footer, FooterViewModel>(_footerRepository.FindSingle(x => x.Id ==
            CommonConstants.DefaultFooterId));
        }

        public List<SlideViewModel> GetSlides(string groupAlias)
        {
            return _slideRepository.FindAll(x => x.Active == true && x.GroupAlias == groupAlias)
                .ProjectTo<SlideViewModel>().ToList();
        }

        public SystemConfigViewModel GetSystemConfig(string code)
        {
            return Mapper.Map<SystemConfig, SystemConfigViewModel>(_systemConfigRepository.FindSingle(x => x.Id == code));
        }
    }
}
