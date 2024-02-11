namespace Hyperar.HPA.UI
{
    using System.ComponentModel;
    using System.Windows;
    using Microsoft.Extensions.Configuration;
    using UI.ViewModels;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string appTitleKeyName = "App:Title";

        private const string appVersionKeyName = "App:Version";

        private readonly IConfiguration configuration;

        public MainWindow(IConfiguration configuration, object dataContext)
        {
            this.InitializeComponent();
            this.configuration = configuration;
            this.DataContext = dataContext;
            this.Title = $"{this.configuration[appTitleKeyName]} v{this.configuration[appVersionKeyName]}";
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (this.DataContext is MainViewModel mainViewModel)
            {
                e.Cancel = !mainViewModel.CanNavigate;
            }

            base.OnClosing(e);
        }
    }
}