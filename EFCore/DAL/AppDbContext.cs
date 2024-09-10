using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.CodeFirst.DAL
{
    public class AppDbContext:DbContext
    {
        //DbSet classı tanımlanarak Tablo isminin Products olması, Product nesnesindeki Id propertysinin
        //Key olması tamamen EFCore sayesindedir.
        public DbSet<Product> Products { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            Initializer.Build();
            optionsBuilder.UseSqlServer(Initializer.configurationRoot.GetConnectionString("DefaultConnection"));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Burdan yapılan configurationslara Fluent Api denir ve Fluent Api migrationda Data Annotations Attributesden daha önceliklidir.DAA ignore edilir.
            //modelBuilder.Entity<Product>().ToTable("ProductTb");
            //modelBuilder.Entity<Product>().HasKey(x => x.Id);
            //modelBuilder.Entity<Product>().Property(x => x.Name).IsRequired().HasMaxLength(100).IsFixedLength();

            base.OnModelCreating(modelBuilder);
        }

        //public override int SaveChanges()
        //{
        //    ChangeTracker.Entries().ToList().ForEach(e => 
        //    {
        //        if (e.Entity is Product product) 
        //        {
        //            if (e.State == EntityState.Added) 
        //            {
        //                product.CreatedDate = DateTime.Now;
        //            }
        //        }
        //    });
        //    return base.SaveChanges();
        //}


    }
}
