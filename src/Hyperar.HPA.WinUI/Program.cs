﻿namespace Hyperar.HPA.WinUI
{
    using System;
    using Avalonia;

    class Program
    {
        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
        {
            return AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .WithInterFont()
                .LogToTrace();
        }

        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        [STAThread]
        public static void Main(string[] args)
        {
            var appBuilder = BuildAvaloniaApp();

            appBuilder.StartWithClassicDesktopLifetime(args);
        }
    }
}