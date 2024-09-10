using EFCore.CodeFirst;
using EFCore.CodeFirst.DAL;
using Microsoft.EntityFrameworkCore;
using System.Text;

Initializer.Build();

using(var context = new AppDbContext())
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
    

}
