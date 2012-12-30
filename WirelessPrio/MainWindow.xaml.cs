namespace WirelessPrio
{
	using System.Collections.Generic;
	using System.Windows;
	using ViewModel;
	using Wireless;

	/// <summary>
	///     Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			this.InitializeComponent();
		}

		private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
		{
			// all done, set the viewmodel
			this.DataContext = new MainWindowViewModel()
				{

				};
		}
	}
}