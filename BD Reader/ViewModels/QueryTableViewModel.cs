// QueryTableViewModel
// Логика таблицы запроса

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
    public class QueryTableViewModel : MainWindowViewModel
    {
        private List<List<object>> queryList;   // Список значений каждой колонки
        public QueryTableViewModel(List<Dictionary<string, object?>> _queryDict)
        {
            queryList = new List<List<object>>();

            List<string> properties = new List<string>();

            // Первый элемент в списке значений - название колонки
            foreach (var property in _queryDict[0])
            {
                properties.Add(property.Key);
            }

            // Преобразуем словарь в список значений каждой колонки
            foreach (string property in properties)
            {
                List<object> values = new List<object>();
                values.Add(property + "    ");
                values.Add(" ");
                foreach (Dictionary<string, object?> item in _queryDict)
                {
                    values.Add(item[property]);
                }
                queryList.Add(values);
            }
        }

        public List<List<object>> QueryList
        {
            get
            {
                return queryList;
            }
        }
    }
}
