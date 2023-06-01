using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.ViewModels
{
    internal class UserViewModel : BaseViewModel
    {
        public string Name { get; set; }    
        public int Age { get; set; }
        public string Gender { get; set; }
        public override string ToString()
        {
            return "ID:" + ID + ",Name:" + Name + ",Age:" + Age + ",Gender:" + Gender;
        }
    }

}
