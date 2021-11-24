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
        public PartialViewResult CategoryList()
        {

            return PartialView();
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
    }
}