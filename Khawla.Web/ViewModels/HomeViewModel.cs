using Khawla.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Khawla.Web.ViewModels
{
    public class HomeViewModel
    {
        public string SearchTerm { get; set; }
        public Pager Pager { get; set; }
        public List<Category> AllCategory { get; set; }
        public List<SubCategory> AllSubCategory { get; set; }
        public List<Product>AllProduct { get; set; }
    }
}