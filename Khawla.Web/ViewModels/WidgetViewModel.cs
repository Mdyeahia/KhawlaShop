using Khawla.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Khawla.Web.ViewModels
{
    public class ProductsWidgetViewModel
    {
        public bool Isfeatured { get; set; }
        public bool IsLatestProduct { get; set; }
        
        public List<Product> AllWidgetProduct { get; set; }
        public List<Category> AllWidgetCategory { get; set; }
    }
    public class HeroSectionViewModel
    {
        public List<Category> AllCategory { get; set; }
    }
}