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
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductFeature> ProductFeatures { get; set; }
        //public DbSet<Student> Students { get; set; }
        //public DbSet<Teacher> Teachers { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            Initializer.Build();
            //LazyLoadingAktif
            optionsBuilder.LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information)
                .UseLazyLoadingProxies()
                .UseSqlServer(Initializer.configurationRoot.GetConnectionString("DefaultConnection"));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Burdan yapılan configurationslara Fluent Api denir ve Fluent Api migrationda Data Annotations Attributesden daha önceliklidir.DAA ignore edilir.
            //modelBuilder.Entity<Product>().ToTable("ProductTb");
            //modelBuilder.Entity<Product>().HasKey(x => x.Id);
            //modelBuilder.Entity<Product>().Property(x => x.Name).IsRequired().HasMaxLength(100).IsFixedLength();

            //one to many
            //modelBuilder.Entity<Category>().HasMany(x => x.Products).WithOne(x=>x.Category).HasForeignKey(x=>x.CategoryId);

            //one to one
            //modelBuilder.Entity<Product>().HasOne(x=>x.ProductFeature).WithOne(x => x.Product).HasForeignKey<ProductFeature>(x=>x.Id);

            //many to many
            //modelBuilder.Entity<Student>()
            //    .HasMany(x => x.Teachers).WithMany(x => x.Students)
            //    .UsingEntity<Dictionary<string, object>>(
            //    "StudentTeacherManyToManyAraTabloIsmi",
            //    x=>x.HasOne<Teacher>().WithMany().HasForeignKey("TeacherId").HasConstraintName("FK__TeacherId"),
            //    x=>x.HasOne<Student>().WithMany().HasForeignKey("StudentId").HasConstraintName("FK__StudentId")

            //    );

            ////Aşağıdaki çalışma veri tabanından bir parent tablosunda bir satır  delete edildiğinde bunu tutan tüm child satırlatr için yapılan işlemleri anlatır;
            ///Cascade ile bu parenta ait tüm childları siler
            //modelBuilder.Entity<Category>().HasMany(x => x.Products).WithOne(x=>x.Category).HasForeignKey(x=>x.CategoryId).OnDelete(DeleteBehavior.Cascade);
            ///Restrict ile eğer bu categoriye ait product varsa silemezsin. Veri tabanını tutarlı yapan bir özelliktir
            //modelBuilder.Entity<Category>().HasMany(x => x.Products).WithOne(x=>x.Category).HasForeignKey(x=>x.CategoryId).OnDelete(DeleteBehavior.Restrict);
            ///Ek olarak NoAction da sen bir işlem yapma ben veritabanında kendim halledicem? anlamına geliyor.
            //modelBuilder.Entity<Category>().HasMany(x => x.Products).WithOne(x=>x.Category).HasForeignKey(x=>x.CategoryId).OnDelete(DeleteBehavior.NoAction);
            //modelBuilder.Entity<Category>().HasMany(x => x.Products).WithOne(x=>x.Category).HasForeignKey(x=>x.CategoryId).OnDelete(DeleteBehavior.SetNull);

            ////Compute için Database attributenun oluşturulması
            //modelBuilder.Entity<Product>().Property(x => x.PriceKdv).HasComputedColumnSql("[Price]*[Kdv]");

            //DataBase attributelerinin fluent api ile tanımlanması
            //modelBuilder.Entity<Product>().Property(x => x.pricekdv).ValueGeneratedOnAdd();//Identity
            //modelBuilder.Entity<Product>().Property(x => x.pricekdv).ValueGeneratedOnAddOrUpdate();//Computed
            //modelBuilder.Entity<Product>().Property(x => x.pricekdv).ValueGeneratedNever();//None


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
