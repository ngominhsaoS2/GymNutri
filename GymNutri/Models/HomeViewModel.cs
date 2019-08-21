using System.Collections.Generic;
using GymNutri.Data.ViewModels.Blog;
using GymNutri.Data.ViewModels.Common;

namespace GymNutri.Models
{
    public class HomeViewModel
    {
        public List<BlogViewModel> LastestBlogs { get; set; }

        public List<SlideViewModel> HomeSlides { get; set; }

        public string Title { set; get; }

        public string MetaKeyword { set; get; }

        public string MetaDescription { set; get; }
    }
}
