// Table
// Класс, содержащий информацию о таблице

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
    public class Table
    {
        private string name;                // Название таблицы
        private ViewModelBase tableView;    // Окно таблицы
        public Table(string _name, bool _IsSubTable, ViewModelBase _tableView, ObservableCollection<string> _Properties)
        {
            name = _name;
            IsSubTable = _IsSubTable;
            tableView = _tableView;
            Properties = _Properties;
            Rows = new List<Dictionary<string, object?>>();

            // Получаем список элементов таблицы
            dynamic table = TableView.GetTable();

            // Конвертируем его в список словарей, словарь = объект класса Driver, Car и т.д.
            if (table != null)
            {
                Key = table[0].Key();
                for (int j = 0; j < table.Count; j++)
                {
                    Dictionary<string, object?> tmp = new Dictionary<string, object?>();
                    foreach (string prop in Properties)
                    {
                        tmp.Add(prop, table[j][prop]);
                    }
                    Rows.Add(tmp);
                }
            }

            // Иначе, если таблицы запросная, получаем список словарей из ее окна
            else if (IsSubTable)
            {
                Rows = TableView.GetRows();
            }
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

        public string Key { get; set; }                                 // Первичный ключ таблицы

        public bool IsSubTable { get; }                                 // Является ли таблица результатом запроса

        public ViewModelBase TableView
        {
            get
            {
                return tableView;
            }
            set
            {
                tableView = value;
            }
        }

        public List<Dictionary<string, object?>> Rows { get; }          // Строки таблицы, конвертированные в список словарей

        public ObservableCollection<string> Properties { get; set; }    // Список названий колонок таблицы

        // Получение списка удаляемых элементов
        public List<object>? GetRemovableItems()
        {
            return TableView.RemovableItems;
        }

        // Проброс сигнала о том, что идет удаление строк в окно таблицы
        public void SetRemoveInProgress(bool value)
        {
            TableView.RemoveInProgress = value;
        }
    }
}
