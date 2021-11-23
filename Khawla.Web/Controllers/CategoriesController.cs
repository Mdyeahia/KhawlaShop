using Khawla.Entities;
using Khawla.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
        public ActionResult Create(CategoryCreateViewModel model)
        {
            var newCategory = new Category();

            newCategory.Name = model.Name;
            newCategory.Description = model.Description;

            return RedirectToAction("CategoryList");
        }
    }
}