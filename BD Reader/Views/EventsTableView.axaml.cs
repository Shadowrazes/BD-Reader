using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace BD_Reader.Views
{
    public partial class EventsTableView : UserControl
    {
        public EventsTableView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}