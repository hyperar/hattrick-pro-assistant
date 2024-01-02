namespace Hyperar.HPA.UI
{
    using System.Windows;
    using Microsoft.Extensions.Configuration;

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
    }
}