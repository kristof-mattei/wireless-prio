namespace WirelessPrio
{
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
					WirelessInterfacesWithProfiles = this.GetAllWirelessConnectionsWithProfiles(),
				};
		}

		private ObservableCollection<WirelessInterfaceWithProfiles> GetAllWirelessConnectionsWithProfiles()
		{
			using (var nativeHelper = new WirelessManager())
			{
				// build list

				List<WirelessInterface> allInterfaces = nativeHelper.GetAvailableWirelessInterfaces();

				// get all their profiles
				IEnumerable<WirelessInterfaceWithProfiles> interfacesWithProfiles = allInterfaces.Select(e => new WirelessInterfaceWithProfiles()
					{
						WirelessInterface = e,
						Profiles = new ObservableCollection<Profile>(nativeHelper.GetProfilesForWirelessInterface(e.InterfaceGuid)),
					});

				return new ObservableCollection<WirelessInterfaceWithProfiles>(interfacesWithProfiles);
			}
		}
	}
}