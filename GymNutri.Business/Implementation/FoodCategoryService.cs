using AutoMapper;
using AutoMapper.QueryableExtensions;
using GymNutri.Business.Interfaces;
using GymNutri.Data.ViewModels.FoodRelated;
using GymNutri.Data.Entities;
using GymNutri.Infrastructure.Interfaces;
using GymNutri.Utilities.Constants;
using GymNutri.Utilities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using GymNutri.Utilities.Helpers;

namespace GymNutri.Business.Implementation
{
    public class FoodCategoryService : IFoodCategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<FoodCategory, int> _foodCategoryRepository;
        private readonly IRepository<FoodCategoryImage, int> _foodCategoryImageRepository;
        private readonly IRepository<Tag, string> _tagRepository;
        private readonly IRepository<FoodCategoryTag, int> _foodCategoryTagRepository;

        public FoodCategoryService(IUnitOfWork unitOfWork,
            IRepository<FoodCategory, int> foodCategoryRepository,
            IRepository<FoodCategoryImage, int> foodCategoryImageRepository,
            IRepository<Tag, string> tagRepository,
            IRepository<FoodCategoryTag, int> foodCategoryTagRepository)
        {
            _unitOfWork = unitOfWork;
            _foodCategoryRepository = foodCategoryRepository;
            _foodCategoryImageRepository = foodCategoryImageRepository;
            _tagRepository = tagRepository;
            _foodCategoryTagRepository = foodCategoryTagRepository;
        }

        public void Add(FoodCategory foodCategory, out bool result, out string message)
        {
            foodCategory.FoodCategoryTags = new List<FoodCategoryTag>();

            // Tags
            if (!string.IsNullOrEmpty(foodCategory.Tags))
            {
                string[] tags = foodCategory.Tags.Split(';');
                foreach (string t in tags)
                {
                    var tagId = TextHelper.ToUnsignString(t);
                    // Insert if there is a new tag
                    if (!_tagRepository.FindAll(x => x.Id == tagId).Any())
                    {
                        Tag tag = new Tag
                        {
                            Id = tagId,
                            Active = true,
                            Name = t.Trim(),
                            Type = CommonConstants.TagType.FoodCategoryTag,
                            UserCreated = foodCategory.UserCreated,
                            UserModified = foodCategory.UserModified
                        };
                        _tagRepository.Add(tag, out result, out message);
                    }

                    FoodCategoryTag foodCategoryTag = new FoodCategoryTag
                    {
                        TagId = tagId,
                        Active = true,
                        UserCreated = foodCategory.UserCreated,
                        UserModified = foodCategory.UserModified
                    };
                    foodCategory.FoodCategoryTags.Add(foodCategoryTag);
                }
            }

            // Insert FoodCategory
            _foodCategoryRepository.Add(foodCategory, out result, out message);

            if (result)
                SaveChanges();
            else
                Dispose();
        }

        public void UpdateChangedProperties(FoodCategory foodCategory, out bool result, out string message)
        {
            string oldTags = _foodCategoryRepository.FindById(foodCategory.Id).Tags;
            if(oldTags != foodCategory.Tags)
            {
                _foodCategoryTagRepository.RemoveMultiple(_foodCategoryTagRepository.FindAll(x => x.FoodCategoryId == foodCategory.Id).ToList(), out result, out message);

                if (!string.IsNullOrEmpty(foodCategory.Tags))
                {
                    string[] tags = foodCategory.Tags.Split(';');
                    foreach (string t in tags)
                    {
                        var tagId = TextHelper.ToUnsignString(t);
                        // Insert if there is a new tag
                        if (!_tagRepository.FindAll(x => x.Id == tagId).Any())
                        {
                            Tag tag = new Tag
                            {
                                Id = tagId,
                                Active = true,
                                Name = t.Trim(),
                                Type = CommonConstants.TagType.FoodCategoryTag,
                                UserCreated = foodCategory.UserCreated,
                                UserModified = foodCategory.UserModified
                            };
                            _tagRepository.Add(tag, out result, out message);
                        }

                        FoodCategoryTag foodCategoryTag = new FoodCategoryTag
                        {
                            FoodCategoryId = foodCategory.Id,
                            TagId = tagId,
                            Active = true,
                            UserCreated = foodCategory.UserCreated,
                            UserModified = foodCategory.UserModified
                        };
                        _foodCategoryTagRepository.Add(foodCategoryTag, out result, out message);
                    }
                } 
            }
            
            _foodCategoryRepository.UpdateChangedProperties(foodCategory.Id, foodCategory, out result, out message);
            if (result)
                SaveChanges();
            else
                Dispose();
        }

