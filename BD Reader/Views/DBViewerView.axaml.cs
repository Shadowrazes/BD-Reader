using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

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
    }
}
