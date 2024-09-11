using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.CodeFirst.DAL
{
    //Aşağıdaki Configurationslar Data Annotation Attributes olarak geçer
    //[Table("ProductTb",Schema ="products")]
    public class Product
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        ////Alan teker teker otomatik artsın istemiyosak bu şekilde 
        public int Id { get; set; }
        //[Column("Name2",Order =2)]

        //[Required]
        //[StringLength(100)]
        //[MinLength(100)]
        //Bu attributeleri kirletmek yerine third party bi şekilde fluent validation kullanmalısın
        public string Name { get; set; }
        public decimal Price { get; set; }

        public int Kdv { get; set; }
        public int Stock {  get; set; }

        //public DateTime? CreatedDate { get; set; }
        public  int  Barcode { get; set; }

        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        ////Sadece insert işlemlerinde dahil edilmesini istiyorsak bu attribute kullanılır.
        //public DateTime? CreatedDate { get; set; } = DateTime.Now;

        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        ////Hem add hemde update işlemlerini dbde yapıyorsak bu alanı dahil etmeyiz
        public decimal  pricekdv { get; set; }

        //public int? CategoryId { get; set; }
        //public Category? Category { get; set; }

        //public ProductFeature ProductFeature { get; set; }
    }
}
