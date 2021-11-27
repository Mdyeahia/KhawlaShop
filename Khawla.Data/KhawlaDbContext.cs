using Khawla.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khawla.Data
{
    public class KhawlaDbContext : IdentityDbContext<KhawlaUser>
    {
        public KhawlaDbContext(): base("KhawlaConnection")
        {
        }


        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<ProductPicture> ProductPictures { get; set; }
        public DbSet<CategoryPicture> CategoryPictures { get; set; }

        public static KhawlaDbContext Create()
        {
            return new KhawlaDbContext();
        }
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<CategoryPicture>()
        //    .HasRequired(b => b.Category)
        //    .WithMany(b => b.CategoryPictures)
        //    .WillCascadeOnDelete(true);

        //    modelBuilder.Entity<Product>()
        //   .HasRequired(b => b.Category)
        //   .WithRequiredPrincipal()
        //   .WillCascadeOnDelete(true);
        //}
    }
}
