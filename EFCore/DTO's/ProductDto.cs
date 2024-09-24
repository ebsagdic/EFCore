using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.CodeFirst.DTO_s
{
    public class ProductDto
    {
        public string CategoryName { get; set; }
        public string ProductName { get; set; }
        public decimal  ProductPrice { get; set; }
        public int? Width { get; set; }

    }
}
