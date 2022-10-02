using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

namespace WpfTests
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            try
            {
                new AppBootstrapper().Run();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{Assembly.GetExecutingAssembly().GetName().Name} fatal error:\r\n\r\n {ex}");
                Environment.Exit(1);
            }
        }
    }
}
