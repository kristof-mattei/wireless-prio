namespace WirelessPrio.ViewModel
{
    using System;
    using System.Collections.ObjectModel;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using GalaSoft.MvvmLight;
    using Model;
    using Wireless.Model;

    public sealed class MainViewModel : ViewModelBase
    {
        private ObservableCollection<WirelessInterfaceWithProfiles> _wirelessInterfacesWithProfiles;
        private readonly IWirelessService _wirelessService;

        /// <summary>
        /// Command used to delete a profile
        /// </summary>
        public ICommand DeleteProfileCommand { get; set; }

        /// <summary>
        ///     The UI binds on this, one way to source, we need this to know when a profile has been deleted, in order to notify the backend
        /// </summary>
        public WirelessInterfaceWithProfiles SelectedWirelessInterfaceWithProfiles { get; set; }

        public MainViewModel(IWirelessService wirelessService)
        {
            this._wirelessService = wirelessService;

            this.DeleteProfileCommand = new VoidCommand<Profile>(this.DeleteProfile);
            this.PreviewMouseMove = Foo;

            this.GetWirelessInterfaces();
        }

        private void Foo(object sender, MouseEventArgs e)
        {
            //if (sender is ListBoxItem && e.LeftButton == MouseButtonState.Pressed)
            //{
            //    ListBoxItem draggedItem = sender as ListBoxItem;
            //    DragDrop.DoDragDrop(draggedItem, draggedItem.DataContext, DragDropEffects.Move);
            //    draggedItem.IsSelected = true;
            //}
        }

        public MouseEventHandler PreviewMouseMove { get; set; }

        private async void GetWirelessInterfaces()
        {
            this.WirelessInterfacesWithProfiles = await _wirelessService.GetWirelessInterfacesAndTheirProfilesAsync();
        }

        private void DeleteProfile(Profile profile)
        {
            // capture the variable
            var selectedWirelessInterfaceWithProfiles = this.SelectedWirelessInterfaceWithProfiles;

            // notify the 'backend'
            // this._wirelessService.DeleteProfile(selectedWirelessInterfaceWithProfiles.WirelessInterface, profile);

            // Remove it from our collection
            this.SelectedWirelessInterfaceWithProfiles.Profiles.Remove(profile);
        }

        public ObservableCollection<WirelessInterfaceWithProfiles> WirelessInterfacesWithProfiles
        {
            get { return this._wirelessInterfacesWithProfiles; }
            set
            {
                if (Equals(value, this._wirelessInterfacesWithProfiles))
                {
                    return;
                }
                this._wirelessInterfacesWithProfiles = value;

                this.RaisePropertyChanged();
            }
        }
    }
}