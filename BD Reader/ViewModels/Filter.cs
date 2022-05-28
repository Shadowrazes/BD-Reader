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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BD_Reader.ViewModels
{
    public class Filter : INotifyPropertyChanged
    {
        private string? operatoor;          // Оператор условия
        private string example;             // Пример формата вводимого значения 
        private bool isValueInputSupported; // Поддерживается ли ввод значения фильтрации для выбранного оператора
        public Filter(string _BoolOper, ObservableCollection<string> _Columns)
        {
            BoolOper = _BoolOper;
            Columns = _Columns;
            Operator = "";
            Column = "";
            FilterVal = "";
            Example = "";
            IsValueInputSupported = true;
            Operators = new ObservableCollection<string> {
                    ">", ">=", "=", "<>", "<", "<=",
                    "In Range", "Not In Range", "Contains", "Not Contains",
                    "Is Null", "Not Null", "Belong", "Not Belong"
                };
        }
        public string? Operator 
        {
            get => operatoor;
            set
            {
                operatoor = value;

                switch (operatoor)
                {
                    case ">":
                        Example = "Number";
                        IsValueInputSupported = true;
                        break;
                    case ">=":
                        Example = "Number";
                        IsValueInputSupported = true;
                        break;
                    case "=":
                        Example = "Number || String";
                        IsValueInputSupported = true;
                        break;
                    case "<>":
                        Example = "Number || String";
                        IsValueInputSupported = true;
                        break;
                    case "<":
                        Example = "Number";
                        IsValueInputSupported = true;
                        break;
                    case "<=":
                        Example = "Number";
                        IsValueInputSupported = true;
                        break;
                    case "In Range":
                        Example = "10..40";
                        IsValueInputSupported = true;
                        break;
                    case "Not In Range":
                        Example = "10..40";
                        IsValueInputSupported = true;
                        break;
                    case "Contains":
                        Example = "Substring";
                        IsValueInputSupported = true;
                        break;
                    case "Not Contains":
                        Example = "Substring";
                        IsValueInputSupported = true;
                        break;
                    case "Is Null":
                        Example = "";
                        IsValueInputSupported = false;
                        break;
                    case "Not Null":
                        Example = "";
                        IsValueInputSupported = false;
                        break;
                    case "Belong":
                        Example = "1, 2 || str1, str2";
                        IsValueInputSupported = true;
                        break;
                    case "Not Belong":
                        Example = "1, 2 || str1, str2";
                        IsValueInputSupported = true;
                        break;
                }
            } 
        }
        public string Example 
        {
            get => example;
            set
            {
                example = value;
                NotifyPropertyChanged();
            }
        }                                 
        public bool IsValueInputSupported 
        { 
            get => isValueInputSupported;
            set
            {
                isValueInputSupported = value;
                NotifyPropertyChanged();
            } 
        }
        public string? BoolOper { get; set; }                       // Указывает в какой цепочке операторов находится - AND или OR
        public string FilterVal { get; set; }                       // Значение, которое участвует в обработке условия
        public string? Column { get; set; }                         // Имя поля, из которого берется значение
        public ObservableCollection<string> Columns { get; set; }   // Список доступных полей
        public ObservableCollection<string> Operators { get; set; } // Список доступных операторов
        public event PropertyChangedEventHandler PropertyChanged;   // Событие изменения поля

        // Уведомляем контрол об изменении его бинда
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
