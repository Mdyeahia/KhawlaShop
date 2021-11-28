using Khawla.Data;
using Khawla.Entities;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khawla.Service
{
    public class ProductsService
    {
        #region Singleton
        public static ProductsService Instance
        {
            get
            {
                if (instance == null) instance = new ProductsService();

                return instance;
            }
        }
        private static ProductsService instance { get; set; }

        private ProductsService()
        {
        }

        #endregion

        
        public Product GetProductById(int Id)
        {
            var context = new KhawlaDbContext();

            return context.Products.Include(a => a.ProductPictures).Include("ProductPictures.Picture").Where(a => a.ID == Id).First();

        }
        public List<Product> AllProduct()
        {
            var context = new KhawlaDbContext();

            return context.Products.Include(p => p.Category)
                .Include(p => p.ProductPictures)
                .Include("ProductPictures.PIcture").ToList();
        }
        public List<Product> FilterProduct(int? categoryId, string searchTerm, int pageNo, int pageSize)
        {
            var context = new KhawlaDbContext();

            var product = context.Products.Include(a =>a.Category).Include("ProductPictures.Picture").AsQueryable();
            if (categoryId.HasValue && categoryId.Value > 0)
            {
                product = product.Where(x => x.CategoryId == categoryId.Value);
            }
            if (!string.IsNullOrEmpty(searchTerm))
            {
                product = product.Where(x => x.Name.ToLower().Contains(searchTerm.ToLower()));
            }
            int skipCount = pageSize * (pageNo - 1);

            return product.OrderByDescending(a => a.ID).Skip(skipCount).Take(pageSize).ToList();

        }
        public int GetProductCount(int? categoryId, string searchTerm)
        {
            var context = new KhawlaDbContext();

            var product = context.Products.Include(a => a.Category).AsQueryable();
            if (categoryId.HasValue && categoryId.Value > 0)
            {
                product = product.Where(x => x.CategoryId == categoryId.Value);
            }
            if (!string.IsNullOrEmpty(searchTerm))
            {
                product = product.Where(x => x.Name.ToLower().Contains(searchTerm.ToLower()));
            }


            return product.Count();

        }
        public void SaveProduct(Product product)
        {
            KhawlaDbContext context = new KhawlaDbContext();
            context.Products.Add(product);
            context.SaveChanges();
        }
        public void UpdateProduct(Product product)
        {
            var context = new KhawlaDbContext();

            var exitproduct = context.Products.Where(x => x.ID == product.ID).Include(x => x.ProductPictures).First();
            context.ProductPictures.RemoveRange(exitproduct.ProductPictures);

            context.Entry(exitproduct).CurrentValues.SetValues(product);

            context.ProductPictures.AddRange(product.ProductPictures);

            context.SaveChanges();

        }
        public void Deleteproduct(int Id)
        {
            var context = new KhawlaDbContext();
            var deleteproduct = context.Products.Where(p=>p.ID==Id).Include(p=>p.ProductPictures).FirstOrDefault();

            context.ProductPictures.RemoveRange(deleteproduct.ProductPictures);
            context.Products.Remove(deleteproduct);
            
            context.SaveChanges();

        }
    }
}
