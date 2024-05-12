namespace Hyperar.HPA.WinUI.ViewModels
{
    using System.Threading.Tasks;
    using WinUI.State.Interface;

    public class PlayersViewModel : AsyncViewModelBase
    {
        public PlayersViewModel(INavigator navigator) : base(navigator)
        {
        }

        public override async Task InitializeAsync()
        {
            this.Navigator.ResumeNavigation();

            await base.InitializeAsync();
        }
    }
}