// QueryManagerView
// Реализации логики окна менеджера запросов

using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Input;
using BD_Reader.ViewModels;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Avalonia.Interactivity;
using System.Linq;

namespace BD_Reader.Views
{
    public partial class QueryManagerView : UserControl
    {
        public QueryManagerView()
        {
            InitializeComponent();
            FilterAND = this.FindControl<Button>("FilterAND");
            FilterOR = this.FindControl<Button>("FilterOR");
            FilterPop = this.FindControl<Button>("FilterPop");
            GroupFilterAND = this.FindControl<Button>("GroupFilterAND");
            GroupFilterOR = this.FindControl<Button>("GroupFilterOR");
            GroupFilterPop = this.FindControl<Button>("GroupFilterPop");
            RequestName = this.FindControl<TextBox>("RequestName");
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        // Проверяем, есть ли уже таблица с таким именем и блокируем кнопку отправки запроса, если такая существует
        private void IsTableExist(QueryManagerViewModel context)
        {
            bool tableExist = false;
            foreach (Table table in context.AllTables)
            {
                if (table.Name == RequestName.Text)
                {
                    tableExist = true;
                    break;
                }
            }
            if (RequestName.Text != "" && RequestName.Text != null && !tableExist)
                this.FindControl<Button>("Accept").IsEnabled = true;
            else
                this.FindControl<Button>("Accept").IsEnabled = false;
        }

        // Попытка выполнить запрос
        private void AddRequest(object control, RoutedEventArgs args)
        {
            QueryManagerViewModel? context = this.DataContext as QueryManagerViewModel;
            if (context != null)
            {
                context.AddRequest(this.FindControl<TextBox>("RequestName").Text);
                this.FindControl<Button>("Accept").IsEnabled = false;
            }
        }

        // При вводе имени запроса включаем или отключаем кнопку отправки запроса
        // в зависимости от того, есть ли уже запрос с таким именем
        private void RequestNameChanged(object control, KeyEventArgs args)
        {
            TextBox? requestName = control as TextBox;
            if(requestName != null)
            {
                QueryManagerViewModel? context = this.DataContext as QueryManagerViewModel;
                if (context != null)
                {
                    if (context.ResultTable.Count == 0)
                    {
                        this.FindControl<Button>("Accept").IsEnabled = false;
                    }
                    else
                    {
                        IsTableExist(context);
                    }
                }
            }
        }

        // При выборе таблиц вызываем их соединение,
        // если соединение неуспешно - отключаем кнопку отправки запроса
        // Если выбирана запросная таблица, блокируем выбор таблиц БД
        private void TableSelected(object control, SelectionChangedEventArgs args)
        {
            ListBox? tablesList = control as ListBox;
            if(tablesList != null)
            {
                QueryManagerViewModel? context = this.DataContext as QueryManagerViewModel;
                if(context != null)
                {
                    context.SelectedTables = new ObservableCollection<Table>();
                    foreach (Table table in tablesList.SelectedItems)
                    {
                        if (!table.IsSubTable)
                        {
                            context.SelectedTables.Add(table);
                            context.IsBDTableSelected = true;
                        }
                        else
                        {
                            context.ClearAll();
                            foreach (Table subTable in tablesList.SelectedItems)
                            {
                                if (subTable.IsSubTable)
                                {
                                    context.SelectedTables.Add(subTable);
                                }
                            }
                            context.IsBDTableSelected = false;
                            break;
                        }
                    }
                    context.Join();

                    if (context.ResultTable.Count == 0)
                    {
                        this.FindControl<Button>("Accept").IsEnabled = false;
                    }
                    else
                    {
                        IsTableExist(context);
                    }
                }
            }
        }

        // При выборе колонок из списка вызываем выборку и очищаем фильтры,
        // если список выбранных колонок пуст то еще и отключаем их,
        // также, если результат выборки пуст - блокируем кнопку отправки запроса
        private void ColumnSelected(object control, SelectionChangedEventArgs args)
        {
            ListBox? tablesList = control as ListBox;
            if (tablesList != null)
            {
                QueryManagerViewModel? context = this.DataContext as QueryManagerViewModel;
                if (context != null)
                {
                    context.SelectedColumns.Clear();
                    context.Filters.Clear();
                    context.GroupFilters.Clear();
                    context.Filters.Add(new Filter("", context.SelectedColumns));
                    context.GroupFilters.Add(new Filter("", context.SelectedColumns));
                    foreach (string column in tablesList.SelectedItems)
                    {
                        context.SelectedColumns.Add(column);
                    }
                    context.Select();

                    if (context.ResultTable.Count == 0)
                    {
                        this.FindControl<Button>("Accept").IsEnabled = false;
                    }
                    else
                    {
                        IsTableExist(context);
                    }

                    if (context.SelectedColumns.Count != 0)
                    {
                        FilterAND.IsEnabled = true;
                        FilterOR.IsEnabled = true;
                        FilterPop.IsEnabled = true;
                        GroupFilterAND.IsEnabled = true;
                        GroupFilterOR.IsEnabled = true;
                        GroupFilterPop.IsEnabled = true;
                    }
                    else
                    {
                        FilterAND.IsEnabled = false;
                        FilterOR.IsEnabled = false;
                        FilterPop.IsEnabled = false;
                        GroupFilterAND.IsEnabled = false;
                        GroupFilterOR.IsEnabled = false;
                        GroupFilterPop.IsEnabled = false;
                    }
                }
            }
        }

        // Меняем колонку по которой будет группировка при выборе из списка
        private void GroupingColumnSelected(object control, SelectionChangedEventArgs args)
        {
            ListBox? columnList = control as ListBox;
            if (columnList != null)
            {
                QueryManagerViewModel? context = this.DataContext as QueryManagerViewModel;
                if (context != null)
                {
                    context.GroupingColumn = columnList.SelectedItem as string;
                }
            }
        }

        // Добавляем строку фильтрации с булевым оператором OR или AND в нужную таблицу фильтров
        // - фильтры или фильтры группировки, в зависимости от того, в каком Expander'е нажимаются кнопки
        // Если выбрана цепочка OR, то кнопка с добавлением AND блокируется и наоборот
        public void AddFilterOR(object control, RoutedEventArgs args)
        {
            QueryManagerViewModel? context = this.DataContext as QueryManagerViewModel;
            Button? button = control as Button;
            if (context != null && button != null)
            {
                string? type = button.CommandParameter as string;
                if (type == "Default")
                {
                    context.Filters.Add(new Filter("OR", context.SelectedColumns));
                    FilterAND.IsEnabled = false;
                    FilterPop.IsEnabled = true;
                }
                else
                {
                    context.GroupFilters.Add(new Filter("OR", context.SelectedColumns));
                    GroupFilterAND.IsEnabled = false;
                    GroupFilterPop.IsEnabled = true;
                }
            }
        }
        public void AddFilterAND(object control, RoutedEventArgs args)
        {
            QueryManagerViewModel? context = this.DataContext as QueryManagerViewModel;
            Button? button = control as Button;
            if (context != null && button != null)
            {
                string? type = button.CommandParameter as string;
                if (type == "Default")
                {
                    context.Filters.Add(new Filter("AND", context.SelectedColumns));
                    FilterOR.IsEnabled = false;
                    FilterPop.IsEnabled = true;
                }
                else
                {
                    context.GroupFilters.Add(new Filter("AND", context.SelectedColumns));
                    GroupFilterOR.IsEnabled = false;
                    GroupFilterPop.IsEnabled = true;
                }
            }
        }
        //  //

        // Удаляем последнюю строчку в нужной таблице фильтров, а также
        // Блокируем кнопку удаления и активируем кнопки AND и OR, если в таблице осталась одна строка фильтра
        public void PopBackFilter(object control, RoutedEventArgs args)
        {
            QueryManagerViewModel? context = this.DataContext as QueryManagerViewModel;
            Button? button = control as Button;
            if (context != null && button != null)
            {
                string? type = button.CommandParameter as string;
                if (context.Filters.Count > 1 && type == "Default")
                    context.Filters.Remove(context.Filters.Last());
                else if (context.GroupFilters.Count > 1 && type == "Group")
                    context.GroupFilters.Remove(context.GroupFilters.Last());

                if (context.Filters.Count == 1 && type == "Default")
                {
                    FilterOR.IsEnabled = true;
                    FilterAND.IsEnabled = true;
                    FilterPop.IsEnabled = false;
                }  
                else if (context.GroupFilters.Count == 1 && type == "Group")
                {
                    GroupFilterOR.IsEnabled = true;
                    GroupFilterAND.IsEnabled = true;
                    GroupFilterPop.IsEnabled = false;
                }  
            }
        }

        // Очищаем все рабочие массивы и возвращаемся на окно просмотра таблиц
        private void BackToViewer(object control, RoutedEventArgs args)
        {
            QueryManagerViewModel? context = this.DataContext as QueryManagerViewModel;
            MainWindowViewModel? parentContext = this.Parent.DataContext as MainWindowViewModel;
            if (context != null && parentContext != null)
            {
                context.ClearAll();
                parentContext.OpenDBViewer();
            }
        }

        // Меняем оператор условия и название поля для фильтрации для конкретного объекта Filter
        private void ComboBoxSelectChanged(object control, SelectionChangedEventArgs args)
        {
            ComboBox? comboBox = control as ComboBox;
            if (comboBox != null)
            {
                Filter? filterContext = comboBox.DataContext as Filter;
                if (filterContext != null && comboBox.Name != null)
                {
                    if (comboBox.Name.Contains("Columns"))
                    {
                        filterContext.Column = comboBox.SelectedItem as string;
                    }
                    else if (comboBox.Name.Contains("Operators"))
                    {
                        filterContext.Operator = comboBox.SelectedItem as string;
                    }
                }
            }
        }

        // Так же проверяем возможность отправки запроса при редактировании значения фильтрации
        // и блокируем кнопку отправки, если запрос некорректный
        private void FilterValueChanged(object control, KeyEventArgs args)
        {
            TextBox? filterValue = control as TextBox;
            if (filterValue != null)
            {
                QueryManagerViewModel? context = this.DataContext as QueryManagerViewModel;
                if (context != null)
                {
                    if (context.ResultTable.Count == 0)
                    {
                        this.FindControl<Button>("Accept").IsEnabled = false;
                    }
                    else
                    {
                        IsTableExist(context);
                    }
                }
            }
        }
    }
}
