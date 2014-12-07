namespace WirelessPrio.Model
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Threading.Tasks;
    using Wireless;
    using Wireless.Model;

    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    public class WirelessService : IWirelessService
    {
        public async Task<ObservableCollection<WirelessInterfaceWithProfiles>> GetWirelessInterfacesAndTheirProfilesAsync()
        {
            using (var wirelessManager = new WirelessManager())
            {
                // ReSharper disable once AccessToDisposedClosure
                return new ObservableCollection<WirelessInterfaceWithProfiles>(await Task.Run(() =>
                                                                                              {
                                                                                                  List<WirelessInterface> allInterfaces = wirelessManager.GetAvailableWirelessInterfaces();

                                                                                                  // get all their profiles
                                                                                                  List<WirelessInterfaceWithProfiles> interfacesWithProfiles = allInterfaces
                                                                                                      .Select(e => new WirelessInterfaceWithProfiles(e, new ObservableCollection<Profile>(wirelessManager.GetProfilesForWirelessInterface(e))))
                                                                                                      .ToList();

                                                                                                  return interfacesWithProfiles;
                                                                                              }));
            }
        }

        public void DeleteProfile(WirelessInterface wirelessInterface, Profile profile)
        {
            using (var wirelessManager = new WirelessManager())
            {
                wirelessManager.DeleteProfile(wirelessInterface, profile);
            }
        }
    }
}