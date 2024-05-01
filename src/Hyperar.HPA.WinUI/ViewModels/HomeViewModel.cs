namespace Hyperar.HPA.WinUI.ViewModels
{
    using System.Threading.Tasks;
    using WinUI.State.Interface;

    public class HomeViewModel : AsyncViewModelBase
    {
        public HomeViewModel(INavigator navigator) : base(navigator)
        {
        }

        public override Task InitializeAsync()
        {
            this.Navigator.ResumeNavigation();

            return base.InitializeAsync();
        }
    }
}