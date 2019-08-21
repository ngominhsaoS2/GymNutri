using AutoMapper;
using AutoMapper.QueryableExtensions;
using GymNutri.Business.Interfaces;
using GymNutri.Data.Entities;
using GymNutri.Data.ViewModels.FoodRelated;
using GymNutri.Infrastructure.Interfaces;
using GymNutri.Utilities.Constants;
using GymNutri.Utilities.Dtos;
using GymNutri.Utilities.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace GymNutri.Business.Implementation
{
    public class FoodService : IFoodService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Food, long> _foodRepository;
        private readonly IRepository<Tag, string> _tagRepository;
        private readonly IRepository<FoodTag, int> _foodTagRepository;
        private readonly IRepository<FoodImage, int> _foodImageRepository;

        public FoodService(IUnitOfWork unitOfWork,
            IRepository<Food, long> foodRepository,
            IRepository<Tag, string> tagRepository,
            IRepository<FoodTag, int> foodTagRepository,
            IRepository<FoodImage, int> foodImageRepository)
        {
            _unitOfWork = unitOfWork;
            _foodRepository = foodRepository;
            _tagRepository = tagRepository;
            _foodTagRepository = foodTagRepository;
            _foodImageRepository = foodImageRepository;
        }

        public void Add(Food food, out bool result, out string message)
        {
            food.FoodTags = new List<FoodTag>();

            // Tags
            if (!string.IsNullOrEmpty(food.Tags))
            {
                string[] tags = food.Tags.Split(';');
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
                            Type = CommonConstants.TagType.FoodTag,
                            UserCreated = food.UserCreated,
                            UserModified = food.UserModified
                        };
                        _tagRepository.Add(tag, out result, out message);
                    }

                    FoodTag foodTag = new FoodTag
                    {
                        TagId = tagId,
                        Active = true,
                        UserCreated = food.UserCreated,
                        UserModified = food.UserModified
                    };
                    food.FoodTags.Add(foodTag);
                }
            }

            // Insert FoodCategory
            _foodRepository.Add(food, out result, out message);

            if (result)
                SaveChanges();
            else
                Dispose();
        }

        public void UpdateChangedProperties(Food food, out bool result, out string message)
        {
            string oldTags = _foodRepository.FindById(food.Id).Tags;
            if (oldTags != food.Tags)
            {
                _foodTagRepository.RemoveMultiple(_foodTagRepository.FindAll(x => x.FoodId == food.Id).ToList(), out result, out message);

                if (!string.IsNullOrEmpty(food.Tags))
                {
                    string[] tags = food.Tags.Split(';');
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
                                Type = CommonConstants.TagType.FoodTag,
                                UserCreated = food.UserCreated,
                                UserModified = food.UserModified
                            };
                            _tagRepository.Add(tag, out result, out message);
                        }

                        FoodTag foodTag = new FoodTag
                        {
                            FoodId = food.Id,
                            TagId = tagId,
                            Active = true,
                            UserCreated = food.UserCreated,
                            UserModified = food.UserModified
                        };
                        _foodTagRepository.Add(foodTag, out result, out message);
                    }
                }
            }

            _foodRepository.UpdateChangedProperties(food.Id, food, out result, out message);
            if (result)
                SaveChanges();
            else
                Dispose();
        }

        public void Delete(long id, out bool result, out string message)
        {
            _foodRepository.Remove(id, out result, out message);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public PagedResult<Food> GetAllPaging(int? categoryId, string keyword, string sortBy, int page, int pageSize, out bool result, out string message)
        {
            PagedResult<Food> paginationSet = new PagedResult<Food>()
            {
                CurrentPage = page,
                PageSize = pageSize,
                Results = null,
                RowCount = 0
            };

            try
            {
                var query = _foodRepository.FindAll(x => x.Active == true);

                if(categoryId != null)
                    query = query.Where(x => x.FoodCategoryId == categoryId);

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
                        query = query.OrderBy(x => x.Name).ThenBy(x => x.Code).ThenBy(x => x.Id);
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

        public FoodViewModel GetById(long id, out bool result, out string message)
        {
            try
            {
                result = true;
                message = CommonConstants.Message.Success;
                return Mapper.Map<Food, FoodViewModel>(_foodRepository.FindById(id));
            }
            catch (Exception ex)
            {
                result = false;
                message = ex.Message;
                return null;
            }
        }

        public void SaveChanges()
        {
            _unitOfWork.SaveChanges();
        }

        public void SoftDelete(long id, out bool result, out string message)
        {
            _foodRepository.SoftRemove(id, out result, out message);
            if (result)
                SaveChanges();
            else
                Dispose();
        }

        public void AddImages(long foodId, string[] images, out bool result, out string message)
        {
            _foodImageRepository.RemoveMultiple(_foodImageRepository.FindAll(x => x.FoodId == foodId).ToList(), out result, out message);
            FoodImage foodImage;
            foreach (var image in images)
            {
                foodImage = new FoodImage();
                foodImage.FoodId = foodId;
                foodImage.Path = image;
                foodImage.Caption = string.Empty;
                foodImage.Active = true;
                _foodImageRepository.Add(foodImage, out result, out message);
                if (!result) break;
            }

            if (result)
                SaveChanges();
            else
                Dispose();
        }

        public IEnumerable<FoodImage> GetImages(long foodId)
        {
            return _foodImageRepository.FindAll(x => x.FoodId == foodId);
        }

        public IEnumerable<FoodViewModel> GetAll(out bool result, out string message)
        {
            try
            {
                result = true;
                message = CommonConstants.Message.Success;
                return _foodRepository.FindAll(x => x.Active == true).ProjectTo<FoodViewModel>();
            }
            catch (Exception ex)
            {
                result = false;
                message = ex.Message;
                return null;
            }
        }

        public IEnumerable<FoodViewModel> GetTop(int max, out bool result, out string message)
        {
            try
            {
                result = true;
                message = CommonConstants.Message.Success;
                return _foodRepository.FindAll(x => x.Active == true).OrderBy(x => x.Id).Take(max).ProjectTo<FoodViewModel>();
            }
            catch (Exception ex)
            {
                result = false;
                message = ex.Message;
                return null;
            }
        }
    }
}
