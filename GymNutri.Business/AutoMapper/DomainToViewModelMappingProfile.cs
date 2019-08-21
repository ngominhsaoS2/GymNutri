using AutoMapper;
using GymNutri.Data.ViewModels.Blog;
using GymNutri.Data.ViewModels.Common;
using GymNutri.Data.ViewModels.FoodRelated;
using GymNutri.Data.ViewModels.System;
using GymNutri.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using GymNutri.Data.ViewModels.Customer;
using GymNutri.Data.ViewModels.Template;
using GymNutri.Data.ViewModels.Selling;

namespace GymNutri.Business.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Function, FunctionViewModel>();
            CreateMap<AppUser, AppUserViewModel>();
            CreateMap<AppRole, AppRoleViewModel>();
            CreateMap<Permission, PermissionViewModel>();
            CreateMap<Blog, BlogViewModel>().MaxDepth(2);
            CreateMap<BlogTag, BlogTagViewModel>().MaxDepth(2);
            CreateMap<Slide, SlideViewModel>().MaxDepth(2);
            CreateMap<SystemConfig, SystemConfigViewModel>().MaxDepth(2);
            CreateMap<Footer, FooterViewModel>().MaxDepth(2);
            CreateMap<Feedback, FeedbackViewModel>().MaxDepth(2);
            CreateMap<Contact, ContactViewModel>().MaxDepth(2);
            CreateMap<Page, PageViewModel>().MaxDepth(2);
            CreateMap<StatusCategory, StatusCategoryViewModel>().MaxDepth(2);
            CreateMap<CommonCategory, CommonCategoryViewModel>().MaxDepth(2);
            CreateMap<FoodCategory, FoodCategoryViewModel>().MaxDepth(2);
            CreateMap<Food, FoodViewModel>().MaxDepth(2);
            CreateMap<UserBodyIndex, UserBodyIndexViewModel>().MaxDepth(2);
            CreateMap<LocationToGetOrderOfUser, LocationToGetOrderOfUserViewModel>().MaxDepth(2);
            CreateMap<BodyClassification, BodyClassificationViewModel>().MaxDepth(2);
            CreateMap<UserDesire, UserDesireViewModel>().MaxDepth(2);
            CreateMap<SetOfFood, SetOfFoodViewModel>().MaxDepth(2);
            CreateMap<FoodsInSet, FoodsInSetViewModel>().MaxDepth(2);
            CreateMap<Meal, MealViewModel>().MaxDepth(2);
            CreateMap<TemplateMenu, TemplateMenuViewModel>().MaxDepth(2);
            CreateMap<TemplateMenuSet, TemplateMenuSetViewModel>().MaxDepth(2);
            CreateMap<TemplateMenuSetDetail, TemplateMenuSetDetailViewModel>().MaxDepth(2);
            CreateMap<TemplateMenuForBodyClassification, TemplateMenuForBodyClassificationViewModel>().MaxDepth(2);
            CreateMap<MemberCard, MemberCardViewModel>().MaxDepth(2);
        }
    }
}