        public void AddImages(int foodCategoryId, string[] images, out bool result, out string message)
        {
            _foodCategoryImageRepository.RemoveMultiple(_foodCategoryImageRepository.FindAll(x => x.FoodCategoryId == foodCategoryId).ToList(), out result, out message);
            FoodCategoryImage foodCategoryImage;
            foreach (var image in images)
            {
                foodCategoryImage = new FoodCategoryImage();
                foodCategoryImage.FoodCategoryId = foodCategoryId;
                foodCategoryImage.Path = image;
                foodCategoryImage.Caption = string.Empty;
                foodCategoryImage.Active = true;
                _foodCategoryImageRepository.Add(foodCategoryImage, out result, out message);
                if (!result) break;
            }

            if (result)
                SaveChanges();
            else
                Dispose();
        }

        public void Delete(int id, out bool result, out string message)
        {
            _foodCategoryRepository.Remove(id, out result, out message);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public PagedResult<FoodCategory> GetAllPaging(string keyword, string sortBy, int page, int pageSize, out bool result, out string message)
        {
            PagedResult<FoodCategory> paginationSet = new PagedResult<FoodCategory>()
            {
                CurrentPage = page,
                PageSize = pageSize,
                Results = null,
                RowCount = 0
            };

            try
            {
                var query = _foodCategoryRepository.FindAll(x => x.Active == true);

                if (!string.IsNullOrEmpty(keyword))
                    query = query.Where(x => x.Id.ToString().Contains(keyword) || x.Code.Contains(keyword) || x.Name.Contains(keyword) || x.Description.Contains(keyword));

                if (query.Any())
                {
                    if (!string.IsNullOrEmpty(sortBy))
                    {
                        var sortProperty = query.First().GetType().GetProperty(sortBy, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                        if (sortProperty != null)
                            query = query.OrderBy(e => sortProperty.GetValue(e, null));
                    }
                    else
                        query = query.OrderBy(x => x.OrderNo).ThenBy(x => x.Name).ThenBy(x => x.Id);
                }
                    
                int totalRow = query.Count();

                query = query.Skip((page - 1) * pageSize).Take(pageSize);
                var data = query.ToList();

                paginationSet.Results = data;
                paginationSet.RowCount = totalRow;

                result = true;
                message = CommonConstants.Message.Success;
            }
            catch (Exception ex)
            {
                result = false;
                message = ex.Message;
            }

            return paginationSet;
        }

        public FoodCategoryViewModel GetById(int id, out bool result, out string message)
        {
            try
            {
                result = true;
                message = CommonConstants.Message.Success;
                return Mapper.Map<FoodCategory, FoodCategoryViewModel>(_foodCategoryRepository.FindById(id));
            }
            catch (Exception ex)
            {
                result = false;
                message = ex.Message;
                return null;
            }
        }

        public IEnumerable<FoodCategoryViewModel> GetAll(out bool result, out string message)
        {
            try
            {
                result = true;
                message = CommonConstants.Message.Success;
                return _foodCategoryRepository.FindAll(x => x.Active == true).ProjectTo<FoodCategoryViewModel>();
            }
            catch (Exception ex)
            {
                result = false;
                message = ex.Message;
                return null;
            }
        }

        public IEnumerable<FoodCategoryImage> GetImages(int foodCategoryId)
        {
            return _foodCategoryImageRepository.FindAll(x => x.FoodCategoryId == foodCategoryId);
        }

        public void SaveChanges()
        {
            _unitOfWork.SaveChanges();
        }

        public void SoftDelete(int id, out bool result, out string message)
        {
            _foodCategoryRepository.SoftRemove(id, out result, out message);
            if (result)
                SaveChanges();
            else
                Dispose();
        }

        
    }
}
