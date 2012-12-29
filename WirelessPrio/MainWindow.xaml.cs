namespace WirelessPrio
{
	using System.Collections.Generic;
	using System.Windows;
	using Wireless;

	/// <summary>
	///     Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			this.InitializeComponent();


			var manager = new Manager();

			manager.GetWirelessNetworks();
		}
	}
}