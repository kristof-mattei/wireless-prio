namespace WirelessPrio.Model
{
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using Wireless.Model;

    public interface IWirelessService
    {
        Task<ObservableCollection<WirelessInterfaceWithProfiles>> GetWirelessInterfacesAndTheirProfilesAsync();
        void DeleteProfile(WirelessInterface wirelessInterface, Profile profile);
    }
}