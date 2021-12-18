using Khawla.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Khawla.Web.ViewModels
{
    public class ShopViewModel
    {
        public List<Product> Products { get; set; }
        public List<Product> LatestProduct { get; set; }
        public int MaximumPrice { get; set; }
        public int MinimumPrice { get; set; }
        public List<Category> FeaturedCategories { get; set; }
        public List<Category> AllCategory { get; set; }
        public int? SortBy { get; set; }
        public int? CategoryID { get; set; }
        public int? SubCategoryID { get; set; }
        public Pager Pager { get; set; }
        public string SearchTerm { get; set; }
    }
    public class FilterProductsViewModel
    {
        public List<Product> Products { get; set; }
        public Pager Pager { get; set; }
        public int? SortBy { get; set; }
        public int? CategoryID { get; set; }
        public int? SubCategoryID { get; set; }
        public string SearchTerm { get; set; }
    }
}