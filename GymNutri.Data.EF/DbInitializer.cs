using GymNutri.Data.Entities;
using GymNutri.Data.Enums;
using GymNutri.Utilities.Constants;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymNutri.Data.EF
{
    public class DbInitializer
    {
        private readonly AppDbContext _context;
        private UserManager<AppUser> _userManager;
        private RoleManager<AppRole> _roleManager;

        public DbInitializer(AppDbContext context, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task Seed()
        {
            if (!_roleManager.Roles.Any())
            {
                await _roleManager.CreateAsync(new AppRole()
                {
                    Name = "Admin",
                    NormalizedName = "Admin",
                    Description = "Administrator"
                });
                await _roleManager.CreateAsync(new AppRole()
                {
                    Name = "Manager",
                    NormalizedName = "Manager",
                    Description = "Manager"
                });
                await _roleManager.CreateAsync(new AppRole()
                {
                    Name = "Staff",
                    NormalizedName = "Staff",
                    Description = "Staff"
                });
                await _roleManager.CreateAsync(new AppRole()
                {
                    Name = "Customer",
                    NormalizedName = "Customer",
                    Description = "Customer"
                });
            }

            if (!_userManager.Users.Any())
            {
                await _userManager.CreateAsync(new AppUser()
                {
                    UserName = "admin",
                    FullName = "Administrator",
                    Email = "admin@gmail.com",
                    Balance = 0,
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now,
                    Active = true
                }, "123");
                var user = await _userManager.FindByNameAsync("admin");
                await _userManager.AddToRoleAsync(user, "Admin");
            }

            if (!_context.Contacts.Any())
            {
                _context.Contacts.Add(new Contact()
                {
                    Id = CommonConstants.DefaultContactId,
                    Address = "Ngõ 168, đường Vương Thừa Vũ, quận Thanh Xuân, Hà Nội",
                    Email = "gymnutri@gmail.com",
                    Name = "GymNutri",
                    Phone = "0974 240 911",
                    Website = "http://GymNutri.com",
                    Lat = 21.0435009,
                    Lng = 105.7894758,
                    Active = true
                });
            }

            if (_context.Functions.Count() == 0)
            {
                _context.Functions.AddRange(new List<Function>()
                {
                    new Function() {Id = "Business", Name = "Business", ParentId = null, OrderNo = 1, Active = true, Url = "/", IconCss = "fa fa-building" },
                    new Function() {Id = "MemberCard", Name = "Member Card", ParentId = "Business", OrderNo = 1, Active = true, Url = "/Admin/MemberCard/Index", IconCss = "fa fa-credit-card-alt" },
                    new Function() {Id = "DaisyOrder", Name = "Daisy Order", ParentId = "Business", OrderNo = 2, Active = true, Url = "/Admin/DaisyOrder/Index", IconCss = "fa fa-folder-o" },

                    new Function() {Id = "Category", Name = "Category", ParentId = null, OrderNo = 1, Active = true, Url = "/", IconCss = "fa fa-th-list" },
                    new Function() {Id = "CommonCategory", Name = "Common Category", ParentId = "Category", OrderNo = 1, Active = true, Url = "/Admin/CommonCategory/Index", IconCss = "fa fa-list-ol" },
                    new Function() {Id = "StatusCategory", Name = "Status Category", ParentId = "Category", OrderNo = 2, Active = true, Url = "/Admin/StatusCategory/Index", IconCss = "fa fa-star-half-o" },

                    new Function() {Id = "CustomerRelated", Name = "Related To Customer", ParentId = null, OrderNo = 2, Active = true, Url = "/", IconCss = "fa fa-user" },
                    new Function() {Id = "Customer", Name = "Customer", ParentId = "CustomerRelated", OrderNo = 1, Active = true, Url = "/Admin/Customer/Index", IconCss = "fa fa-user-plus" },
                    new Function() {Id = "BodyClassification", Name = "Body Classification", ParentId = "CustomerRelated", OrderNo = 2, Active = true, Url = "/Admin/BodyClassification/Index", IconCss = "fa fa-male" },

                    new Function() {Id = "System", Name = "System", ParentId = null, OrderNo = 3, Active = true, Url = "/", IconCss = "fa fa-cog" },
                    new Function() {Id = "Role", Name = "Role", ParentId = "System", OrderNo = 1, Active = true, Url = "/Admin/Role/Index", IconCss = "fa fa-id-card" },
                    new Function() {Id = "User", Name = "User", ParentId = "System", OrderNo = 2, Active = true, Url = "/Admin/User/Index", IconCss = "fa fa-users" },

                    new Function() {Id = "FoodRelated", Name = "Related To Food", ParentId = null, OrderNo = 4, Active = true, Url = "/", IconCss = "fa fa-apple" },
                    new Function() {Id = "FoodCategory", Name = "Food Category", ParentId = "FoodRelated", OrderNo = 1, Active = true, Url = "/Admin/FoodCategory/Index", IconCss = "fa fa-external-link" },
                    new Function() {Id = "Food", Name = "Food", ParentId = "FoodRelated", OrderNo = 2, Active = true, Url = "/Admin/Food/Index", IconCss = "fa fa-pie-chart" },
                    new Function() {Id = "SetOfFood", Name = "Set Of Food", ParentId = "FoodRelated", OrderNo = 3, Active = true, Url = "/Admin/SetOfFood/Index", IconCss = "fa fa-bars" },
                    new Function() {Id = "FoodPrice", Name = "Food Price", ParentId = "FoodRelated", OrderNo = 4, Active = true, Url = "/Admin/FoodPrice/Index", IconCss = "fa fa-money" },
                    new Function() {Id = "Meal", Name = "Meal", ParentId = "FoodRelated", OrderNo = 5, Active = true, Url = "/Admin/Meal/Index", IconCss = "fa fa-circle-o" },

                    new Function() {Id = "Template", Name = "Template", ParentId = null, OrderNo = 5, Active = true, Url = "/", IconCss = "fa fa-folder-open" },
                    new Function() {Id = "TemplateMenu", Name = "Template Menu", ParentId = "Template", OrderNo = 1, Active = true, Url = "/Admin/TemplateMenu/Index", IconCss = "fa fa-road" },

                    new Function() {Id = "Utility", Name = "Utility", ParentId = null, OrderNo = 6, Active = true, Url = "/", IconCss = "fa fa-clone" },
                    new Function() {Id = "Footer", Name = "Footer", ParentId = "Utility", OrderNo = 1, Active = true, Url = "/Admin/Footer/Index", IconCss = "fa fa-fort-awesome" },
                    new Function() {Id = "Feedback", Name = "Feedback", ParentId = "Utility", OrderNo = 2, Active = true, Url = "/Admin/Feedback/Index", IconCss = "fa fa-comments" },
                    new Function() {Id = "Announcement", Name = "Announcement", ParentId = "Utility", OrderNo = 3, Active = true, Url = "/Admin/Announcement/Index", IconCss = "fa fa-comment" },
                    new Function() {Id = "Contact", Name = "Contact", ParentId = "Utility", OrderNo = 4, Active = true, Url = "/Admin/Contact/Index", IconCss = "fa fa-address-book" },
                    new Function() {Id = "Slide", Name = "Slide", ParentId = "Utility", OrderNo = 5, Active = true, Url = "/Admin/Slide/Index", IconCss = "fa fa-picture-o" },
                    new Function() {Id = "Advertisment", Name = "Advertisment", ParentId = "Utility", OrderNo = 6, Active = true, Url = "/Admin/Advertisement/Index", IconCss = "fa fa-eye" },

                });
            }

            if (_context.Footers.Count(x => x.Id == CommonConstants.DefaultFooterId) == 0)
            {
                string content = "Footer";
                _context.Footers.Add(new Footer()
                {
                    Id = CommonConstants.DefaultFooterId,
                    Content = content
                });
            }

            if (_context.Slides.Count() == 0)
            {
                List<Slide> slides = new List<Slide>()
                {
                    new Slide() {Name = "Slide 01", Image = "/client-side/images/slider/slide-1.jpg", Url = "#", DisplayOrder = 0, GroupAlias = "top", Active = true},
                    new Slide() {Name = "Slide 02", Image = "/client-side/images/slider/slide-2.jpg", Url = "#", DisplayOrder = 1, GroupAlias = "top", Active = true},
                    new Slide() {Name = "Slide 03", Image = "/client-side/images/slider/slide-3.jpg", Url = "#", DisplayOrder = 2, GroupAlias = "top", Active = true},
                    new Slide() {Name = "Slide 01", Image = "/client-side/images/brand1.png", Url = "#", DisplayOrder = 1, GroupAlias = "brand", Active = true},
                    new Slide() {Name = "Slide 02", Image = "/client-side/images/brand2.png", Url = "#", DisplayOrder = 2, GroupAlias = "brand", Active = true},
                    new Slide() {Name = "Slide 03", Image = "/client-side/images/brand3.png", Url = "#", DisplayOrder = 3, GroupAlias = "brand", Active = true},
                    new Slide() {Name = "Slide 04", Image = "/client-side/images/brand4.png", Url = "#", DisplayOrder = 4, GroupAlias = "brand", Active = true},
                    new Slide() {Name = "Slide 05", Image = "/client-side/images/brand5.png", Url = "#", DisplayOrder = 5, GroupAlias = "brand", Active = true},
                    new Slide() {Name = "Slide 06", Image = "/client-side/images/brand6.png", Url = "#", DisplayOrder = 6, GroupAlias = "brand", Active = true},
                    new Slide() {Name = "Slide 07", Image = "/client-side/images/brand7.png", Url = "#", DisplayOrder = 7, GroupAlias = "brand", Active = true},
                    new Slide() {Name = "Slide 08", Image = "/client-side/images/brand8.png", Url = "#", DisplayOrder = 8, GroupAlias = "brand", Active = true},
                    new Slide() {Name = "Slide 09", Image = "/client-side/images/brand9.png", Url = "#", DisplayOrder = 9, GroupAlias = "brand", Active = true},
                    new Slide() {Name = "Slide 10", Image = "/client-side/images/brand10.png", Url = "#", DisplayOrder = 10, GroupAlias = "brand", Active = true},
                    new Slide() {Name = "Slide 11", Image = "/client-side/images/brand11.png", Url = "#", DisplayOrder = 11, GroupAlias = "brand", Active = true},
                };
                _context.Slides.AddRange(slides);
            }

            if (!_context.SystemConfigs.Any(x => x.Id == "HomeTitle"))
            {
                _context.SystemConfigs.Add(new SystemConfig()
                {
                    Id = "HomeTitle",
                    Name = "Home's title",
                    Value1 = "Gym Nutri",
                    Active = true
                });
            }

            if (!_context.SystemConfigs.Any(x => x.Id == "HomeMetaKeyword"))
            {
                _context.SystemConfigs.Add(new SystemConfig()
                {
                    Id = "HomeMetaKeyword",
                    Name = "Home Keyword",
                    Value1 = "Gym, Nutrition",
                    Active = true
                });
            }

            if (!_context.SystemConfigs.Any(x => x.Id == "HomeMetaDescription"))
            {
                _context.SystemConfigs.Add(new SystemConfig()
                {
                    Id = "HomeMetaDescription",
                    Name = "Home Description",
                    Value1 = "Home Gym nutrition",
                    Active = true
                });
            }

            if (_context.AdvertisementPages.Count() == 0)
            {
                List<AdvertisementPage> pages = new List<AdvertisementPage>()
                {
                    new AdvertisementPage() {Id="home", Name = "Home", AdvertisementPositions = new List<AdvertisementPosition>(){
                        new AdvertisementPosition(){Id="home-left", Name = "Bên trái"}
                    } },
                    new AdvertisementPage() {Id="product-cate", Name = "Product category" ,
                        AdvertisementPositions = new List<AdvertisementPosition>(){
                        new AdvertisementPosition(){Id="product-cate-left", Name = "Bên trái"}
                    }},
                    new AdvertisementPage() {Id="product-detail", Name = "Product detail",
                        AdvertisementPositions = new List<AdvertisementPosition>(){
                        new AdvertisementPosition(){Id="product-detail-left", Name = "Bên trái"}
                    } },

                };
                _context.AdvertisementPages.AddRange(pages);
            }

            if (_context.BodyClassifications.Count() == 0)
            {
                List<BodyClassification> bodyClassifications = new List<BodyClassification>()
                {
                    new BodyClassification() {
                        GroupCode = "BodyType",
                        Code = "Ectomorph",
                        Name = "Ectomorph",
                        Description = "Tạng người gầy",
                        Active = true
                    },
                    new BodyClassification() {
                        GroupCode = "BodyType",
                        Code = "Mesomorph",
                        Name = "Mesomorph",
                        Description = "Tạng người cơ bắp",
                        Active = true
                    },
                    new BodyClassification() {
                        GroupCode = "BodyType",
                        Code = "Endomorph",
                        Name = "Endomorph",
                        Description = "Tạng người béo",
                        Active = true
                    }
                };
                _context.BodyClassifications.AddRange(bodyClassifications);
            }

            if (_context.CommonCategories.Count() == 0)
            {
                List<CommonCategory> commonCategories = new List<CommonCategory>()
                {
                    new CommonCategory() {
                        GroupCode = "FoodType",
                        Code = "Eating",
                        Name = "Đồ ăn",
                        Active = true,
                        UserCreated = "admin@gmail.com"
                    },
                    new CommonCategory() {
                        GroupCode = "FoodType",
                        Code = "Beverage",
                        Name = "Đồ uống",
                        Active = true,
                        UserCreated = "admin@gmail.com"
                    }
                };
                _context.CommonCategories.AddRange(commonCategories);
            }

            if (_context.FoodCategories.Count() == 0)
            {
                List<FoodCategory> foodCategories = new List<FoodCategory>()
                {
                    new FoodCategory() {
                        Code = "main",
                        Name = "Món chính",
                        FoodTypeCode = "Eating",
                        OrderNo = 1,
                        Active = true,
                        UserCreated = "admin@gmail.com"
                    }
                };
                _context.FoodCategories.AddRange(foodCategories);
            }

            if (_context.Foods.Count() == 0)
            {
                List<Food> foods = new List<Food>()
                {
                    new Food() {
                        Code = "beef",
                        Name = "Thịt bò xào tổng hợp",
                        FoodCategoryId = 1,
                        Description = "Món ăn quen thuộc nhưng để làm ngon cũng không phải là dễ dàng",
                        UserCreated = "admin@gmail.com",
                        Active = true
                    },
                    new Food() {
                        Code = "chicken",
                        Name = "Gà xào xả ớt",
                        FoodCategoryId = 1,
                        Description = "Ngon ngon, lạ miệng, dễ làm",
                        UserCreated = "admin@gmail.com",
                        Active = true
                    }
                };
                _context.Foods.AddRange(foods);
            }

            if (_context.StatusCategories.Count() == 0)
            {
                List<StatusCategory> statusCategories = new List<StatusCategory>()
                {
                    new StatusCategory() {
                        Code = "vip",
                        Name = "Vip",
                        OrderNo = 1,
                        Table = "User",
                        Active = true
                    },
                    new StatusCategory() {
                        Code = "done",
                        Name = "Done",
                        OrderNo = 1,
                        Table = "Order",
                        Active = true
                    }
                };
                _context.StatusCategories.AddRange(statusCategories);
            }

            await _context.SaveChangesAsync();
        }

    }
}
