namespace WirelessPrio.ViewModel
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using JetBrains.Annotations;
    using Wireless;
    using Wireless.Model;

    public sealed class MainWindowViewModel : INotifyPropertyChanged
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
                this.WirelessInterfacesWithProfiles = await Task.Factory.StartNew(() => this.GetWirelessInterfacesAndTheirProfiles());
            }
        }

        private ObservableCollection<WirelessInterfaceWithProfiles> GetWirelessInterfacesAndTheirProfiles()
        {
            using (var wirelessManager = new WirelessManager())
            {
                // build list
                List<WirelessInterface> allInterfaces = wirelessManager.GetAvailableWirelessInterfaces();

                // get all their profiles
                List<WirelessInterfaceWithProfiles> interfacesWithProfiles = allInterfaces.Select(e => new WirelessInterfaceWithProfiles()
                                                                                                       {
                                                                                                           WirelessInterface = e, 
                                                                                                           Profiles = new ObservableCollection<Profile>(wirelessManager.GetProfilesForWirelessInterface(e.InterfaceGuid)),
                                                                                                       }).ToList();

                return new ObservableCollection<WirelessInterfaceWithProfiles>(interfacesWithProfiles);
            }
        }


        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}