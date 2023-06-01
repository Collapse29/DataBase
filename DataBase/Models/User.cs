using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Models
{
    internal class User : Entity
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public int Gender { get; set; }
    }   
    
}
