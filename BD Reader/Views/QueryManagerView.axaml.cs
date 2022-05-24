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
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
        private void AddRequest(object control, RoutedEventArgs args)
        {
            QueryManagerViewModel? context = this.DataContext as QueryManagerViewModel;
            if (context != null)
            {
                context.AddRequest(this.FindControl<TextBox>("RequestName").Text);
                this.FindControl<Button>("Accept").IsEnabled = false;
            }
        }
        private void RequestNameChanged(object control, KeyEventArgs args)
        {
            TextBox? requestName = control as TextBox;
            if(requestName != null)
            {
                QueryManagerViewModel? context = this.DataContext as QueryManagerViewModel;
                if (context != null)
                {
                    bool tableExist = false;
                    foreach (Table table in context.Tables)
                    {
                        if (table.Name == requestName.Text)
                        {
                            tableExist = true;
                            break;
                        }
                    }
                    if (requestName.Text != "" && !tableExist)
                        this.FindControl<Button>("Accept").IsEnabled = true;
                    else
                        this.FindControl<Button>("Accept").IsEnabled = false;
                }
            }
        }
        private void TableSelected(object control, SelectionChangedEventArgs args)
        {
            ListBox? tablesList = control as ListBox;
            if(tablesList != null)
            {
                QueryManagerViewModel? context = this.DataContext as QueryManagerViewModel;
                if(context != null)
                {
                    List<Table> tables = new List<Table>();
                    foreach (Table table in tablesList.SelectedItems)
                    {
                        tables.Add(table);
                    }
                    context.SelectedTables = tables;
                    context.Join();
                }
            }
        }
        public void AddFilterOR(object control, RoutedEventArgs args)
        {
            QueryManagerViewModel? context = this.DataContext as QueryManagerViewModel;
            Button? button = control as Button;
            if (context != null && button != null)
            {
                string? type = button.CommandParameter as string;
                if (type == "Default")
                {
                    context.Filters.Add(new Filter("OR", context.ColumnList));
                    this.FindControl<Button>("FilterAND").IsEnabled = false;
                }
                else
                {
                    context.GroupFilters.Add(new Filter("OR", context.ColumnList));
                    this.FindControl<Button>("GroupFilterAND").IsEnabled = false;
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
                    context.Filters.Add(new Filter("AND", context.ColumnList));
                    this.FindControl<Button>("FilterOR").IsEnabled = false;
                }
                else
                {
                    context.GroupFilters.Add(new Filter("AND", context.ColumnList));
                    this.FindControl<Button>("GroupFilterOR").IsEnabled = false;
                }
            }
        }
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
                    this.FindControl<Button>("FilterOR").IsEnabled = true;
                    this.FindControl<Button>("FilterAND").IsEnabled = true;
                }  
                else if (context.GroupFilters.Count == 1 && type == "Group")
                {
                    this.FindControl<Button>("GroupFilterOR").IsEnabled = true;
                    this.FindControl<Button>("GroupFilterAND").IsEnabled = true;
                }  
            }
        }
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
    }
}
