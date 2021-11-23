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
    }
    public class CategoryListingViewModel
    {
        public List<Category> Allcategories { get; set; }
    }
}