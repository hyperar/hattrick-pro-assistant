namespace Hyperar.HPA.UserInterface
{
    using System.Windows;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IConfiguration configuration;

        public MainWindow(IConfiguration configuration, object dataContext)
        {
            this.InitializeComponent();
            this.configuration = configuration;
            this.DataContext = dataContext;
            this.Title = this.configuration["OAuth:UserAgent"];
        }
    }
}
