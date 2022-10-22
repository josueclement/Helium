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
            UnhandledException += AppBootstrapper_UnhandledException;
        }

        private void AppBootstrapper_UnhandledException(object sender, Exception e)
        {
            MessageBox.Show(e.ToString(), "Unhandled exception", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public override bool IsSplashScreenEnabled => true;
        public override TimeSpan SplashScreenDuration => TimeSpan.FromSeconds(4);

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

            // Must be called after adding services
            base.ConfigureServices(services);
        }
    }
}
