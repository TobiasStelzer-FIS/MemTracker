using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Unity;
using System.Windows;
using MemTracker.Views;

namespace MemTracker
{
    class Bootstrapper : UnityBootstrapper
    {
        protected override System.Windows.DependencyObject CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void InitializeShell()
        {
            Application.Current.MainWindow.Show();
        }

        protected override Prism.Modularity.IModuleCatalog CreateModuleCatalog()
        {
            return new ConfigurationModuleCatalog();
        }

    }
}
