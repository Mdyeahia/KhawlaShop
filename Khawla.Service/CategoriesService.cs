using Khawla.Data;
using Khawla.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khawla.Service
{
    public class CategoriesService
    {
        #region Singleton
        public static CategoriesService Instance
        {
            get
            {
                if (instance == null) instance = new CategoriesService();

                return instance;
            }
        }
        private static CategoriesService instance { get; set; }

        private CategoriesService()
        {
        }

        #endregion
        

        public List<Category> allCategory()
        {
            KhawlaDbContext context = new KhawlaDbContext();
            return context.Categories.ToList();
        }
        public Category categoryById(int Id)
        {
            KhawlaDbContext context = new KhawlaDbContext();
            return context.Categories.Include(x => x.CategoryPictures).Include("CategoryPictures.Picture").Where(x => x.ID == Id).First();
        }
        public List<Category> FilterCategories(string searchTerm,int pageNo,int pageSize)
        {
            KhawlaDbContext context = new KhawlaDbContext();
            var categoy = context.Categories.AsQueryable();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                categoy = categoy.Where(x => x.Name.ToLower().Contains(searchTerm.ToLower()));
            }
            int skipCount = pageSize * (pageNo - 1);

            return categoy.OrderByDescending(a => a.ID).Skip(skipCount).Take(pageSize).ToList();
        }
        public int TotalCategoryCount(string searchTerm)
        {
            var context = new KhawlaDbContext();

            var category = context.Categories.AsQueryable();
            
            if (!string.IsNullOrEmpty(searchTerm))
            {
                category = category.Where(x => x.Name.ToLower().Contains(searchTerm.ToLower()));
            }


            return category.Count();

        }

        public void UpdateCategory(Category category)
        {
            KhawlaDbContext context = new KhawlaDbContext();
            var exitCategory = context.Categories.Where(x => x.ID == category.ID).Include(x => x.CategoryPictures).First();
            
            context.CategoryPictures.RemoveRange(exitCategory.CategoryPictures);

            context.Entry(exitCategory).CurrentValues.SetValues(category);

            context.CategoryPictures.AddRange(category.CategoryPictures);

            context.SaveChanges();
        }
        public void SaveCategory(Category category)
        {
            KhawlaDbContext context = new KhawlaDbContext();
            context.Categories.Add(category);
            context.SaveChanges();
        }
        public void DeleteCategory(int Id)
        {
            KhawlaDbContext context = new KhawlaDbContext();

            var deleteCategory = context.Categories.Where(x => x.ID ==Id).Include(x => x.CategoryPictures).Include(x=>x.Products).FirstOrDefault();

            context.CategoryPictures.RemoveRange(deleteCategory.CategoryPictures);
            context.Products.RemoveRange(deleteCategory.Products);

            context.Categories.Remove(deleteCategory);
            context.SaveChanges();
        }
    }
}
