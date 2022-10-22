using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Helium.Core.Bootstrapper
{
    /// <summary>
    /// Bootstrapper for .NET applications
    /// </summary>
    public abstract class Bootstrapper
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
        /// Raises <see cref="Starting"/> event
        /// </summary>
        protected void RaiseStartingEvent()
        {
            Starting?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Raises <see cref="Started"/> event
        /// </summary>
        protected void RaiseStartedEvent()
        {
            Started?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Raises <see cref="UnhandledException"/> event
        /// </summary>
        /// <param name="ex">Unhandled exception</param>
        protected void RaiseUnhandledExceptionEvent(Exception ex)
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
            RaiseStartingEvent();
            ConfigureServices();
            RegisterUnhandledExceptions();
            RaiseStartedEvent();
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

        /// <summary>
        /// Configures services and build service provider
        /// </summary>
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

        /// <summary>
        /// Method called when <see cref="AppDomain.UnhandledException"/> is raised
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Unhandled exception args</param>
        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is Exception ex)
                RaiseUnhandledExceptionEvent(ex);
        }

        /// <summary>
        /// Method called when <see cref="TaskScheduler.UnobservedTaskException"/> is raised
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Unobserved task exception args</param>
        private void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            e.SetObserved();
            RaiseUnhandledExceptionEvent(e.Exception);
        }

        #endregion
    }
}
