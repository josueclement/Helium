using System;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace Helium.Wpf.Bootstrapper
{
    /// <summary>
    /// Bootstrapper for Wpf applications
    /// </summary>
    public class WpfBootstrapper : Core.Bootstrapper.Bootstrapper
    {
        #region Properties

        /// <summary>
        /// Gets or sets wether the splash screen is enabled
        /// </summary>
        public bool IsSplashScreenEnabled { get; set; }

        /// <summary>
        /// Gets or sets the splash screen duration
        /// </summary>
        public TimeSpan SplashScreenDuration { get; set; } = TimeSpan.FromSeconds(2);

        /// <summary>
        /// Splash screen window
        /// </summary>
        public Window? SplashScreenWindow { get; protected set; }

        /// <summary>
        /// Main window
        /// </summary>
        public Window? MainWindow { get; protected set; }

        #endregion

        #region Public methods

        /// <inheritdoc/>
        public override void Run()
        {
            OnStarting();
            ConfigureServices();
            RegisterUnhandledExceptions();
            ShowSplashScreenAndWindow();
            OnStarted();
        }

        /// <inheritdoc/>
        public override void Shutdown()
        {
            base.Shutdown();

            Application.Current.Shutdown();
        }

        #endregion

        #region Unhandled exceptions methods

        /// <inheritdoc/>
        protected override void RegisterUnhandledExceptions()
        {
            base.RegisterUnhandledExceptions();

            Application.Current.DispatcherUnhandledException += OnDispatcherUnhandledException;
        }

        /// <inheritdoc/>
        protected override void UnregisterUnhandledExceptions()
        {
            base.UnregisterUnhandledExceptions();

            Application.Current.DispatcherUnhandledException -= OnDispatcherUnhandledException;
        }

        private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;
            OnUnhandledException(e.Exception);
        }

        protected override void OnUnhandledException(Exception ex)
        {
            base.OnUnhandledException(ex);

            MessageBox.Show(ex.ToString(), "Unhandled error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        #endregion

        #region Splash screen and Main window

        /// <summary>
        /// Create a new splash screen window<br />
        /// Override this method to create your own splash screen window
        /// </summary>
        /// <returns>Splash screen window</returns>
        protected virtual Window CreateSplashScreenWindow() => new Window();

        /// <summary>
        /// Create a new main window<br />
        /// Override this method to create your own main window
        /// </summary>
        /// <returns></returns>
        protected virtual Window CreateMainWindow() => new Window();

        /// <summary>
        /// Show splash screen and main window
        /// </summary>
        protected virtual void ShowSplashScreenAndWindow()
        {
            MainWindow = CreateMainWindow();

            if (IsSplashScreenEnabled)
            {
                SplashScreenWindow = CreateSplashScreenWindow();
                SplashScreenWindow.Show();

                DateTime splashScreenEnd = DateTime.Now + SplashScreenDuration;

                while (DateTime.Now < splashScreenEnd)
                {
                    Thread.Sleep(200);
                }

                SplashScreenWindow.Close();
            }

            MainWindow.Show();
            MainWindow.Closing += OnMainWindowClosing;
        }

        /// <summary>
        /// Occurs when the main window is closing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnMainWindowClosing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MainWindow != null)
                MainWindow.Closing -= OnMainWindowClosing;

            Shutdown();
        }

        #endregion
    }
}
