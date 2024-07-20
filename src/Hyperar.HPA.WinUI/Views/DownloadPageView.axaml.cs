namespace Hyperar.HPA.WinUI.Views
{
    using System.Linq;
    using Avalonia.Controls;
    using Shared.Models.UI.Download;

    public partial class DownloadPageView : UserControl
    {
        public DownloadPageView()
        {
            this.InitializeComponent();
        }

        private void DataGrid_SelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            if (sender is DataGrid dataGrid && dataGrid.SelectedItem is TaskRow currentTask)
            {
                dataGrid.ScrollIntoView(currentTask, dataGrid.Columns.First());
            }
        }
    }
}