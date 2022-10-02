using System;
using System.Windows;
using Helium.Wpf.Bootstrapper;
using Microsoft.Extensions.DependencyInjection;
using WpfTests.DependencyInjection;
using WpfTests.ViewModel;

namespace WpfTests
{
    public class AppBootstrapper : WpfBootstrapper
    {
        public AppBootstrapper()
        {
            IsSplashScreenEnabled = true;
        }

        protected override Window CreateMainWindow()
        {
            return new MainWindow
            {
                DataContext = new MainWindowViewModel { Title = "My main window" }
            };
        }

        protected override Window CreateSplashScreenWindow()
        {
            return new SplashScreen();
        }

        protected override void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ISettingsService, SettingsService>();
            services.AddTransient<IDateTimeService, DateTimeService>();
            services.AddTransient<IHelloService, HelloService>();

            base.ConfigureServices(services);
        }

        protected override void OnUnhandledException(Exception ex)
        {
            base.OnUnhandledException(ex);
        }

        protected override void OnStarted()
        {
            //ServiceProvider.GetService<ISettingsService>().ReadSettings(@"C:\Temp\settings.txt");

        }
    }
}
