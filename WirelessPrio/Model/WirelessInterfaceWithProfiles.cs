namespace WirelessPrio.Model
{
    using System.Collections.ObjectModel;
    using System.Diagnostics.CodeAnalysis;
    using Wireless.Model;

    public class WirelessInterfaceWithProfiles
    {
        public WirelessInterfaceWithProfiles(WirelessInterface wirelessInterface, ObservableCollection<Profile> profiles)
        {
            this.WirelessInterface = wirelessInterface;
            this.Profiles = profiles;
        }

        public WirelessInterface WirelessInterface { get; private set; }

        [SuppressMessage("ReSharper", "CollectionNeverQueried.Global")]
        public ObservableCollection<Profile> Profiles { get; private set; }
    }
}