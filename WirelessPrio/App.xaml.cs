namespace WirelessPrio
{
    using System.Collections.Generic;
    using System.Windows;
    using Microsoft.Shell;

    /// <summary>
    ///     Interaction logic for App.xaml
    ///     This page is compiled as Page, not as ApplicationDefinition to omit generation of Main method
    /// </summary>
    public partial class App : Application, ISingleInstanceApp
    {
        #region ISingleInstanceApp Members

        public bool SignalExternalCommandLineArgs(IList<string> args)
        {
            return true;
        }

        #endregion
    }
}