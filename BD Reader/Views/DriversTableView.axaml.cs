using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using ReactiveUI;
using System.Reactive;
using BD_Reader.Models;
using BD_Reader.ViewModels;
using Microsoft.Data.Sqlite;
using System.IO;
using System;

namespace BD_Reader.Views
{
    public partial class DriversTableView : UserControl
    {
        public DriversTableView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
        private void DeleteNullColumn(object control, DataGridAutoGeneratingColumnEventArgs args)
        {   
            if (args.PropertyName == "Car" || args.PropertyName == "Results" || args.PropertyName == "TeamNameNavigation" || args.PropertyName == "Item")
            {
                args.Cancel = true;
            }
        }

        private void RowSelected(object control, SelectionChangedEventArgs args)
        {
            DataGrid? grid = control as DataGrid;
            ViewModelBase? context = this.DataContext as ViewModelBase;
            if (grid != null && context != null)
            {
                if (context.RemoveInProgress)
                    return;
                context.RemovableItems.Clear();
                foreach (object item in grid.SelectedItems)
                {
                    context.RemovableItems.Add(item);
                }
            }
        }
    }
}
