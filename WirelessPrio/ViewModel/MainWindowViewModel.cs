namespace WirelessPrio.ViewModel
{
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.ComponentModel;
	using System.Linq;
	using System.Runtime.CompilerServices;
	using System.Threading.Tasks;
	using Annotations;
	using Model;
	using Wireless;

	public class MainWindowViewModel : INotifyPropertyChanged
	{
		private ObservableCollection<WirelessInterfaceWithProfiles> _wirelessInterfacesWithProfiles;

		public MainWindowViewModel()
		{
			this.InstantiateMainWindowViewModel();
		}

		public ObservableCollection<WirelessInterfaceWithProfiles> WirelessInterfacesWithProfiles
		{
			get
			{
				return this._wirelessInterfacesWithProfiles;
			}
			set
			{
				if (Equals(value, this._wirelessInterfacesWithProfiles))
				{
					return;
				}
				this._wirelessInterfacesWithProfiles = value;
				this.OnPropertyChanged();
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		private async void InstantiateMainWindowViewModel()
		{
			if (ReferenceEquals(null, this._wirelessInterfacesWithProfiles))
			{
				this.WirelessInterfacesWithProfiles = await Task.Factory.StartNew(() => this.GetAllWirelessConnectionsWithProfiles());
			}
		}

		private ObservableCollection<WirelessInterfaceWithProfiles> GetAllWirelessConnectionsWithProfiles()
		{
			using (var nativeHelper = new WirelessManager())
			{
				// build list

				List<WirelessInterface> allInterfaces = nativeHelper.GetAvailableWirelessInterfaces();

				// get all their profiles
				List<WirelessInterfaceWithProfiles> interfacesWithProfiles = allInterfaces.Select(e => new WirelessInterfaceWithProfiles()
					{
						WirelessInterface = e,
						Profiles = new ObservableCollection<Profile>(nativeHelper.GetProfilesForWirelessInterface(e.InterfaceGuid)),
					}).ToList();

				return new ObservableCollection<WirelessInterfaceWithProfiles>(interfacesWithProfiles);
			}
		}

		[NotifyPropertyChangedInvocator]
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChangedEventHandler handler = this.PropertyChanged;
			if (handler != null)
			{
				handler(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}