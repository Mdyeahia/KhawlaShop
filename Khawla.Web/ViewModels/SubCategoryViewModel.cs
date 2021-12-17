using Khawla.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Khawla.Web.ViewModels
{
    public class SubCategoryCreateViewModel
    {
        public string Name { get; set; }
        public int CategoryId { get; set; }

        public int Id { get; set; }
        public SubCategory subCategory { get; set; }
        public List<Category> AllCategory { get; set; }
    }
    public class SubCategoryListingViewModel
    {
        public string SearchTerm { get; set; }
        public List<SubCategory> Allsubcategories { get; set; }
        public Pager Pager { get; set; }
    }
}