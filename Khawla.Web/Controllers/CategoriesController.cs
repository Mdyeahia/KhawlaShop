using Khawla.Entities;
using Khawla.Web.ViewModels;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Khawla.Service;

namespace Khawla.Web.Controllers
{
    public class CategoriesController : Controller
    {
        // GET: Categories
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult CategoryList(string search,int? pageNo)
        {
            var pageSize = 3;
            pageNo = pageNo ?? 1;

            CategoryListingViewModel model = new CategoryListingViewModel();

            model.SearchTerm = search;
            model.Allcategories = CategoriesService.Instance.FilterCategories(search,pageNo.Value,pageSize);

            var TotalCategory = CategoriesService.Instance.TotalCategoryCount(search);

            model.Pager = new Pager(TotalCategory, pageNo, pageSize);

            return PartialView(model);
        }
        public ActionResult Create()
        {
            
            return PartialView();
        }
        [HttpPost]
        public JsonResult Create(CategoryCreateViewModel model)
        {
            JsonResult result = new JsonResult();
            if (ModelState.IsValid)
            {
                var newCategory = new Category();

                newCategory.Name = model.Name;
                newCategory.Description = model.Description;
                newCategory.IsFeatured = model.IsFeatured;
                if (!string.IsNullOrEmpty(model.CategoryPictures))
                {
                    var pictureId = model.CategoryPictures
                        .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(int.Parse).ToList();
                    newCategory.CategoryPictures = new List<CategoryPicture>();
                    newCategory.CategoryPictures.AddRange(pictureId.Select(s => new CategoryPicture()
                    { CategoryId = newCategory.ID, PictureId = s }).ToList());
                }
                CategoriesService.Instance.SaveCategory(newCategory);

                    result.Data = new { success = true };
                }
                else
                {
                    result.Data = new { success = false, message = "Invalid Inputs" };
                }
                return result;
            }

        public ActionResult Edit(int Id)
        {
            EditCategoryViewModel model = new EditCategoryViewModel();
            model.category = CategoriesService.Instance.categoryById(Id);

            return PartialView(model);
        }
        [HttpPost]
        public ActionResult Edit(EditCategoryViewModel model)
        {
            var category = CategoriesService.Instance.categoryById(model.Id);

            category.ID = model.Id;
            category.Name = model.Name;
            category.Description = model.Description;
            category.IsFeatured = model.IsFeatured;
            if (!string.IsNullOrEmpty(model.CategoryPictures))
            {
                var pictureId = model.CategoryPictures
                      .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                      .Select(int.Parse).ToList();
                category.CategoryPictures = new List<CategoryPicture>();
                category.CategoryPictures.AddRange(pictureId.Select(s => new CategoryPicture()
                { CategoryId = category.ID, PictureId = s }).ToList());
            }
            CategoriesService.Instance.UpdateCategory(category);

            return RedirectToAction("CategoryList");
        }
        public ActionResult Delete(int Id)
        {
            CategoriesService.Instance.DeleteCategory(Id);

            return RedirectToAction("CategoryList");
        }
    }
}