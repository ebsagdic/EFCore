using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.CodeFirst.DAL
{
    public class Student
    {
        //Student ve Teacher classları ManyToMany ilişkisini göstermek için oluşturulmuştur

        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public List<Teacher> Teachers  { get; set; }

    }
}
