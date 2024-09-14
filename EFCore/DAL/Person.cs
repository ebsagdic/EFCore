using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.CodeFirst.DAL
{
    //Bunu aynı şekilde Fluent Api ile de yapabiliriz.
    [Owned]
    public class Person
    {
        public  string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }    

    }
}
