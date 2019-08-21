using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GymNutri.Data.EF.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdvertisementPages",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 20, nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdvertisementPages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppRoleClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    NormalizedName = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Description = table.Column<string>(maxLength: 2550, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: true),
                    ProviderKey = table.Column<string>(nullable: true),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserLogins", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "AppUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserRoles", x => new { x.RoleId, x.UserId });
                });

            migrationBuilder.CreateTable(
                name: "AppUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    NormalizedUserName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    NormalizedEmail = table.Column<string>(nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    FullName = table.Column<string>(maxLength: 255, nullable: true),
                    BirthDay = table.Column<DateTime>(nullable: true),
                    Balance = table.Column<decimal>(nullable: false),
                    Avatar = table.Column<string>(maxLength: 1000, nullable: true),
                    UserCreated = table.Column<string>(maxLength: 255, nullable: true),
                    UserModified = table.Column<string>(maxLength: 255, nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserTokens", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Blogs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: false),
                    Image = table.Column<string>(maxLength: 256, nullable: true),
                    Description = table.Column<string>(maxLength: 500, nullable: true),
                    Content = table.Column<string>(nullable: true),
                    HomeFlag = table.Column<bool>(nullable: true),
                    HotFlag = table.Column<bool>(nullable: true),
                    ViewCount = table.Column<int>(nullable: true),
                    Tags = table.Column<string>(maxLength: 255, nullable: true),
                    SeoPageTitle = table.Column<string>(maxLength: 500, nullable: true),
                    SeoAlias = table.Column<string>(maxLength: 500, nullable: true),
                    SeoKeywords = table.Column<string>(maxLength: 500, nullable: true),
                    SeoDescription = table.Column<string>(maxLength: 2000, nullable: true),
                    UserCreated = table.Column<string>(maxLength: 255, nullable: true),
                    UserModified = table.Column<string>(maxLength: 255, nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BodyClassifications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    GroupCode = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    Code = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    Detail = table.Column<string>(maxLength: 4000, nullable: true),
                    SeoPageTitle = table.Column<string>(maxLength: 500, nullable: true),
                    SeoAlias = table.Column<string>(maxLength: 500, nullable: true),
                    SeoKeywords = table.Column<string>(maxLength: 500, nullable: true),
                    SeoDescription = table.Column<string>(maxLength: 2000, nullable: true),
                    UserCreated = table.Column<string>(maxLength: 255, nullable: true),
                    UserModified = table.Column<string>(maxLength: 255, nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BodyClassifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CommonCategories ",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    Code = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    GroupCode = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    OrderNo = table.Column<int>(nullable: false),
                    UserCreated = table.Column<string>(maxLength: 255, nullable: true),
                    UserModified = table.Column<string>(maxLength: 255, nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommonCategories ", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 255, nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    Phone = table.Column<string>(maxLength: 50, nullable: true),
                    Email = table.Column<string>(maxLength: 255, nullable: true),
                    Website = table.Column<string>(maxLength: 255, nullable: true),
                    Address = table.Column<string>(maxLength: 255, nullable: true),
                    Other = table.Column<string>(nullable: true),
                    Lat = table.Column<double>(nullable: true),
                    Lng = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Feedbacks",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    Email = table.Column<string>(maxLength: 255, nullable: true),
                    Message = table.Column<string>(maxLength: 500, nullable: true),
                    UserCreated = table.Column<string>(maxLength: 255, nullable: true),
                    UserModified = table.Column<string>(maxLength: 255, nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedbacks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FoodCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    ParentId = table.Column<int>(nullable: true),
                    Code = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    FoodTypeCode = table.Column<string>(maxLength: 255, nullable: true),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    OrderNo = table.Column<int>(nullable: false),
                    Image = table.Column<string>(maxLength: 255, nullable: true),
                    SeoPageTitle = table.Column<string>(maxLength: 500, nullable: true),
                    SeoAlias = table.Column<string>(maxLength: 500, nullable: true),
                    SeoKeywords = table.Column<string>(maxLength: 500, nullable: true),
                    SeoDescription = table.Column<string>(maxLength: 2000, nullable: true),
                    UserCreated = table.Column<string>(maxLength: 255, nullable: true),
                    UserModified = table.Column<string>(maxLength: 255, nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Footers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    Content = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Footers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Functions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", maxLength: 128, nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    Url = table.Column<string>(maxLength: 1000, nullable: false),
                    ParentId = table.Column<string>(maxLength: 255, nullable: true),
                    IconCss = table.Column<string>(maxLength: 255, nullable: true),
                    OrderNo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Functions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    IsDefault = table.Column<bool>(nullable: false),
                    Resources = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LocationToGetOrderOfUsers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    Address = table.Column<string>(maxLength: 500, nullable: false),
                    FirstFrom = table.Column<DateTime>(nullable: false),
                    FirstTo = table.Column<DateTime>(nullable: false),
                    SecondFrom = table.Column<DateTime>(nullable: false),
                    SecondTo = table.Column<DateTime>(nullable: false),
                    ThirdFrom = table.Column<DateTime>(nullable: false),
                    ThirdTo = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    UserCreated = table.Column<string>(maxLength: 255, nullable: true),
                    UserModified = table.Column<string>(maxLength: 255, nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationToGetOrderOfUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Meals",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    Code = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    DesireTypeCode = table.Column<int>(maxLength: 255, nullable: false),
                    OrderNo = table.Column<int>(nullable: false),
                    MealTime = table.Column<TimeSpan>(nullable: false),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    SeoPageTitle = table.Column<string>(maxLength: 500, nullable: true),
                    SeoAlias = table.Column<string>(maxLength: 500, nullable: true),
                    SeoKeywords = table.Column<string>(maxLength: 500, nullable: true),
                    SeoDescription = table.Column<string>(maxLength: 2000, nullable: true),
                    UserCreated = table.Column<string>(maxLength: 255, nullable: true),
                    UserModified = table.Column<string>(maxLength: 255, nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pages",
                columns: table => new
                {
                    Id = table.Column<int>(maxLength: 255, nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    Alias = table.Column<string>(maxLength: 255, nullable: false),
                    Content = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SetOfFoods",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    Code = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    SeoPageTitle = table.Column<string>(maxLength: 500, nullable: true),
                    SeoAlias = table.Column<string>(maxLength: 500, nullable: true),
                    SeoKeywords = table.Column<string>(maxLength: 500, nullable: true),
                    SeoDescription = table.Column<string>(maxLength: 2000, nullable: true),
                    UserCreated = table.Column<string>(maxLength: 255, nullable: true),
                    UserModified = table.Column<string>(maxLength: 255, nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SetOfFoods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Slides",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    Image = table.Column<string>(maxLength: 1000, nullable: false),
                    Url = table.Column<string>(maxLength: 1000, nullable: true),
                    DisplayOrder = table.Column<int>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    GroupAlias = table.Column<string>(maxLength: 25, nullable: false),
                    UserCreated = table.Column<string>(maxLength: 255, nullable: true),
                    UserModified = table.Column<string>(maxLength: 255, nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Slides", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StatusCategories ",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    Code = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    OrderNo = table.Column<int>(nullable: false),
                    Table = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    Color = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    ShowInAdmin = table.Column<bool>(nullable: false),
                    ShowInClient = table.Column<bool>(nullable: false),
                    UserCreated = table.Column<string>(maxLength: 255, nullable: true),
                    UserModified = table.Column<string>(maxLength: 255, nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusCategories ", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SystemConfigs",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 255, nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    Value1 = table.Column<string>(nullable: true),
                    Value2 = table.Column<int>(nullable: true),
                    Value3 = table.Column<bool>(nullable: true),
                    Value4 = table.Column<DateTime>(nullable: true),
                    Value5 = table.Column<decimal>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemConfigs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Type = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TemplateMenus",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    Code = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    Description = table.Column<string>(maxLength: 255, nullable: false),
                    SeoPageTitle = table.Column<string>(maxLength: 500, nullable: true),
                    SeoAlias = table.Column<string>(maxLength: 500, nullable: true),
                    SeoKeywords = table.Column<string>(maxLength: 500, nullable: true),
                    SeoDescription = table.Column<string>(maxLength: 2000, nullable: true),
                    UserCreated = table.Column<string>(maxLength: 255, nullable: true),
                    UserModified = table.Column<string>(maxLength: 255, nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemplateMenus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserBodyIndexes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Height = table.Column<decimal>(nullable: false),
                    Weight = table.Column<decimal>(nullable: false),
                    Chest = table.Column<decimal>(nullable: false),
                    Waist = table.Column<decimal>(nullable: false),
                    Ass = table.Column<decimal>(nullable: false),
                    FatPercent = table.Column<decimal>(nullable: false),
                    MusclePercent = table.Column<decimal>(nullable: false),
                    SeoPageTitle = table.Column<string>(maxLength: 500, nullable: true),
                    SeoAlias = table.Column<string>(maxLength: 500, nullable: true),
                    SeoKeywords = table.Column<string>(maxLength: 500, nullable: true),
                    SeoDescription = table.Column<string>(maxLength: 2000, nullable: true),
                    UserCreated = table.Column<string>(maxLength: 255, nullable: true),
                    UserModified = table.Column<string>(maxLength: 255, nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBodyIndexes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserDesires",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    DesireTypeCode = table.Column<string>(maxLength: 255, nullable: false),
                    Description = table.Column<string>(maxLength: 255, nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    Detail = table.Column<string>(maxLength: 4000, nullable: false),
                    SeoPageTitle = table.Column<string>(maxLength: 500, nullable: true),
                    SeoAlias = table.Column<string>(maxLength: 500, nullable: true),
                    SeoKeywords = table.Column<string>(maxLength: 500, nullable: true),
                    SeoDescription = table.Column<string>(maxLength: 2000, nullable: true),
                    UserCreated = table.Column<string>(maxLength: 255, nullable: true),
                    UserModified = table.Column<string>(maxLength: 255, nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDesires", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserFavoriteTastes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    FavoriteTasteTypeCode = table.Column<string>(maxLength: 255, nullable: false),
                    Description = table.Column<string>(maxLength: 255, nullable: false),
                    SeoPageTitle = table.Column<string>(maxLength: 500, nullable: true),
                    SeoAlias = table.Column<string>(maxLength: 500, nullable: true),
                    SeoKeywords = table.Column<string>(maxLength: 500, nullable: true),
                    SeoDescription = table.Column<string>(maxLength: 2000, nullable: true),
                    UserCreated = table.Column<string>(maxLength: 255, nullable: true),
                    UserModified = table.Column<string>(maxLength: 255, nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFavoriteTastes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserMenus",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    UserCreated = table.Column<string>(maxLength: 255, nullable: true),
                    UserModified = table.Column<string>(maxLength: 255, nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMenus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AdvertisementPositions",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 20, nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    PageId = table.Column<string>(maxLength: 20, nullable: true),
                    Name = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdvertisementPositions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdvertisementPositions_AdvertisementPages_PageId",
                        column: x => x.PageId,
                        principalTable: "AdvertisementPages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Announcements",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 128, nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    Title = table.Column<string>(maxLength: 250, nullable: false),
                    Content = table.Column<string>(maxLength: 250, nullable: true),
                    UserId = table.Column<Guid>(nullable: false),
                    OrderNo = table.Column<int>(nullable: false),
                    UserCreated = table.Column<string>(maxLength: 255, nullable: true),
                    UserModified = table.Column<string>(maxLength: 255, nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Announcements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Announcements_AppUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserBodyClassifications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    BodyClassificationId = table.Column<int>(nullable: false),
                    BodyClassificationCode = table.Column<string>(maxLength: 255, nullable: false),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    UserCreated = table.Column<string>(maxLength: 255, nullable: true),
                    UserModified = table.Column<string>(maxLength: 255, nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBodyClassifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserBodyClassifications_BodyClassifications_BodyClassificationId",
                        column: x => x.BodyClassificationId,
                        principalTable: "BodyClassifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Foods",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    Code = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    FoodCategoryId = table.Column<int>(nullable: false),
                    FoodCategoryCode = table.Column<string>(maxLength: 255, nullable: false),
                    Description = table.Column<string>(maxLength: 255, nullable: false),
                    Ingredient = table.Column<string>(maxLength: 1000, nullable: true),
                    Recipe = table.Column<string>(maxLength: 4000, nullable: true),
                    CookingDuration = table.Column<int>(nullable: false),
                    CookingGuideLink = table.Column<string>(maxLength: 1000, nullable: true),
                    Image = table.Column<string>(maxLength: 1000, nullable: true),
                    Tags = table.Column<string>(maxLength: 255, nullable: true),
                    SeoPageTitle = table.Column<string>(maxLength: 500, nullable: true),
                    SeoAlias = table.Column<string>(maxLength: 500, nullable: true),
                    SeoKeywords = table.Column<string>(maxLength: 500, nullable: true),
                    SeoDescription = table.Column<string>(maxLength: 2000, nullable: true),
                    UserCreated = table.Column<string>(maxLength: 255, nullable: true),
                    UserModified = table.Column<string>(maxLength: 255, nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Foods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Foods_FoodCategories_FoodCategoryId",
                        column: x => x.FoodCategoryId,
                        principalTable: "FoodCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false),
                    FunctionId = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    CanCreate = table.Column<bool>(nullable: false),
                    CanRead = table.Column<bool>(nullable: false),
                    CanUpdate = table.Column<bool>(nullable: false),
                    CanDelete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Permissions_Functions_FunctionId",
                        column: x => x.FunctionId,
                        principalTable: "Functions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Permissions_AppRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AppRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BlogTags",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    BlogId = table.Column<int>(nullable: false),
                    TagId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlogTags_Blogs_BlogId",
                        column: x => x.BlogId,
                        principalTable: "Blogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BlogTags_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TemplateMenuDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    TemplateMenuId = table.Column<int>(nullable: false),
                    TemplateMenuCode = table.Column<string>(maxLength: 255, nullable: false),
                    MealId = table.Column<int>(nullable: false),
                    MealCode = table.Column<string>(maxLength: 255, nullable: false),
                    SetOfFoodId = table.Column<int>(nullable: false),
                    SetOfFoodCode = table.Column<string>(maxLength: 255, nullable: false),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    UserCreated = table.Column<string>(maxLength: 255, nullable: true),
                    UserModified = table.Column<string>(maxLength: 255, nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemplateMenuDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TemplateMenuDetails_Meals_MealId",
                        column: x => x.MealId,
                        principalTable: "Meals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TemplateMenuDetails_TemplateMenus_TemplateMenuId",
                        column: x => x.TemplateMenuId,
                        principalTable: "TemplateMenus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TemplateMenuForBodyClassifications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    TemplateMenuId = table.Column<int>(nullable: false),
                    TemplateMenuCode = table.Column<string>(maxLength: 255, nullable: false),
                    BodyClassificationId = table.Column<int>(maxLength: 255, nullable: false),
                    BodyClassificationCode = table.Column<string>(nullable: false),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    SeoPageTitle = table.Column<string>(maxLength: 500, nullable: true),
                    SeoAlias = table.Column<string>(maxLength: 500, nullable: true),
                    SeoKeywords = table.Column<string>(maxLength: 500, nullable: true),
                    SeoDescription = table.Column<string>(maxLength: 2000, nullable: true),
                    UserCreated = table.Column<string>(maxLength: 255, nullable: true),
                    UserModified = table.Column<string>(maxLength: 255, nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemplateMenuForBodyClassifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TemplateMenuForBodyClassifications_BodyClassifications_BodyClassificationId",
                        column: x => x.BodyClassificationId,
                        principalTable: "BodyClassifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TemplateMenuForBodyClassifications_TemplateMenus_TemplateMenuId",
                        column: x => x.TemplateMenuId,
                        principalTable: "TemplateMenus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MemberCards",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    MemberTypeCode = table.Column<string>(maxLength: 255, nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    MonthDuration = table.Column<int>(nullable: false),
                    UserMenuId = table.Column<int>(nullable: false),
                    Cost = table.Column<decimal>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    PromotionPrice = table.Column<decimal>(nullable: false),
                    SeoPageTitle = table.Column<string>(maxLength: 500, nullable: true),
                    SeoAlias = table.Column<string>(maxLength: 500, nullable: true),
                    SeoKeywords = table.Column<string>(maxLength: 500, nullable: true),
                    SeoDescription = table.Column<string>(maxLength: 2000, nullable: true),
                    UserCreated = table.Column<string>(maxLength: 255, nullable: true),
                    UserModified = table.Column<string>(maxLength: 255, nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberCards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MemberCards_UserMenus_UserMenuId",
                        column: x => x.UserMenuId,
                        principalTable: "UserMenus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserMenuDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    UserMenuId = table.Column<int>(nullable: false),
                    TemplateMenuId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    UserCreated = table.Column<string>(maxLength: 255, nullable: true),
                    UserModified = table.Column<string>(maxLength: 255, nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMenuDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserMenuDetails_TemplateMenus_TemplateMenuId",
                        column: x => x.TemplateMenuId,
                        principalTable: "TemplateMenus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserMenuDetails_UserMenus_UserMenuId",
                        column: x => x.UserMenuId,
                        principalTable: "UserMenus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Advertisements",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: true),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    Image = table.Column<string>(maxLength: 1000, nullable: true),
                    Url = table.Column<string>(maxLength: 1000, nullable: true),
                    PositionId = table.Column<string>(maxLength: 20, nullable: true),
                    OrderNo = table.Column<int>(nullable: false),
                    UserCreated = table.Column<string>(maxLength: 255, nullable: true),
                    UserModified = table.Column<string>(maxLength: 255, nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Advertisements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Advertisements_AdvertisementPositions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "AdvertisementPositions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AnnouncementUsers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    AnnouncementId = table.Column<string>(maxLength: 128, nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    HasRead = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnnouncementUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnnouncementUsers_Announcements_AnnouncementId",
                        column: x => x.AnnouncementId,
                        principalTable: "Announcements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FoodPrices",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    FoodId = table.Column<long>(nullable: false),
                    Cost = table.Column<decimal>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    PromotionPrice = table.Column<decimal>(nullable: false),
                    UserCreated = table.Column<string>(maxLength: 255, nullable: true),
                    UserModified = table.Column<string>(maxLength: 255, nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodPrices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FoodPrices_Foods_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Foods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FoodsInSets",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    SetOfFoodId = table.Column<int>(nullable: false),
                    SetOfFoodCode = table.Column<string>(maxLength: 255, nullable: false),
                    FoodId = table.Column<long>(nullable: false),
                    FoodCode = table.Column<string>(maxLength: 255, nullable: false),
                    SeoPageTitle = table.Column<string>(maxLength: 500, nullable: true),
                    SeoAlias = table.Column<string>(maxLength: 500, nullable: true),
                    SeoKeywords = table.Column<string>(maxLength: 500, nullable: true),
                    SeoDescription = table.Column<string>(maxLength: 2000, nullable: true),
                    UserCreated = table.Column<string>(maxLength: 255, nullable: true),
                    UserModified = table.Column<string>(maxLength: 255, nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodsInSets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FoodsInSets_Foods_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Foods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FoodsInSets_SetOfFoods_SetOfFoodId",
                        column: x => x.SetOfFoodId,
                        principalTable: "SetOfFoods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FoodTags",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    FoodId = table.Column<long>(nullable: false),
                    TagId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FoodTags_Foods_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Foods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FoodTags_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserFavoriteFoods",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    FoodId = table.Column<long>(nullable: false),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    UserCreated = table.Column<string>(maxLength: 255, nullable: true),
                    UserModified = table.Column<string>(maxLength: 255, nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFavoriteFoods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserFavoriteFoods_Foods_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Foods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DaisyOrders",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    MemberCardId = table.Column<long>(nullable: false),
                    OrderDate = table.Column<DateTime>(nullable: false),
                    OrderInDay = table.Column<int>(nullable: false),
                    PlanOrderTime = table.Column<TimeSpan>(nullable: false),
                    RealOrderTime = table.Column<TimeSpan>(nullable: false),
                    LocationId = table.Column<int>(nullable: false),
                    AlternativePersonName = table.Column<string>(maxLength: 255, nullable: true),
                    AlternativePersonPhone = table.Column<string>(maxLength: 255, nullable: true),
                    AlternativePersonEmail = table.Column<string>(maxLength: 255, nullable: true),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    UserFeedback = table.Column<string>(maxLength: 500, nullable: true),
                    SeoPageTitle = table.Column<string>(maxLength: 500, nullable: true),
                    SeoAlias = table.Column<string>(maxLength: 500, nullable: true),
                    SeoKeywords = table.Column<string>(maxLength: 500, nullable: true),
                    SeoDescription = table.Column<string>(maxLength: 2000, nullable: true),
                    UserCreated = table.Column<string>(maxLength: 255, nullable: true),
                    UserModified = table.Column<string>(maxLength: 255, nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DaisyOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DaisyOrders_LocationToGetOrderOfUsers_LocationId",
                        column: x => x.LocationId,
                        principalTable: "LocationToGetOrderOfUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DaisyOrders_MemberCards_MemberCardId",
                        column: x => x.MemberCardId,
                        principalTable: "MemberCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DaisyOrderDetails",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    DaisyOrderId = table.Column<long>(nullable: false),
                    MealId = table.Column<int>(nullable: false),
                    MealCode = table.Column<string>(maxLength: 255, nullable: false),
                    SetOfFoodId = table.Column<int>(nullable: false),
                    SetOfFoodCode = table.Column<string>(maxLength: 255, nullable: false),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    RealEatTime = table.Column<TimeSpan>(nullable: false),
                    UserFeedback = table.Column<string>(maxLength: 500, nullable: true),
                    UserCreated = table.Column<string>(maxLength: 255, nullable: true),
                    UserModified = table.Column<string>(maxLength: 255, nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DaisyOrderDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DaisyOrderDetails_DaisyOrders_DaisyOrderId",
                        column: x => x.DaisyOrderId,
                        principalTable: "DaisyOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DaisyOrderDetails_Meals_MealId",
                        column: x => x.MealId,
                        principalTable: "Meals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DaisyOrderDetails_SetOfFoods_SetOfFoodId",
                        column: x => x.SetOfFoodId,
                        principalTable: "SetOfFoods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdvertisementPositions_PageId",
                table: "AdvertisementPositions",
                column: "PageId");

            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_PositionId",
                table: "Advertisements",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Announcements_UserId",
                table: "Announcements",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AnnouncementUsers_AnnouncementId",
                table: "AnnouncementUsers",
                column: "AnnouncementId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogTags_BlogId",
                table: "BlogTags",
                column: "BlogId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogTags_TagId",
                table: "BlogTags",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_DaisyOrderDetails_DaisyOrderId",
                table: "DaisyOrderDetails",
                column: "DaisyOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_DaisyOrderDetails_MealId",
                table: "DaisyOrderDetails",
                column: "MealId");

            migrationBuilder.CreateIndex(
                name: "IX_DaisyOrderDetails_SetOfFoodId",
                table: "DaisyOrderDetails",
                column: "SetOfFoodId");

            migrationBuilder.CreateIndex(
                name: "IX_DaisyOrders_LocationId",
                table: "DaisyOrders",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_DaisyOrders_MemberCardId",
                table: "DaisyOrders",
                column: "MemberCardId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodPrices_FoodId",
                table: "FoodPrices",
                column: "FoodId");

            migrationBuilder.CreateIndex(
                name: "IX_Foods_FoodCategoryId",
                table: "Foods",
                column: "FoodCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodsInSets_FoodId",
                table: "FoodsInSets",
                column: "FoodId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodsInSets_SetOfFoodId",
                table: "FoodsInSets",
                column: "SetOfFoodId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodTags_FoodId",
                table: "FoodTags",
                column: "FoodId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodTags_TagId",
                table: "FoodTags",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_MemberCards_UserMenuId",
                table: "MemberCards",
                column: "UserMenuId");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_FunctionId",
                table: "Permissions",
                column: "FunctionId");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_RoleId",
                table: "Permissions",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_TemplateMenuDetails_MealId",
                table: "TemplateMenuDetails",
                column: "MealId");

            migrationBuilder.CreateIndex(
                name: "IX_TemplateMenuDetails_TemplateMenuId",
                table: "TemplateMenuDetails",
                column: "TemplateMenuId");

            migrationBuilder.CreateIndex(
                name: "IX_TemplateMenuForBodyClassifications_BodyClassificationId",
                table: "TemplateMenuForBodyClassifications",
                column: "BodyClassificationId");

            migrationBuilder.CreateIndex(
                name: "IX_TemplateMenuForBodyClassifications_TemplateMenuId",
                table: "TemplateMenuForBodyClassifications",
                column: "TemplateMenuId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBodyClassifications_BodyClassificationId",
                table: "UserBodyClassifications",
                column: "BodyClassificationId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFavoriteFoods_FoodId",
                table: "UserFavoriteFoods",
                column: "FoodId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMenuDetails_TemplateMenuId",
                table: "UserMenuDetails",
                column: "TemplateMenuId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMenuDetails_UserMenuId",
                table: "UserMenuDetails",
                column: "UserMenuId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Advertisements");

            migrationBuilder.DropTable(
                name: "AnnouncementUsers");

            migrationBuilder.DropTable(
                name: "AppRoleClaims");

            migrationBuilder.DropTable(
                name: "AppUserClaims");

            migrationBuilder.DropTable(
                name: "AppUserLogins");

            migrationBuilder.DropTable(
                name: "AppUserRoles");

            migrationBuilder.DropTable(
                name: "AppUserTokens");

            migrationBuilder.DropTable(
                name: "BlogTags");

            migrationBuilder.DropTable(
                name: "CommonCategories ");

            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "DaisyOrderDetails");

            migrationBuilder.DropTable(
                name: "Feedbacks");

            migrationBuilder.DropTable(
                name: "FoodPrices");

            migrationBuilder.DropTable(
                name: "FoodsInSets");

            migrationBuilder.DropTable(
                name: "FoodTags");

            migrationBuilder.DropTable(
                name: "Footers");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropTable(
                name: "Pages");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "Slides");

            migrationBuilder.DropTable(
                name: "StatusCategories ");

            migrationBuilder.DropTable(
                name: "SystemConfigs");

            migrationBuilder.DropTable(
                name: "TemplateMenuDetails");

            migrationBuilder.DropTable(
                name: "TemplateMenuForBodyClassifications");

            migrationBuilder.DropTable(
                name: "UserBodyClassifications");

            migrationBuilder.DropTable(
                name: "UserBodyIndexes");

            migrationBuilder.DropTable(
                name: "UserDesires");

            migrationBuilder.DropTable(
                name: "UserFavoriteFoods");

            migrationBuilder.DropTable(
                name: "UserFavoriteTastes");

            migrationBuilder.DropTable(
                name: "UserMenuDetails");

            migrationBuilder.DropTable(
                name: "AdvertisementPositions");

            migrationBuilder.DropTable(
                name: "Announcements");

            migrationBuilder.DropTable(
                name: "Blogs");

            migrationBuilder.DropTable(
                name: "DaisyOrders");

            migrationBuilder.DropTable(
                name: "SetOfFoods");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Functions");

            migrationBuilder.DropTable(
                name: "AppRoles");

            migrationBuilder.DropTable(
                name: "Meals");

            migrationBuilder.DropTable(
                name: "BodyClassifications");

            migrationBuilder.DropTable(
                name: "Foods");

            migrationBuilder.DropTable(
                name: "TemplateMenus");

            migrationBuilder.DropTable(
                name: "AdvertisementPages");

            migrationBuilder.DropTable(
                name: "AppUsers");

            migrationBuilder.DropTable(
                name: "LocationToGetOrderOfUsers");

            migrationBuilder.DropTable(
                name: "MemberCards");

            migrationBuilder.DropTable(
                name: "FoodCategories");

            migrationBuilder.DropTable(
                name: "UserMenus");
        }
    }
}
