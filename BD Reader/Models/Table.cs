using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace BD_Reader.Models
{
    public class Table<T>
    {
        private string name;
        private ObservableCollection<T> tableList;
        public Table(string _name, ObservableCollection<T> _tableList)
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

        public ObservableCollection<T> TableList
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
