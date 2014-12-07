namespace WirelessPrio
{
    using System.Collections.Generic;
    using System.Windows;
    using GalaSoft.MvvmLight.Threading;
    using Microsoft.Shell;

    /// <summary>
    ///     Interaction logic for App.xaml
    ///     This page is compiled as Page, not as ApplicationDefinition because the latter generates its own Main method
    /// </summary>
    public partial class App : Application, ISingleInstanceApp
    {
        static App()
        {
            DispatcherHelper.Initialize();
        }

        #region ISingleInstanceApp Members

        public bool SignalExternalCommandLineArgs(IList<string> args)
        {
            return true;
        }

        #endregion

        private void App_OnStartup(object sender, StartupEventArgs e)
        {
        }
    }
}