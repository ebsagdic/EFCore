using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.CodeFirst.DAL
{
    public class ProductFeature
    {
        //ProductFeature ile Product arasında OneToOne ilişki vardır

        public int Id { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string Color { get; set; }
        //Virtual Lazy Loading için gerekli 
        public virtual Product Product { get; set; }
        public int ProductId { get; set; }


    }
}
