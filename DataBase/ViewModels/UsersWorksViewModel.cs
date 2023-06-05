using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.ViewModels
{
    internal class UsersWorksViewModel : BaseViewModel
    {
        public int UserID { get; set; }
        public int WorkID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string NameWork { get; set; }
        public override string ToString()
        {
            return "ID:" + ID + ",Юзер ID:" + UserID + ",Работа ID:" + WorkID + ",Имя:" + Name + ",Лет:" + Age + ",Пол:" + Gender + ",Название работы:" + NameWork;
        }


    }
}
