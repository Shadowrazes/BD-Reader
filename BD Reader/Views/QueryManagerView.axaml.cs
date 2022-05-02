using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Input;

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
                var a = tablesList.Items;
                var b = tablesList.SelectedItems;
            }
        }
    }
}
