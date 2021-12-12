using Khawla.Service;
using Khawla.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Khawla.Web.Controllers
{
    public class ShopController : Controller
    {
        // GET: Shop
        public ActionResult Index(string searchTerm, int? maximumPrice, int? minimumPrice, int? categoryID, int? sortBy, int? pageNo)
        {
            var pageSize = 12;
            pageNo = pageNo ?? 1;

            ShopViewModel model = new ShopViewModel();

            model.LatestProduct = ProductsService.Instance.GetLatestProduct(6);

            model.SearchTerm = searchTerm;
            model.FeaturedCategories = CategoriesService.Instance.GetFeatureCategories();
            model.AllCategory= CategoriesService.Instance.allCategory();
            model.MaximumPrice = ProductsService.Instance.GetMaximumPrice();
            model.MinimumPrice = ProductsService.Instance.GetMinimumPrice();
            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;
            model.CategoryID = categoryID;
            model.SortBy = sortBy;
            model.Products = ProductsService.Instance.SearchProducts(searchTerm, maximumPrice, minimumPrice, categoryID, sortBy, pageNo.Value, pageSize);
            
            int totalCount = ProductsService.Instance.SearchProductsCount(searchTerm, maximumPrice, minimumPrice, categoryID, sortBy);
            model.Pager = new Pager(totalCount, pageNo, pageSize);

            return View(model);
        }
        public ActionResult FilterProducts(string searchTerm, int? maximumPrice, int? minimumPrice, int? categoryID, int? sortBy, int? pageNo)
        {
            var pageSize = 12;
            pageNo = pageNo ?? 1;

            FilterProductsViewModel model = new FilterProductsViewModel();

            model.SearchTerm = searchTerm;
            model.CategoryID = categoryID;
            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;
            model.SortBy = sortBy;
            int totalCount = ProductsService.Instance.SearchProductsCount(searchTerm, maximumPrice, minimumPrice, categoryID, sortBy);
            model.Products = ProductsService.Instance.SearchProducts(searchTerm, maximumPrice, minimumPrice, categoryID, sortBy, pageNo.Value, pageSize);

            model.Pager = new Pager(totalCount, pageNo, pageSize);

            return PartialView(model);
        }
    }
}