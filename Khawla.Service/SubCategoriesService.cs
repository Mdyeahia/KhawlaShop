using Khawla.Data;
using Khawla.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;

namespace Khawla.Service
{
    public class SubCategoriesService
    {
        #region Singleton
        public static SubCategoriesService Instance
        {
            get
            {
                if (instance == null) instance = new SubCategoriesService();

                return instance;
            }
        }
        private static SubCategoriesService instance { get; set; }

        private SubCategoriesService()
        {
        }

        #endregion

        public List<SubCategory> allSubCategory()
        {
            KhawlaDbContext context = new KhawlaDbContext();
            return context.SubCategories.Include(m=>m.Category).ToList();
        }
        public List<SubCategory> FilterSubCategories(string searchTerm, int pageNo, int pageSize)
        {
            KhawlaDbContext context = new KhawlaDbContext();
            var subcategoy = context.SubCategories.Include(m=>m.Category).AsQueryable();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                subcategoy = subcategoy.Where(x => x.SubCategoryName.ToLower().Contains(searchTerm.ToLower()));
            }
            int skipCount = pageSize * (pageNo - 1);

            return subcategoy.OrderByDescending(a => a.Id).Skip(skipCount).Take(pageSize).ToList();
        }
        public int TotalSubCategoryCount(string searchTerm)
        {
            var context = new KhawlaDbContext();

            var SubCategory = context.SubCategories.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                SubCategory = SubCategory.Where(x => x.SubCategoryName.ToLower().Contains(searchTerm.ToLower()));
            }


            return SubCategory.Count();

        }

        public SubCategory SubCategoryById(int Id)
        {
            KhawlaDbContext context = new KhawlaDbContext();
            return context.SubCategories.Where(x => x.Id == Id).First();
        }
        public void SaveSubCategory(SubCategory SubCategory)
        {
            KhawlaDbContext context = new KhawlaDbContext();
            context.SubCategories.Add(SubCategory);
            context.SaveChanges();
        }
        public void UpdateSubCategory(SubCategory SubCategory)
        {
            KhawlaDbContext context = new KhawlaDbContext();

            context.Entry(SubCategory).State = System.Data.Entity.EntityState.Modified;

            context.SaveChanges();
        }
        public void DeleteSubCategory(int Id)
        {
            KhawlaDbContext context = new KhawlaDbContext();

            var deleteSubCategory = context.SubCategories.Where(x => x.Id == Id).FirstOrDefault();
            
            context.Products.RemoveRange(deleteSubCategory.Products);

            context.SubCategories.Remove(deleteSubCategory);
            context.SaveChanges();
        }
    }
}
