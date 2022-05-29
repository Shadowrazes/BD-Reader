// DBViewerView
// Реализация логики окна просмотра таблиц

using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Interactivity;
using BD_Reader.ViewModels;
using System;

namespace BD_Reader.Views
{
    public partial class DBViewerView : UserControl
    {
        public DBViewerView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        // При нажатии на кнопку удаления удаляем нужный таб
        private void DeleteTab(object control, RoutedEventArgs args)
        {
            Button? btn = control as Button;
            if (btn != null)
            {
                DBViewerViewModel? context = this.DataContext as DBViewerViewModel;
                if (context != null)
                {
                    context.AllTables.Remove(btn.DataContext as Table);
                    GC.Collect();
                }
            }
        }

        // Если выбран другой таб, меняем имя текущего таба и помечаем, таблица запроса или таблица БД в нем
        private void SelectedTabChanged(object control, SelectionChangedEventArgs args)
        {
            TabControl? tabControl = control as TabControl;
            if(tabControl != null)
            {
                DBViewerViewModel? context = this.DataContext as DBViewerViewModel;
                Table? table = tabControl.SelectedItem as Table;
                if (context != null && table != null)
                {
                    context.CurrentTableName = table.Name;
                    context.CurrentTableIsSubtable = table.IsSubTable;
                }
            }
        }
    }
}
