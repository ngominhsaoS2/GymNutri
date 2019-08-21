using AutoMapper;
using GymNutri.Data.ViewModels.Blog;
using GymNutri.Data.ViewModels.Common;
using GymNutri.Data.ViewModels.FoodRelated;
using GymNutri.Data.ViewModels.System;
using GymNutri.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymNutri.Business.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<FunctionViewModel, Function>()
                .ConstructUsing(c => new Function(c.Name, c.Url, c.ParentId, c.IconCss, c.OrderNo, c.Active));

            CreateMap<AppUserViewModel, AppUser>()
                .ConstructUsing(c => new AppUser(c.Id.GetValueOrDefault(Guid.Empty), c.FullName, c.UserName,
                c.Email, c.PhoneNumber, c.Avatar, c.Active));

            CreateMap<PermissionViewModel, Permission>()
                .ConstructUsing(c => new Permission(c.RoleId, c.FunctionId, c.CanCreate, c.CanRead, c.CanUpdate, c.CanDelete));
            
            CreateMap<ContactViewModel, Contact>()
                .ConstructUsing(c => new Contact(c.Id, c.Name, c.Phone, c.Email, c.Website, c.Address, c.Other, c.Lng, c.Lat, c.Active));

            CreateMap<FeedbackViewModel, Feedback>()
                .ConstructUsing(c => new Feedback(c.Id, c.Name, c.Email, c.Message, c.Active));

            CreateMap<PageViewModel, Page>()
                .ConstructUsing(c => new Page(c.Id, c.Name, c.Alias, c.Content, c.Active));

            CreateMap<StatusCategoryViewModel, StatusCategory>()
                .ConstructUsing(c => new StatusCategory(c.Code, c.Name, c.OrderNo, c.Table, c.Description, c.Color, c.ShowInAdmin, c.ShowInClient, c.Active));

            CreateMap<CommonCategoryViewModel, CommonCategory>()
                .ConstructUsing(c => new CommonCategory(c.Code, c.Name, c.GroupCode, c.OrderNo, c.Description, c.Active));

            CreateMap<FoodCategoryViewModel, FoodCategory>()
                .ConstructUsing(c => new FoodCategory(c.Active, c.ParentId, c.Code, c.Name, c.FoodTypeCode, c.Description, c.OrderNo, c.Image, c.Tags, c.SeoPageTitle,
                c.SeoAlias, c.SeoKeywords, c.SeoDescription));

        }
    }
}