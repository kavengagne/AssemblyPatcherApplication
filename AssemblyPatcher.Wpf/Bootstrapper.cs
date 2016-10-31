using System;
using System.Configuration;
using System.Windows;
using AssemblyPatcher.Wpf.ViewModels;
using Caliburn.Micro;


namespace AssemblyPatcher.Wpf
{
    public class Bootstrapper : BootstrapperBase
    {
        static Bootstrapper()
        {
            if ("true".Equals(ConfigurationManager.AppSettings.Get("Debug.Logging"), StringComparison.InvariantCultureIgnoreCase))
            {
                LogManager.GetLog = type => new DebugLog(type);
            }
        }

        public Bootstrapper()
        {
            Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }
    }
}
