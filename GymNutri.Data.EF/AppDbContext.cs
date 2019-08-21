using GymNutri.Data.EF.Configurations;
using GymNutri.Data.EF.Extensions;
using GymNutri.Data.Entities;
using GymNutri.Data.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GymNutri.Data.EF
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Advertisement> Advertisements { get; set; }
        public DbSet<AdvertisementPage> AdvertisementPages { get; set; }
        public DbSet<AdvertisementPosition> AdvertisementPositions { get; set; }
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<AnnouncementUser> AnnouncementUsers { get; set; }
        public DbSet<AppRole> AppRoles { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<BlogTag> BlogTags { get; set; }
        public DbSet<BodyClassification> BodyClassifications { get; set; }
        public DbSet<CommonCategory> CommonCategories { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<DaisyOrder> DaisyOrders { get; set; }
        public DbSet<DaisyOrderDetail> DaisyOrderDetails { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<FoodCategory> FoodCategories { get; set; }
        public DbSet<FoodCategoryImage> FoodCategoryImages { get; set; }
        public DbSet<FoodCategoryTag> FoodCategoryTags { get; set; }
        public DbSet<FoodPrice> FoodPrices { get; set; }
        public DbSet<FoodsInSet> FoodsInSets { get; set; }
        public DbSet<FoodTag> FoodTags { get; set; }
        public DbSet<FoodImage> FoodImages { get; set; }
        public DbSet<Footer> Footers { get; set; }
        public DbSet<Function> Functions { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<LocationToGetOrderOfUser> LocationToGetOrderOfUsers { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<MemberCard> MemberCards { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<SetOfFood> SetOfFoods { get; set; }
        public DbSet<Slide> Slides { get; set; }
        public DbSet<StatusCategory> StatusCategories { get; set; }
        public DbSet<SystemConfig> SystemConfigs { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TemplateMenu> TemplateMenus { get; set; }
        public DbSet<TemplateMenuSet> TemplateMenuSets { get; set; }
        public DbSet<TemplateMenuSetDetail> TemplateMenuSetDetails { get; set; }
        public DbSet<TemplateMenuForBodyClassification> TemplateMenuForBodyClassifications { get; set; }
        public DbSet<UserBodyIndex> UserBodyIndexes { get; set; }
        public DbSet<UserDesire> UserDesires { get; set; }
        public DbSet<UserFavoriteFood> UserFavoriteFoods { get; set; }
        public DbSet<UserFavoriteTaste> UserFavoriteTastes { get; set; }
        public DbSet<UserMenu> UserMenus { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            #region Identity Config

            builder.Entity<IdentityUserClaim<Guid>>().ToTable("AppUserClaims").HasKey(x => x.Id);

            builder.Entity<IdentityRoleClaim<Guid>>().ToTable("AppRoleClaims")
                .HasKey(x => x.Id);

            builder.Entity<IdentityUserLogin<Guid>>().ToTable("AppUserLogins").HasKey(x => x.UserId);

            builder.Entity<IdentityUserRole<Guid>>().ToTable("AppUserRoles")
                .HasKey(x => new { x.RoleId, x.UserId });

            builder.Entity<IdentityUserToken<Guid>>().ToTable("AppUserTokens")
               .HasKey(x => new { x.UserId });

            #endregion Identity Config
            
            builder.AddConfiguration(new TagConfiguration());
            builder.AddConfiguration(new BlogTagConfiguration());
            builder.AddConfiguration(new ContactDetailConfiguration());
            builder.AddConfiguration(new FooterConfiguration());
            builder.AddConfiguration(new PageConfiguration());
            builder.AddConfiguration(new FunctionConfiguration());
            builder.AddConfiguration(new SystemConfigConfiguration());
            builder.AddConfiguration(new AdvertisementPositionConfiguration());
            builder.AddConfiguration(new AdvertisementPageConfiguration());
            builder.AddConfiguration(new AnnouncementConfiguration());
        }

        public override int SaveChanges()
        {
            var modified = ChangeTracker.Entries().Where(e => e.State == EntityState.Modified || e.State == EntityState.Added);

            foreach(EntityEntry item in modified)
            {
                var changedOrAddedItem = item.Entity as ITracking;
                if (changedOrAddedItem != null)
                {
                    if (item.State == EntityState.Added)
                    {
                        changedOrAddedItem.DateCreated = DateTime.Now;
                    }

                    changedOrAddedItem.DateModified = DateTime.Now;
                }
            }

            return base.SaveChanges();
        }

    }

    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();
            var builder = new DbContextOptionsBuilder<AppDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            builder.UseSqlServer(connectionString);
            return new AppDbContext(builder.Options);
        }
    }
}
