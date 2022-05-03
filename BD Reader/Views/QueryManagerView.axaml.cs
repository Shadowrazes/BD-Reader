using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Input;
using BD_Reader.ViewModels;
using System.Collections.ObjectModel;
using System.Collections.Generic;

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

        public void RequestNameChanged(object control, KeyEventArgs args)
        {
            TextBox? requestname = control as TextBox;
            if(requestname != null)
            {
                if(requestname.Text != "")
                    this.FindControl<Button>("Accept").IsEnabled = true;
                else
                    this.FindControl<Button>("Accept").IsEnabled = false;
            }
        }
        public void TableSelected(object control, SelectionChangedEventArgs args)
        {
            ListBox? tablesList = control as ListBox;
            if(tablesList != null)
            {
                var context = this.DataContext as QueryManagerViewModel;
                if(context != null)
                {
                    List<Table> tables = new List<Table>();
                    foreach (var table in tablesList.SelectedItems)
                    {
                        tables.Add(table as Table);
                    }
                    context.UpdateColumnList(tables);
                }
            }
        }
    }
}
