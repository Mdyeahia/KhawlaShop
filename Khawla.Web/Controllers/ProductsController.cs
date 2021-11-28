using Khawla.Entities;
using Khawla.Service;
using Khawla.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Khawla.Web.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult ProductList(int? categoryId, string search, int? pageNo)
        {
            var pageSize = 3;
            pageNo = pageNo ?? 1;

            ProductListingViewModel model = new ProductListingViewModel();
            //model.PageNo= pageNo ?? 1;
            model.CategoryId = categoryId;
            model.SearchTerm = search;
            
            model.AllProduct = ProductsService.Instance.FilterProduct(categoryId, search, pageNo.Value, pageSize);
            model.AllCategory = CategoriesService.Instance.allCategory();
            var totalProducts = ProductsService.Instance.GetProductCount(categoryId, search);

            model.Pager = new Pager(totalProducts, pageNo, pageSize);

            return PartialView(model);
        }

        public ActionResult Create()
        {
            ProductsCreateViewModel model = new ProductsCreateViewModel();
            model.Categories = CategoriesService.Instance.allCategory();
            return PartialView(model);
        }
        [HttpPost]
        public JsonResult Create(ProductsCreateViewModel model)
        {
            JsonResult result = new JsonResult();
            if (ModelState.IsValid)
            {
                var newProduct = new Product();

                newProduct.Name = model.Name;
                newProduct.Description = model.Description;
                newProduct.Summery = model.Summery;
                newProduct.Price = model.Price;
                newProduct.Quantity = model.Quantity;
                newProduct.CategoryId = model.CategoryId;

                if (!string.IsNullOrEmpty(model.ProductPictures))
                {
                    var pictureId = model.ProductPictures
                        .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(int.Parse).ToList();
                    newProduct.ProductPictures = new List<ProductPicture>();
                    newProduct.ProductPictures.AddRange(pictureId.Select(s => new ProductPicture()
                    { ProductId = newProduct.ID, PictureId = s }).ToList());
                }
                ProductsService.Instance.SaveProduct(newProduct);

                result.Data = new { success = true };
            }
            else
            {
                result.Data = new { success = false, message = "Invalid Inputs" };
            }
            return result;
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            ProductsCreateViewModel model = new ProductsCreateViewModel();

            model.Product = ProductsService.Instance.GetProductById(Id);
            model.Categories = CategoriesService.Instance.allCategory();


            return PartialView(model);
        }
        [HttpPost]
        public ActionResult Edit(ProductsCreateViewModel model)
        {
            
            var Product = ProductsService.Instance.GetProductById(model.Id);
            Product.Name = model.Name;
            Product.Summery = model.Summery;
            Product.Description = model.Description;
            Product.Price = model.Price;
            Product.Quantity = model.Quantity;
            Product.CategoryId = model.CategoryId;


            if (!string.IsNullOrEmpty(model.ProductPictures))
            {
                var pictureIds = model.ProductPictures.Split(',').Select(int.Parse);
                Product.ProductPictures = new List<ProductPicture>();
                Product.ProductPictures.AddRange(pictureIds.Select(x => new ProductPicture() { ProductId = Product.ID, PictureId = x }).ToList());

            }



            ProductsService.Instance.UpdateProduct(Product);

            return RedirectToAction("ProductList");
        }

        [HttpGet]
        public ActionResult Details(int Id)
        {
            ProductDetalisViewModel model = new ProductDetalisViewModel();

            model.Product = ProductsService.Instance.GetProductById(Id);
            


            return View(model);
        }
        [HttpPost]
        public ActionResult Delete(int Id)
        {

            ProductsService.Instance.Deleteproduct(Id);
            return RedirectToAction("ProductList");
        }

    }
}