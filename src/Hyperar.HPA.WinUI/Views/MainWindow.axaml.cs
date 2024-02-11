namespace Hyperar.HPA.WinUI.Views
{
    using FluentAvalonia.UI.Windowing;

    public partial class MainWindow : AppWindow
    {
        public MainWindow()
        {
            this.InitializeComponent();

            this.TitleBar.ExtendsContentIntoTitleBar = true;
            this.TitleBar.TitleBarHitTestType = TitleBarHitTestType.Complex;
        }
    }
}