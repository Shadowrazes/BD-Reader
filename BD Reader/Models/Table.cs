using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using BD_Reader.ViewModels;

namespace BD_Reader.Models
{
    public class Table
    {
        private string name;
        private ViewModelBase tableList;
        public Table(string _name, ViewModelBase _tableList)
        {
            name = _name;
            tableList = _tableList;
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        public ViewModelBase TableList
        {
            get
            {
                return tableList;
            }
            set
            {
                tableList = value;
            }
        }
    }
}
