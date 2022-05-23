using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Input;
using BD_Reader.ViewModels;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Avalonia.Interactivity;

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
                    context.Join.Try();
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
