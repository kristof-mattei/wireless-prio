namespace WirelessPrio
{
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Linq;
	using System.Windows;
	using Model;
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
					WirelessInterfacesWithProfiles = GetAllWirelessConnectionsWithProfiles(),
				};
		}

		private ObservableCollection<WirelessInterfaceWithProfiles> GetAllWirelessConnectionsWithProfiles()
		{
			using (var nativeHelper = new Wireless.NativeHelper())
			{
				// build list

				var allInterfaces = nativeHelper.GetAvailableWirelessInterfaces();

				// get all their profiles
				var interfacesWithProfiles = allInterfaces.Select(e => new WirelessInterfaceWithProfiles()
					{
						WirelessInterface = e,
						Profiles = new ObservableCollection<Profile>(nativeHelper.GetProfilesForWirelessInterface(e.InterfaceGuid)),
					});

				return new ObservableCollection<WirelessInterfaceWithProfiles>(interfacesWithProfiles);
			}
		}
	}
}