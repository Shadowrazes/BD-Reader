using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

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
    }
}
