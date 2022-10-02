using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Helium.Core.Bootstrapper
{
    /// <summary>
    /// Bootstrapper for .NET applications
    /// </summary>
    public class Bootstrapper
    {
        #region Constructors

        /// <summary>
        /// Constructor for <see cref="Bootstrapper"/>
        /// </summary>
        public Bootstrapper()
        {
            Current = this;
            ServiceCollection = new ServiceCollection();
        }

        #endregion

        #region Events

        /// <summary>
        /// Occurs when the bootstrapper is starting
        /// </summary>
        public event EventHandler? Starting;

        /// <summary>
        /// Occurs when the bootstrapper started everything
        /// </summary>
        public event EventHandler? Started;

        /// <summary>
        /// Occurs when an unhandled exception is thrown
        /// </summary>
        public event EventHandler<Exception>? UnhandledException;

        /// <summary>
        /// Method called when the bootstrapper is starting
        /// </summary>
        protected virtual void OnStarting()
        {
            Starting?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Method called when the bootstrapper started everything
        /// </summary>
        protected virtual void OnStarted()
        {
            Started?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Method called when an unhandled exception occurs
        /// </summary>
        /// <param name="ex"></param>
        protected virtual void OnUnhandledException(Exception ex)
        {
            UnhandledException?.Invoke(this, ex);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Current bootstrapper
        /// </summary>
        public static Bootstrapper? Current { get; protected set; }

        /// <summary>
        /// Services collection
        /// </summary>
        public ServiceCollection ServiceCollection { get; protected set; }

        /// <summary>
        /// Services provider
        /// </summary>
        public ServiceProvider? ServiceProvider { get; protected set; }

        #endregion

        #region Public methods

        /// <summary>
        /// Runs the bootstrapper
        /// </summary>
        public virtual void Run()
        {
            OnStarting();
            ConfigureServices();
            RegisterUnhandledExceptions();
            OnStarted();
        }

        /// <summary>
        /// Terminates the bootstrapper
        /// </summary>
        public virtual void Shutdown()
        {
            UnregisterUnhandledExceptions();

            ServiceCollection.Clear();
            ServiceProvider?.Dispose();
        }

        #endregion

        #region Dependency injection methods

        /// <summary>
        /// Configure services<br />
        /// Override this method to add custom services
        /// </summary>
        /// <param name="services">Services</param>
        protected virtual void ConfigureServices(IServiceCollection services)
        { }

        protected void ConfigureServices()
        {
            ConfigureServices(ServiceCollection);
            ServiceProvider = ServiceCollection.BuildServiceProvider();
        }

        #endregion

        #region Unhandled exceptions methods

        /// <summary>
        /// Registers unhandled exceptions<br />
        /// Override this method to handle other unhandled exceptions
        /// </summary>
        protected virtual void RegisterUnhandledExceptions()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
        }

        /// <summary>
        /// Unregisters unhandled exceptions<br />
        /// Override this method to unhandle other unhandled exceptions
        /// </summary>
        protected virtual void UnregisterUnhandledExceptions()
        {
            AppDomain.CurrentDomain.UnhandledException -= CurrentDomain_UnhandledException;
            TaskScheduler.UnobservedTaskException -= TaskScheduler_UnobservedTaskException;
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is Exception ex)
                OnUnhandledException(ex);
        }

        private void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            e.SetObserved();
            OnUnhandledException(e.Exception);
        }

        #endregion
    }
}
