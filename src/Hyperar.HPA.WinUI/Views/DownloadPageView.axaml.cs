namespace Hyperar.HPA.WinUI
{
    using System.Linq;
    using Avalonia.Controls;

    public partial class DownloadPageView : UserControl
    {
        public DownloadPageView()
        {
            this.InitializeComponent();
        }

        private void DataGrid_SelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            if (sender is DataGrid dataGrid && dataGrid.SelectedItem != null)
            {
                dataGrid.ScrollIntoView(dataGrid.SelectedItem, dataGrid.Columns.First());
            }
        }
    }
}