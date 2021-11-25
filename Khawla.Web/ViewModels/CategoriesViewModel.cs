using Khawla.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Khawla.Web.ViewModels
{
    public class CategoryCreateViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string CategoryPictures { get; set; }
        public List<CategoryPicture> CategoryPictureList { get; set; }

        
    }
    public class CategoryListingViewModel
    {
        public List<Category> Allcategories { get; set; }
        public string SearchTerm { get; set; }
        public Pager Pager { get; set; }
    }
    public class EditCategoryViewModel
    {
        public int Id { get; set; }
        public Category category { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CategoryPictures { get; set; }
        
    }
}