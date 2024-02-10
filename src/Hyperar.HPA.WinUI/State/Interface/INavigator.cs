namespace Hyperar.HPA.WinUI.State.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface INavigator
    {
        event Action? StateChanged;

        bool CanNavigate { get; }

        void ResumeNavigation();

        void SuspendNavigation();
    }
}