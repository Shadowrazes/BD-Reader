// QueryManagerViewModel
// Реализация логики менеджера запросов

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
    public class QueryManagerViewModel : ViewModelBase
    {                            
        private ObservableCollection<Table> m_tables;           // Ссылка на таблицы БД   
        private ObservableCollection<Table> m_allTables;        // Ссылка на таблицы БД + таблицы запросов
        private ObservableCollection<Table> m_requests;         // Список таблиц запросов
        private ObservableCollection<string> m_columnList;      // Список имен колонок, которые можно использовать для запроса
        private ObservableCollection<Filter> m_filters;         // Список фильтров
        private ObservableCollection<Filter> m_groupFilters;    // Список фильтров для группировки
        private MainWindowViewModel m_mainWindow;               // Ссылка на главное окно приложения
        internal Dictionary<string, string> Keys = new Dictionary<string, string>()
        {
            { "CarId", "CarId"},
            { "DriverFullName", "DriverFullName"},
            { "EventName", "EventName"},
            { "TeamName", "TeamName"}
        };  // Сопоставление ключей связанных таблиц

        public QueryManagerViewModel(DBViewerViewModel _DBViewer, MainWindowViewModel _mainWindow)
        {
            DBViewer = _DBViewer;
            m_mainWindow = _mainWindow;
            m_tables = DBViewer.Tables;
            m_allTables = DBViewer.AllTables;
            m_requests = new ObservableCollection<Table>();
            m_filters = new ObservableCollection<Filter>();
            m_groupFilters = new ObservableCollection<Filter>();
            m_columnList = new ObservableCollection<string>();

            SelectedTables = new ObservableCollection<Table>();
            SelectedColumns = new ObservableCollection<string>();

            ResultTable = new List<Dictionary<string, object?>>();
            JoinedTable = new List<Dictionary<string, object?>>();
            SelectedColumnsTable = new List<Dictionary<string, object?>>();

            FilterChain = new FilterHandler(this, "Filters");
            GroupChain = new GroupHandler(this);
            GroupFilterChain = new FilterHandler(this, "GroupFilters");

            FilterChain.NextHope = GroupChain;
            //GroupChain.NextHope = GroupFilterChain;

            IsRequestSuccess = false;
        }

        // Обновляем список доступных колонок для запроса и очищаем фильтры
        public void UpdateColumnList()
        {
            ColumnList = new ObservableCollection<string>();
            if (JoinedTable.Count != 0)
            {
                foreach (var column in JoinedTable[0])
                {
                    ColumnList.Add(column.Key);
                }
            }
            Filters.Clear();
            GroupFilters.Clear();
        }

        // Вызываем цепочку фильтр-группировка-фильтр
        public void AddRequest(string tableName)
        {
            FilterChain.Try();

            // Если запрос успешный добавляем результирующую таблицу в общий список и список запросов
            if (IsRequestSuccess)
            {
                Requests.Add(new Table(tableName, true, new QueryTableViewModel(ResultTable.ToList()), new ObservableCollection<string>()));
                AllTables.Add(Requests.Last());
            }

            // Очищаем всё
            ClearAll();

            // Открываем окно просмотра таблиц
            m_mainWindow.OpenDBViewer();
        }

        // Находим удаляемую таблицу и удаляем ее из списка запросов
        public void DeleteRequests()
        {
            Requests = new ObservableCollection<Table>(Requests.Where(table => AllTables.Any(tables => tables.Name == table.Name)));
            GC.Collect();
        }

        // Очистка всех рабочих списков
        public void ClearAll()
        {
            ResultTable.Clear();
            JoinedTable.Clear();
            SelectedColumnsTable.Clear();
            SelectedTables.Clear();
            SelectedColumns.Clear();
            Filters.Clear();
            GroupFilters.Clear();
            ColumnList.Clear();
        }

        // Попытка соединить 2 таблицы по переданным ключам
        private bool TryJoin(string key1, List<Dictionary<string, object?>> table2, string key2)
        {
            try
            {
                JoinedTable = JoinedTable.Join(
                    table2,
                    firstItem => firstItem[key1],
                    secondItem => secondItem[key2],
                    (firstItem, secondItem) =>
                    {
                        Dictionary<string, object?> resultItem = new Dictionary<string, object?>();
                        foreach (var item in firstItem)
                        {
                            resultItem.TryAdd(item.Key, item.Value);
                        }
                        foreach (var item in secondItem)
                        {
                            if (item.Key != key2)
                                resultItem.TryAdd(item.Key, item.Value);
                        }
                        return resultItem;
                    }
                    ).ToList();
            }
            catch
            {
                return false;
            }
            return true;
        }

        // Соединение таблиц
        public void Join()
        {
            // Если выбрана хотя бы 1 таблица
            if (SelectedTables.Count > 0)
            {
                // Если есть таблица событий, то перемещаем ее в конец, списка соединения, т.к только она на самом краю зависимостей
                var check = SelectedTables.Where(tab => tab.Name == "Events");
                if (check.Count() != 0)
                {
                    Table tmp = check.Last();
                    SelectedTables.Remove(check.Last());
                    SelectedTables.Add(tmp);
                }
                // Результат = первая выбранная таблица 
                JoinedTable = new List<Dictionary<string, object?>>(SelectedTables[0].Rows);

                // Если выбрано больше 1 таблицы
                if (SelectedTables.Count > 1)
                {
                    // Создаем ссылку для таблица, которая будет соединятся с результатом
                    List<Dictionary<string, object?>> joiningTable;

                    // Создаем сигнал об успешном соединении
                    bool success = false;
                    for (int i = 1; i < SelectedTables.Count; i++)
                    {
                        joiningTable = SelectedTables[i].Rows;
                        
                        // Перебираем каждую пару ключей
                        foreach (var keysPair in Keys)
                        {
                            // И пытаемся соединить передав ключи возможной связи key1 - key2
                            success = TryJoin(keysPair.Key, joiningTable, keysPair.Value);
                            if (success)
                                break;

                            // Если не получилось, пытаемся передать key2 - key1
                            else
                            {
                                success = TryJoin(keysPair.Value, joiningTable, keysPair.Key);
                                if (success)
                                    break;
                            }
                        }
                        // Если никак не получилось соединить, то очищаем результирующий массив и обновляем список доступных колонок
                        if (!success)
                        {
                            JoinedTable.Clear();
                            ResultTable = JoinedTable;
                            UpdateColumnList();
                            return;
                        }
                    }
                }
                // Обновляем список доступных колонок и результирующий массив
                UpdateColumnList();
                ResultTable = JoinedTable;
            }

            // Если не выбрана ни одна таблица, то очищаем результирующий массив и обновляем список доступных колонок
            else
            {
                JoinedTable.Clear();
                ResultTable = JoinedTable;
                ColumnList.Clear();
            }
        }

        // Заносим в результирующий массив элементы только с выбранными колонками
        public void Select()
        {
            SelectedColumnsTable = JoinedTable.Select(item =>
            {
                return new Dictionary<string, object?>(item.Where(property => SelectedColumns.Any(column => column == property.Key)));
            }).ToList();
            ResultTable = SelectedColumnsTable;
        }

        public bool IsRequestSuccess { get; set; }                                  // Успешен ли запрос
        public List<Dictionary<string, object?>> ResultTable { get; set; }          // Результирующий массив
        public List<Dictionary<string, object?>> JoinedTable { get; set; }          // Массив для соединения таблиц
        public List<Dictionary<string, object?>> SelectedColumnsTable { get; set; } // Массив элементов с выбранными колонками
        public ObservableCollection<string> ColumnList
        {
            get => m_columnList;
            set
            {
                this.RaiseAndSetIfChanged(ref m_columnList, value);
            }
        }
        public ObservableCollection<string> SelectedColumns { get; set; }           // Имена выбранных колонок
        public ObservableCollection<Filter> Filters
        {
            get => m_filters;
            set
            {
                this.RaiseAndSetIfChanged(ref m_filters, value);
            }
        }
        public ObservableCollection<Filter> GroupFilters
        {
            get => m_groupFilters;
            set
            {
                this.RaiseAndSetIfChanged(ref m_groupFilters, value);
            }
        }
        public ObservableCollection<Table> Tables
        {
            get => m_tables;
            set
            {
                this.RaiseAndSetIfChanged(ref m_tables, value);
            }
        }
        public ObservableCollection<Table> SelectedTables { get; set; }             // Список выбранных таблиц
        public ObservableCollection<Table> AllTables
        {
            get => m_allTables;
            set
            {
                this.RaiseAndSetIfChanged(ref m_allTables, value);
            }
        }
        public ObservableCollection<Table> Requests
        {
            get => m_requests;
            set
            {
                this.RaiseAndSetIfChanged(ref m_requests, value);
            }
        }
        public FilterHandler FilterChain { get; set; }                              // Звено-обработчик фильтров
        public GroupHandler GroupChain { get; set; }                                // Звено-обработчик группировки
        public FilterHandler GroupFilterChain { get; set; }                         // Звено-обработчик фильтров группировки
        public DBViewerViewModel DBViewer { get; }                                  // Ссылка на окно просмотра таблиц       
    }
}
