using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Models
{
    internal class UsersWorks : Entity
    {
        public int UserID { get; set; }
        public int WorkID { get; set; }
    }
}
