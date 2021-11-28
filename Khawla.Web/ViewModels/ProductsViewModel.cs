using Khawla.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Khawla.Web.ViewModels
{
    public class ProductsCreateViewModel
    {
        public string Name { get; set; }

        public string Description { get; set; }
        public string Summery { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public int CategoryId { get; set; }
        public  Category Category { get; set; }
        public List<ProductPicture> ProductPictureList { get; set; }
        public string ProductPictures { get; set; }
        public List<Category> Categories { get; set; }
        public int Id { get; set; }
        public Product Product { get; set; }
    }
    public class ProductListingViewModel
    {
        public int? CategoryId { get; set; }
        public string SearchTerm { get; set; }
        public List<ProductPicture> ProductPictures { get; set; }
        public List<Category> AllCategory { get; set; }
        public List<Product> AllProduct { get; set; }
        
        public Pager Pager { get; set; }
    }
    public class ProductDetalisViewModel
    {
        public Product Product { get; set; }
        
    }

}