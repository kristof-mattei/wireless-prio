namespace WirelessPrio
{
    using System;
    using Microsoft.Shell;

    public static class EntryPoint
    {
        private const string Unique = "01E9936D-271D-4B30-9E38-4D6BEA78E012";

        /// <summary>
        ///     Application Entry Point.
        /// </summary>
        [STAThread()]
        public static void Main()
        {
            if (!SingleInstance<App>.InitializeAsFirstInstance(Unique))
            {
                return;
            }

            var application = new App();

            application.InitializeComponent();
            application.Run();

            // Allow single instance code to perform cleanup operations
            SingleInstance<App>.Cleanup();
        }
    }
}