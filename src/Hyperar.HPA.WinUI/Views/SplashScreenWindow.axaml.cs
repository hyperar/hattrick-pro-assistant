namespace Hyperar.HPA.WinUI.Views
{
    using FluentAvalonia.UI.Windowing;

    public partial class SplashScreenWindow : AppWindow
    {
        public SplashScreenWindow()
        {
            this.InitializeComponent();
            this.TitleBar.ExtendsContentIntoTitleBar = true;
            this.TitleBar.TitleBarHitTestType = TitleBarHitTestType.Complex;
        }
    }
}