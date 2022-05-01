using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace BD_Reader.Views
{
    public partial class QueryTableView : UserControl
    {
        public QueryTableView()
        {
            InitializeComponent();
            //this.FindControl<DataGrid>("Row").row
            //this.FindControl<Grid>("row")
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
