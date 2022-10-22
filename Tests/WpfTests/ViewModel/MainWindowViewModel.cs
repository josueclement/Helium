using System;
using System.Collections.Generic;
using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Xml.Serialization;
using Helium.Core;
using Helium.Core.Bootstrapper;
using Helium.Wpf.Bootstrapper;
using Helium.Wpf.Commands;
using Microsoft.Extensions.DependencyInjection;
using WpfTests.DependencyInjection;

namespace WpfTests.ViewModel
{
    public class MainWindowViewModel : ObservableObject
    {
        private AppBootstrapper _bootstrapper;

        public MainWindowViewModel()
        {
            TestSingletonCommand = new WpfCommand(ReturnTrue, TestSingleton);
            TestScopedCommand = new WpfCommand(ReturnTrue, TestScoped);
            TestTransiantCommand = new WpfCommand(ReturnTrue, TestTransiant);

            if (Bootstrapper.Current is AppBootstrapper abs)
                _bootstrapper = abs;
        }

        #region Properties

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }
        private string _title;

        #endregion

        #region Commands

        public WpfCommand TestSingletonCommand { get; private set; }
        public WpfCommand TestScopedCommand { get; private set; }
        public WpfCommand TestTransiantCommand { get; private set; }

        #endregion

        #region Methods

        private bool ReturnTrue(object param) => true;

        private void TestSingleton(object param)
        {
            IHelloService helloService = _bootstrapper.ServiceProvider.GetService<IHelloService>();
            string result = helloService.SayHello();
        }

        private void TestScoped(object param)
        {
            ISettingsService settingsService = _bootstrapper.ServiceProvider.GetService<ISettingsService>();
            settingsService.ReadSettings(@"C:\Temp\settings.txt");
        }

        private void TestTransiant(object param)
        {
            throw new Exception("Test exception");
        }

        #endregion
    }
}
