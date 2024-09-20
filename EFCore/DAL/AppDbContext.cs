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
        //public DbSet<People> Peoples { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductFeature> ProductFeatures { get; set; }
        public DbSet<ProductWithFeature> ProductWithFeatures { get; set; }
        public DbSet<ProductFull> ProductFulls { get; set; }

        //public DbSet<Student> Students { get; set; }
        //public DbSet<Teacher> Teachers { get; set; }
        //public DbSet<Employee> Employees { get; set; }
        //public DbSet<Manager> Managers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            Initializer.Build();
            //LazyLoadingAktif
            //optionsBuilder.LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information)
            //    .UseLazyLoadingProxies()
            //    .UseSqlServer(Initializer.configurationRoot.GetConnectionString("DefaultConnection"));

            optionsBuilder.LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information).UseSqlServer(Initializer.configurationRoot.GetConnectionString("DefaultConnection"));
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


            //modelBuilder.Entity<Manager>().OwnsOne(x=>x.Person);
            //modelBuilder.Entity<Employee>().OwnsOne(x=>x.Person);

            //attribute  tarafında NotMapped propertysinin karşılığı
            //modelBuilder.Entity<Product>().Ignore(x => x.Stock);

            //Sütun adı ve tipini değiştirmeyi FluentApı ile yapmayı burda da tutmak istedim
            //modelBuilder.Entity<Product>().Property(x=>x.Stock).HasColumnName("Stok").HasColumnType("int");

            ////Normal Index
            //modelBuilder.Entity<Product>().HasIndex(x=>x.Id);
            ////Composed Index
            //modelBuilder.Entity<Product>().HasIndex(x=> new { x.Id, x.Name });

            //context.Products.Where(x=>x.Name =="Kalem").Select(x=> new { name = x.Name , Price = x.Price, Stock = x.Stock});
            //Yukarıdaki gibi bir sorgu için, yani Name alaı ilk seçilip buna ait price ve stock ek olarak çekiliceği zamanlarda Included Columns Index tercih edilir
            //modelBuilder.Entity<Product>().HasIndex(x=>x.Id).IncludeProperties(x=> new { x.Price, x.Stock });

            //Veri tabanının tutarlı olması için Constraintler tanımlayabiliriz, Örneğin fiyat alanının indirimli fiyattan fazla olmasını zorunlu kılan constraint
            //modelBuilder.Entity<Product>().HasCheckConstraint("PriceAndDiscountPriceCheck", "[Price]>[DiscountPrice]");
            //Yukarıdaki işlemi hem veritabanı için hem de business kod tarafında yapmak tutarlılığı arttırıcaktır.

            ////Ön tanımlı Sql Cümleciği için
            //modelBuilder.Entity<ProductWithFeature>().ToSqlQuery("Select Price, Name from Products ");

            modelBuilder.Entity<ProductFull>().HasNoKey();

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
