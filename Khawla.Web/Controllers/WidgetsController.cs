using Khawla.Service;
using Khawla.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Khawla.Web.Controllers
{
    public class WidgetsController : Controller
    {
        // GET: Widgets
        public ActionResult Products(bool isFeatured,bool isLatestProduct, bool topRated, bool topReview,int? CategoryID = 0)
        {
            
            ProductsWidgetViewModel model = new ProductsWidgetViewModel();
            model.Isfeatured = isFeatured;
            model.IsLatestProduct = isLatestProduct;
            model.toprated = topRated;
            model.topreview = topReview;
            if (isFeatured)
            {
                model.AllWidgetProduct = ProductsService.Instance.GetFeatureProduct();
            }
            else if (isLatestProduct)
            {
                model.AllWidgetProduct = ProductsService.Instance.GetLatestProduct(12);
            }
            else if (topRated)
            {
                model.AllWidgetProduct = ProductsService.Instance.GetLatestProduct(12);
            }
            else if (topReview)
            {
                model.AllWidgetProduct = ProductsService.Instance.GetLatestProduct(12);
            }
            else if (CategoryID.HasValue && CategoryID.Value > 0)
            {
                model.AllWidgetProduct = ProductsService.Instance.GetWidgetProductsbyCategory(CategoryID.Value, 4);
            }
            else
            {
                model.AllWidgetProduct = ProductsService.Instance.GetProducts(1, 8);
            }


            return PartialView(model);
        }
        [HttpGet]
        [ChildActionOnly]
        public ActionResult CategoryDropDownWidget()
        {
            HomeViewModel model = new HomeViewModel();
            model.AllCategory = CategoriesService.Instance.allCategory();
            return PartialView("~/Views/Widgets/_CategoryDropDown.cshtml",model);
        }
        [HttpGet]
        [ChildActionOnly]
        public ActionResult SearchingWidget(string searchterm, int? pageNo)
        {
            var pageSize = 3;
            pageNo = pageNo ?? 1;
            HomeViewModel model = new HomeViewModel();
            model.SearchTerm = searchterm;
            model.AllProduct = ProductsService.Instance.FilterProduct(searchterm,pageNo.Value, pageSize);
            var totalProducts = ProductsService.Instance.GetProductCount(searchterm);

            model.Pager = new Pager(totalProducts, pageNo, pageSize);

            return PartialView("~/Views/Widgets/_SearchingWidget.cshtml", model);
        }
       
    }
}