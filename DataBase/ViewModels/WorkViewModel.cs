using DataBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.ViewModels
{
    internal class WorkViewModel : BaseViewModel
    {
        public string WorkName { get; set; }

        public override string ToString()
        {
            return "ID:" + ID + ",Название работы:" + WorkName;
        }
    }
}
