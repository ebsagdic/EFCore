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
        public int Id { get; set; }
        //[Column("Name2",Order =2)]

        //[Required]
        //[StringLength(100)]
        //[MinLength(100)]
        //Bu attributeleri kirletmek yerine third party bi şekilde fluent validation kullanmalısın
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock {  get; set; }

        //public DateTime? CreatedDate { get; set; }
        public  int  Barcode { get; set; }

    }
}
