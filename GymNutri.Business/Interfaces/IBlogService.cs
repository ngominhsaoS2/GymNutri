using System;
using System.Collections.Generic;
using System.Text;
using GymNutri.Data.ViewModels.Blog;
using GymNutri.Data.ViewModels.Common;
using GymNutri.Utilities.Dtos;

namespace GymNutri.Business.Interfaces
{
    public interface IBlogService
    {
        void Add(BlogViewModel product, out bool result, out string message);

        void Update(BlogViewModel product, out bool result, out string message);

        void Delete(int id, out bool result, out string message);

        void SaveChanges();

        List<BlogViewModel> GetAll();

        PagedResult<BlogViewModel> GetAllPaging(string keyword, int pageSize, int page);

        List<BlogViewModel> GetLastest(int top);

        List<BlogViewModel> GetHotProduct(int top);

        List<BlogViewModel> GetListPaging(int page, int pageSize, string sort, out int totalRow);

        List<BlogViewModel> Search(string keyword, int page, int pageSize, string sort, out int totalRow);

        List<BlogViewModel> GetList(string keyword);

        List<BlogViewModel> GetReatedBlogs(int id, int top);

        List<string> GetListByName(string name);

        BlogViewModel GetById(int id);
        
        List<TagViewModel> GetListTagById(int id);

        TagViewModel GetTag(string tagId);

        void IncreaseView(int id);

        List<BlogViewModel> GetListByTag(string tagId, int page, int pagesize, out int totalRow);

        List<TagViewModel> GetListTag(string searchText);
    }
}
