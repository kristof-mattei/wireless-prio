namespace WirelessPrio.ViewModel
{
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using Wireless;
    using Wireless.Model;

    public class WirelessInterfaceWithProfiles
    {
        public WirelessInterfaceWithProfiles()
        {
            this.DeleteProfileCommand = new VoidCommand<Profile>(this.DeleteProfile);
        }

        public ICommand DeleteProfileCommand { get; set; }
        public WirelessInterface WirelessInterface { get; set; }
        public ObservableCollection<Profile> Profiles { get; set; }

        private void DeleteProfile(Profile profile)
        {
            using (var wirelessManager = new WirelessManager())
            {
                wirelessManager.DeleteProfile(this.WirelessInterface, profile);
            }

            this.Profiles.Remove(profile);
        }
    }
}