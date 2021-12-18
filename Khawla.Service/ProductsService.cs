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
        public List<Product> GetProducts(int pageNo, int pageSize)
        {
            var context = new KhawlaDbContext();
            
                return context.Products.OrderByDescending(p => p.ID)
                    .Skip((pageNo - 1) * pageSize)
                    .Take(pageSize)
                    .Include(p => p.Category).ToList();
            
        }
        public List<Product> FilterProduct(int? categoryId, string searchTerm, int pageNo, int pageSize)
        {
            var context = new KhawlaDbContext();

            var product = context.Products.Include(a =>a.Category).Include(a => a.SubCategory).Include("ProductPictures.Picture").AsQueryable();
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
        public List<Product> FilterProduct(string searchTerm, int pageNo, int pageSize)
        {
            var context = new KhawlaDbContext();

            var product = context.Products.Include(a => a.Category).Include(a => a.SubCategory).Include("ProductPictures.Picture").AsQueryable();
            
            if (!string.IsNullOrEmpty(searchTerm))
            {
                product = product.Where(x => x.Name.ToLower().Contains(searchTerm.ToLower()));
            }
            int skipCount = pageSize * (pageNo - 1);

            return product.OrderByDescending(a => a.ID).Skip(skipCount).Take(pageSize).ToList();

        }
        public List<Product> SearchProducts(string searchTerm, int? maximumPrice, int? minimumPrice, int? categoryId, int? sortBy, int pageNo, int pageSize)
        {
            using (var context = new KhawlaDbContext())
            {
                var products = context.Products.Include(p => p.Category)
                    .Include(a => a.SubCategory)
                .Include(p => p.ProductPictures)
                .Include("ProductPictures.Picture").ToList();
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    products = products.Where(p => p.Name.ToLower().Contains(searchTerm.ToLower())).ToList();
                }

                if (maximumPrice.HasValue)
                {
                    products = products.Where(p => p.Price >= minimumPrice.Value).ToList();
                }

                if (minimumPrice.HasValue)
                {
                    products = products.Where(p => p.Price <= maximumPrice.Value).ToList();
                }

                if (categoryId.HasValue)
                {
                    products = products.Where(p => p.Category.ID == categoryId.Value).ToList();
                }

                if (sortBy.HasValue)
                {
                    switch (sortBy.Value)
                    {
                        case 2:
                            products = products.OrderByDescending(x => x.ID).ToList();
                            break;
                        case 3:
                            products = products.OrderBy(x => x.Price).ToList();
                            break;
                        default:
                            products = products.OrderByDescending(x => x.Price).ToList();
                            break;
                    }
                }

                //return products;
                return products.Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();
            }
        }
        public int SearchProductsCount(string searchTerm, int? maximumPrice, int? minimumPrice, int? categoryId,
          int? sortBy)
        {
            using (var context = new KhawlaDbContext())
            {
                var products = context.Products.ToList();
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    products = products.Where(p => p.Name.ToLower().Contains(searchTerm.ToLower())).ToList();
                }

                if (maximumPrice.HasValue)
                {
                    products = products.Where(p => p.Price >= minimumPrice.Value).ToList();
                }

                if (minimumPrice.HasValue)
                {
                    products = products.Where(p => p.Price <= maximumPrice.Value).ToList();
                }

                if (categoryId.HasValue)
                {
                    products = products.Where(p => p.Category.ID == categoryId.Value).ToList();
                }

                if (sortBy.HasValue)
                {
                    switch (sortBy.Value)
                    {
                        case 2:
                            products = products.OrderByDescending(x => x.ID).ToList();
                            break;
                        case 3:
                            products = products.OrderBy(x => x.Price).ToList();
                            break;
                        default:
                            products = products.OrderByDescending(x => x.Price).ToList();
                            break;
                    }
                }

                return products.Count;

            }
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
        public int GetProductCount(string searchTerm)
        {
            var context = new KhawlaDbContext();

            var product = context.Products.Include(a => a.Category).AsQueryable();
           
            if (!string.IsNullOrEmpty(searchTerm))
            {
                product = product.Where(x => x.Name.ToLower().Contains(searchTerm.ToLower()));
            }


            return product.Count();

        }
        public List<Product> GetFeatureProduct()
        {
            var context = new KhawlaDbContext();

            return context.Products.Include(c => c.Category)
                .Include(c=>c.ProductPictures)
                .Include("ProductPictures.Picture")
                .Where(c => c.Category.IsFeatured == true).Take(8).ToList();
        }
       
        public List<Product> GetWidgetProductsbyCategory(int categoryID, int pageSize)
        {
            var context = new KhawlaDbContext();

            return context.Products.Where(p => p.Category.ID == categoryID)
                    .OrderByDescending(p => p.ID)
                    .Take(pageSize)
                    .Include(p => p.Category)
                    .ToList();
            
        }
        public List<Product> GetLatestProduct(int numberofProducts)
        {
            KhawlaDbContext context = new KhawlaDbContext();
            return context.Products.OrderByDescending(p => p.ID)
                .Include(c => c.ProductPictures)
                .Include("ProductPictures.Picture")
                .Take(numberofProducts).Include(p => p.Category)
                    .ToList();
        }

        public int GetMaximumPrice()
        {
            KhawlaDbContext context = new KhawlaDbContext();
            
           return (int)(context.Products.Max(p => p.Price));
            
        }
        public int GetMinimumPrice()
        {
            KhawlaDbContext context = new KhawlaDbContext();

            return (int)(context.Products.Min(p => p.Price));

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
