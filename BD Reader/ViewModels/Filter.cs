// Filter
// Одно из условия фильтрации

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using ReactiveUI;
using System.Reactive;
using BD_Reader.Models;
using Microsoft.Data.Sqlite;
using System.IO;
using System;

namespace BD_Reader.ViewModels
{
    public class Filter
    {
        public Filter(string _BoolOper, ObservableCollection<string> _Columns)
        {
            BoolOper = _BoolOper;
            Columns = _Columns;
            Operator = "";
            Column = "";
            FilterVal = "";
            Operators = new ObservableCollection<string> {
                    ">", ">=", "=", "<>", "<", "<=",
                    "In Range", "Not In Range", "Contains", "Not Contains",
                    "Is Null", "Not Null", "Belong", "Not Belong"
                };
        }
        public string? BoolOper { get; set; }                       // Указывает в какой цепочке операторов находится - AND или OR
        public string FilterVal { get; set; }                       // Значение, которое участвует в обработке условия
        public string? Operator { get; set; }                       // Оператор условия
        public string? Column { get; set; }                         // Имя поля, из которого берется значение
        public ObservableCollection<string> Columns { get; set; }   // Список доступных полей
        public ObservableCollection<string> Operators { get; set; } // Список доступных операторов
    }
}
