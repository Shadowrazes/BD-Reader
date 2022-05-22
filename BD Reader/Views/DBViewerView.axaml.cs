using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Interactivity;
using BD_Reader.ViewModels;

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

        private void DeleteTab(object control, RoutedEventArgs args)
        {
            Button? btn = control as Button;
            if (btn != null)
            {
                DBViewerViewModel? context = this.DataContext as DBViewerViewModel;
                if (context != null)
                {
                    context.Tables.Remove(btn.DataContext as Table);
                    context.Requests.Remove(btn.DataContext as Table);
                }
            }
        }

        private void SelectedTabChanged(object control, SelectionChangedEventArgs args)
        {
            TabControl? tabControl = control as TabControl;
            if(tabControl != null)
            {
                DBViewerViewModel? context = this.DataContext as DBViewerViewModel;
                Table? table = tabControl.SelectedItem as Table;
                if (context != null && table != null)
                {
                    context.CurrentTableIsSubtable = table.IsSubTable;
                }
            }
        }
    }
}
