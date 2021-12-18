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
    public class SubCategoriesController : Controller
    {
        // GET: SubCategories
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult SubCategoryList(string search, int? pageNo)
        {
            var pageSize = 3;
            pageNo = pageNo ?? 1;

            SubCategoryListingViewModel model = new SubCategoryListingViewModel();

            model.SearchTerm = search;
            model.Allsubcategories = SubCategoriesService.Instance.FilterSubCategories(search, pageNo.Value, pageSize);

            var TotalCategory = CategoriesService.Instance.TotalCategoryCount(search);

            model.Pager = new Pager(TotalCategory, pageNo, pageSize);

            return PartialView(model);
        }
        public ActionResult Create()
        {
            SubCategoryCreateViewModel model = new SubCategoryCreateViewModel();
            model.AllCategory = CategoriesService.Instance.allCategory();
            return PartialView(model);
        }
        [HttpPost]
        public JsonResult Create(SubCategoryCreateViewModel model)
        {
            JsonResult result = new JsonResult();
            if (ModelState.IsValid)
            {
                var newSubCategory = new SubCategory();

                newSubCategory.SubCategoryName = model.Name;
                newSubCategory.CategoryID = model.CategoryId;
                
                SubCategoriesService.Instance.SaveSubCategory(newSubCategory);

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
            SubCategoryCreateViewModel model = new SubCategoryCreateViewModel();
            model.subCategory = SubCategoriesService.Instance.SubCategoryById(Id);

            return PartialView(model);
        }
        [HttpPost]
        public ActionResult Edit(SubCategoryCreateViewModel model)
        {
            var subcategory = SubCategoriesService.Instance.SubCategoryById(model.Id);

            subcategory.Id = model.Id;
            subcategory.SubCategoryName = model.Name;

            SubCategoriesService.Instance.UpdateSubCategory(subcategory);

            return RedirectToAction("SubCategoryList");
        }
        public ActionResult Delete(int Id)
        {
            SubCategoriesService.Instance.DeleteSubCategory(Id);

            return RedirectToAction("SubCategoryList");
        }

        public JsonResult SubcategoryByCategoryId(int? CategoryId)
        {
            var subcategories = SubCategoriesService.Instance.GetSubCategoryByCategoryId((int)CategoryId);
            return Json(subcategories, JsonRequestBehavior.AllowGet);
        }
    }
}