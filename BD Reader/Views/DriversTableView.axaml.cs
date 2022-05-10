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
using Microsoft.Data.Sqlite;
using System.IO;
using System;

namespace BD_Reader.Views
{
    public partial class DriversTableView : UserControl
    {
        public class Txt
        {
            public string Field { get; set; }
            public Txt(string _Field)
            {
                Field = _Field;
            }
        }
        public DriversTableView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
        private void OnEdit(object control, DataGridBeginningEditEventArgs args)
        {
            var a = control as DataGrid;
            ObservableCollection<Txt> list = new ObservableCollection<Txt>(); 

            for(int i = 0; i < 15; i++)
            {
                list.Add(new Txt("assssss"));
            }

            DataGridTextColumn b = new DataGridTextColumn();
            //StyledProperty<string> 
            //b.Bind(new TextBox(), list);
        }
        private void DeleteNullColumn(object control, DataGridAutoGeneratingColumnEventArgs args)
        {
            var a = control as DataGrid;
            var b = a.Columns;
            var c = a.Items;
            
            if (args.PropertyName == "Car" || args.PropertyName == "TeamNameNavigation")
            {
                args.Cancel = true;
            }
        }
    }
}
