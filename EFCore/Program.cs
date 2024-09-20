using EFCore.CodeFirst;
using EFCore.CodeFirst.DAL;
using Microsoft.EntityFrameworkCore;
using System.Text;

Initializer.Build();

using (var context = new AppDbContext())
{
    //Aşağıdaki işlemlerde stateleri anlamak önemli
    //Eğer Detached ise memoryden okunmuyordur, kod üzerinden okunuyodur, bu state ile savechange desem bile dbye yansımayacaktır.
    //Eğer Added ise veri tabanına daha yansıtılmamıştır,
    //Eğer Unchanged ise Veritabanından değişiklik yapılmadna direkt okunuyordur. 

    //var newProduct = new Product { Name = "Kalem 200", Price = 200, Stock = 100, Barcode = 234 };

    //Console.WriteLine($"İlk state:{context.Entry(newProduct).State}");

    //context.Entry(newProduct).State = EntityState.Modified;
    //context.Entry(newProduct).State = EntityState.Deleted;
    //context.Entry(newProduct).State = EntityState.Unchanged;
    //context.Entry(newProduct).State = EntityState.Added;
    //context.Entry(newProduct).State = EntityState.Detached;
    //////////////////////////////////////////////////////////////////////
    //context.Entry(newProduct).State = EntityState.Added;
    //Yukarıdaki işlem ile Add methodu aynı işlevdedir
    //await context.AddAsync( newProduct );
    //////////////////////////////////////////////////////////////////////

    //Console.WriteLine($"son state:{context.Entry(newProduct).State}");

    //await context.SaveChangesAsync();

    //Console.WriteLine($"state after saveChangeAsync:{context.Entry(newProduct).State}");



    //////////////////////////////////////////////////////////////////////

    //var products = await context.Products.AsNoTracking().ToListAsync();
    //Burda AsNoTracking methodu ef core tarafında Dbden dönen değerleri memoryde track etmemeye yarıyor

    //products.ForEach(p =>
    //{
    //    var state = context.Entry(p).State;
    //    Console.WriteLine($"{p.Id}:{p.Name}- {p.Price} state:{state}");
    //});
    //////////////////////////////////////////////////////////////////////

    //context.Products.Add(new() { Name = "Kalem 9", Price = 89, Stock = 100, Barcode = 789 });
    //context.Products.Add(new() { Name = "Kalem 19", Price = 189, Stock = 200, Barcode = 589 });
    //context.Products.Add(new() { Name = "Kalem 29", Price = 289, Stock = 300, Barcode = 689 });

    //Yukarıda Add Methodu ile memoryde track edilen datalara AppDbContext sınıfı içerisnde Track edilen entityler içerisinden belirli işlemler yapılmıştır.
    //Her SaveChanges methodu öncesi bu işlemlerin yaazılmaması için SaveChanges methodu AppDbContext içinde override edilip bu track edilen entityler üzerrindeki işlemler
    //buraya alınmıştır.

    //context.SaveChanges();
    //////////////////////////////////////////////////////////////////////

    //var products = context.Products.ToList();
    //products.ForEach(p =>
    //{
    //    p.Stock += 100;

    //    context.Products.Update(p);
    //});

    //context.SaveChanges();

    //Burdaki yanlış şu p.Stock +=100 işleminden sonra zaten bu productın statei Modified olarak değiştiriliyor
    //Ek olarak Update methodunu çağırmaya gerek yok
    //Update methodu ancak şu şekilde kullanılabilir;
    //context.Update(new Product() { Id = 1, Name = "Kasım" });
    //Yani demek istediğim direkt modified yaptığım yerde düzenleme yaparsam update ile yapabilirim.
    //////////////////////////////////////////////////////////////////////

    //One to many ilişki için data ekleme 
    //var category = new Category { Name = "Defter" };
    //var product = new Product { Name ="Defterinho",Price = 100, Stock = 200, Barcode=123,Category=category,CategoryId=category.Id };
    ////veya
    //category.Products.Add(new() { Name = "Defterdarbaba",Price=137,Stock=250,Barcode=222 });
    //context.Products.Add(product);
    // context.SaveChanges();

    //////////////////////////////////////////////////////////////////////

    //OnetoOne ilişki için data ekleme

    //var product = new Product() { Name = "silgi5", Price = 35, Stock = 900, Barcode = 345, Category = new Category() { Name = "Silgi" }, ProductFeature = new ProductFeature { Width = 78, Height = 45, Color = "Orange" } };
    ////Veya

    //var category = context.Categories.First(x=>x.Name == "Silgi");

    //var product = new Product() { Name = "silgi7", Price = 35, Stock = 900, Barcode = 345, Category = category };

    //var productFeature = new ProductFeature() { Color="kara",Width=250,Height=45,Product= product};

    //context.ProductFeatures.Add(productFeature);
    //context.SaveChanges();
    //var teacher = new Teacher() { Name = "Canan Hoca" };
    //teacher.Students.Add(new() { Age = 12, Name= "Kemal"});
    //context.Add(teacher);
    //context.SaveChanges();
    //Console.WriteLine("OK");

    //var student = new Student() { Name = "Osman", Age = 16 };
    //student.Teachers.Add(new() { Name = "Ulema Hoca" });
    //context.Add(student);
    //context.SaveChanges();

    ////veya
    //var teacher = context.Teachers.First(x => x.Name == "Ulema Hoca");
    //teacher.Students.AddRange(new List<Student> { new() { Name = "Bino",Age = 17 },new() { Name = "Hüso", Age = 18 } });
    //context.SaveChanges();

    ////Delete Behaviour Cascade
    //var category = context.Categories.First(x => x.Name == "Kalembaba");
    //var category = new Category()
    //{
    //    Name = "Kalembaba",
    //    Products = new List<Product>()
    //{
    //    new(){ Name="kalembaba1",Price=200,Stock = 240, Barcode=764 },
    //    new(){ Name="kalembaba2",Price=250,Stock = 260, Barcode=765 },
    //    new(){ Name="kalembaba3",Price=290,Stock = 440, Barcode=766 }
    //}
    //};
    //context.Remove(category);
    //context.Add(category);
    //context.SaveChanges();

    //var product = new Product() { Name = "Kebabçi", Price = 250, Barcode = 1234, Stock = 150, Kdv = 18 };
    //context.Products.Add(product);
    //context.SaveChanges();


    //Related Data Load - Eager Loading
    //Navigation propertyler üzerinden ilgili entitylerin yüklenmesiişlemlerine data load deniyor.
    //Eager loading ise örneğin product entitysini çekmek isterken aynı zamanda product featureuda çekmek istersek bu eager loadingdir
    //var categoryWithProducts = context.Categories.Include(c => c.Products).ThenInclude(x=>x.ProductFeature).First();

    //categoryWithProducts.Products.ForEach(product =>
    //Console.WriteLine($"{categoryWithProducts.Name} {product.Name} {product.ProductFeature.Color}")

    //Related Data Load - Explicit Loading
    //Bir datayi çektiğimde navigation propertyleri kullanıp kullanmayacağım kodun devamında belli olacaksa bu loading kullanılır
    //var category = context.Categories.First();
    ////
    ////
    ////
    //if (true)
    //{
    //    context.Entry(category).Collection(x=>x.Products).Load();

    //    category.Products.ForEach(x =>
    //    {
    //        Console.WriteLine(x.Name);
    //    });
    //}
    ////Birebir ilşikli olan Navigation Propertylerde
    //var product = context.Products.First();

    //if (true) 
    //{
    //    //Eğer navigation propertyler olmasa aşağıdaki satırı kullanırdık
    //    context.ProductFeatures.Where(x => x.Id == product.Id).First();

    //    context.Entry(product).Reference(x=>x.ProductFeature).Load();
    //}

    //Related Data Load - Lazy Loading
    //EF Coreun Lazy loading özelliği enable edilirse ortaya çıkar. +/- Yanları vardır.- Yanı iki defa veritabanından data çeker. Aynı Exlicit loadingde olduğu gibi.
    //Lazy loadingi açmak yani enable etmek için gereken kütüphane; Microsoft.EntityFrameworkCore.Proxies.
    //var category = context.Categories.First();
    //Console.WriteLine("ÇEKTİK");
    //var products = category.Products;
    //Console.WriteLine("Bitti");

    //Ama lazy loadingin en büyük handikapı foreach döngülerinde ortaya çıkar
    //Çünkü ilgili itemi alırken her defasında dbye sorgu basar. Bknz;

    //var category = context.Categories.First();
    //Console.WriteLine("ÇEKTİK");
    //var products = category.Products;
    //foreach (var item in products)
    //{
    //    var productFeature = item.ProductFeature;
    //}

    ////////////////////////////////Client and Server Evalution
    //var person = context.Peoples.Where(x => FormatPhone(x.Phone) == "5551482071").ToList();
    ////Yukarıda hata alma sebebi Peoples tablosundan yapılan sorgunun tamamen Server side olması ve Serverside bi sorguda local bir fonksiyon kullanamazsın.
    //var person = context.Peoples.ToList().Where(x => FormatPhone(x.Phone) == "5551482071").ToList();
    ////Yukarıdaki method Peoples tablosundaki verileri tolist yaparak clienta çeker ver onun ardından Where sorgusu yaptığı için hata almaz.Client sorgusunda local fonksiyon çalıştırılabilir. 
    //string FormatPhone(string phone)
    //{
    //    return phone.Substring(1, phone.Length - 1);
    //}

    //////////////////Inner Join
    //var result = context.Categories.Join(context.Products,x=>x.Id,y=>y.CategoryId,(c,p) => p).ToList();

    //var result2 = (from c in context.Categories
    //              join p in context.Products on c.Id equals p.CategoryId
    //              join pf in context.ProductFeatures on p.Id equals pf.Id
    //              select new
    //              {
    //                  CategoryName = c.Name,
    //                  ProductName = p.Name,
    //                  ProductFeatureColor = pf.Color
    //              }).ToList();
    //////////////////Left Join
    //var result = await (from p in context.Products
    //                    join pf in context.ProductFeatures on p.Id equals pf.Id into pflist
    //                    from pf in pflist.DefaultIfEmpty()
    //                    select new { p }).ToListAsync();

    ////////////////////Raw Sql
    //decimal price = 10;
    //var product = await context.Products.FromSqlRaw("select * from Products where Price>{0}",price).ToListAsync();
    //var products = await context.Products.FromSqlInterpolated($"select * from Products where Price>{price}").ToListAsync();

    //Sqlden ham sorgu ile getirdiğimiz datalar ile entitymiz uymaz ise hata alırız
    //var productWithFeature = await context.ProductFeatures.FromSqlRaw("  select p.Id, p.Name,p.Price,pf.Color,pf.Height from Products as p " +
    //    "  join ProductFeatures as pf on p.Id = pf.Id").ToListAsync();
    //Yukarıdaki koddan dönen hatayı düzeltmek için ham sorgudan dönen propertylere uygun entity oluşturmak gerekir
    //var productWithFeatures = await context.ProductWithFeatures.FromSqlRaw("  select p.Id, p.Name,p.Price,pf.Color,pf.Height from Products as p " +
    //    "  join ProductFeatures as pf on p.Id = pf.Id").ToListAsync();

    //Ön tanımlı Sql cümleciği için ToSqlQuery methodu AppDbContextde ilgili entity için modelBuildera eklenir.
    //Ve bu şekilde ilgili entity için sabit bir sorgumuz var ise her defasıdna tekrardan yazmaktansa bir kere yazarak hallederiz
    //Aşağıdaki sorgu price ve name getirir
    //var products = context.ProductWithFeatures.ToList();

    //Store Procedure
    //Direkt bir entity karşılığı olan sp çağrımı
    //var products = context.Products.FromSqlRaw("exec sp_get_products").ToList();
    //Direkt olarak bir entity karşılığı olmayan bir complex bir sp var ise buna ait model oluşturulup çağrılır.
    //var productsFull = context.ProductFulls.FromSqlRaw("exec sp_get_productsFull").ToList();

    ////dışardan değer alan sp örneği
    //int width = 250;
    //var productsFull = context.ProductFulls.FromSqlRaw($"exec sp_get_productsFull_parameters {width}").ToList();

    //Geriye hiçbirşey dönmeyen veyahut sadece insert edilen dataya ait id dönen sp

    Console.WriteLine("");
}
