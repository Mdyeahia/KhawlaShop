using Khawla.Data;
using Khawla.Entities;
using System;
using System.Collections.Generic;
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
        KhawlaDbContext context = new KhawlaDbContext();

        public void SaveCategory(Category category)
        {
            context.Categories.Add(category);
            context.SaveChanges();
        }
    }
}
