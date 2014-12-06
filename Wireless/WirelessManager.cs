namespace Wireless
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using Model;
    using Native;

    /// <summary>
    ///     This class acts as a facade to use the native code
    /// </summary>
    public class WirelessManager : IDisposable
    {
        private IntPtr _handle;

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        private IntPtr GetHandle()
        {
            if (IntPtr.Zero == this._handle)
            {
                // don't need this
                uint negotiatedVersion;

                // 2 is the correct version for dwClientVersion as per http://msdn.microsoft.com/en-us/library/windows/desktop/ms706759.aspx
                uint result = NativeWireless.WlanOpenHandle(2, IntPtr.Zero, out negotiatedVersion, out this._handle);

                if (NativeConstants.ERROR_SUCCESS != result)
                {
                    throw new Win32Exception((int) result);
                }
            }


            return this._handle;
        }

        /// <summary>
        ///     Gets all the available wireless interfaces on the current machine
        /// </summary>
        /// <returns>List of <see cref="WirelessInterface" /></returns>
        public List<WirelessInterface> GetAvailableWirelessInterfaces()
        {
            IntPtr handle;

            uint result = NativeWireless.WlanEnumInterfaces(this.GetHandle(), IntPtr.Zero, out handle);

            result.ThrowIfNotSuccess();

            var wlanInterfaceInfoList = new WLAN_INTERFACE_INFO_LIST(handle);

            NativeWireless.WlanFreeMemory(handle);

            // convert the native type to something we can pass on to the front
            List<WirelessInterface> wirelessInterfaces = wlanInterfaceInfoList.InterfaceInfo.Select(e => new WirelessInterface(e.InterfaceGuid, e.strInterfaceDescription)).ToList();

            return wirelessInterfaces;
        }

        /// <summary>
        ///     Returns all the profiles associated with this Wireless Interface
        /// </summary>
        /// <param name="wirelessInterface">The wirelessInterface to get the profiles from</param>
        /// <returns></returns>
        public List<Profile> GetProfilesForWirelessInterface(WirelessInterface wirelessInterface)
        {
            IntPtr handle;

            // The interfaceGuid is passed in as a pointer which is why we need to extract it into a variable.
            // the function takes a const Guid* which cannot be exposed in C#
            Guid interfaceGuid = wirelessInterface.InterfaceGuid;

            uint result = NativeWireless.WlanGetProfileList(this.GetHandle(), ref interfaceGuid, IntPtr.Zero, out handle);

            result.ThrowIfNotSuccess();

            var wlanProfileInfoList = new WLAN_PROFILE_INFO_LIST(handle);

            NativeWireless.WlanFreeMemory(handle);

            List<Profile> profilesForWirelessInterface = wlanProfileInfoList.ProfileInfo.Select((wlanProfileInfo, index) => new Profile((uint) index, wlanProfileInfo.strProfileName)).ToList();

            return profilesForWirelessInterface;
        }

        /// <summary>
        ///     Deletes a <paramref name="profile" /> on a given <paramref name="wirelessInterface" />
        /// </summary>
        /// <param name="wirelessInterface">The wireless interface to delete the profile on</param>
        /// <param name="profile">The profile to delete</param>
        public void DeleteProfile(WirelessInterface wirelessInterface, Profile profile)
        {
            Guid interfaceGuid = wirelessInterface.InterfaceGuid;

            uint result = NativeWireless.WlanDeleteProfile(this.GetHandle(), ref interfaceGuid, profile.ProfileName, IntPtr.Zero);

            result.ThrowIfNotSuccess();
        }


        /// <summary>
        ///     Move the <paramref name="profile" /> to a given <paramref name="position" /> for the given <paramref name="wirelessInterface" />
        /// </summary>
        /// <param name="wirelessInterface">The wireless interface</param>
        /// <param name="profile">The profile</param>
        /// <param name="position">The position requested</param>
        public void SetProfilePosition(WirelessInterface wirelessInterface, Profile profile, uint position)
        {
            Guid interfaceGuid = wirelessInterface.InterfaceGuid;

            uint result = NativeWireless.WlanSetProfilePosition(this.GetHandle(), interfaceGuid, profile.ProfileName, position, IntPtr.Zero);

            result.ThrowIfNotSuccess();
        }

        /// <summary>Destructor</summary>
        ~WirelessManager()
        {
            this.Dispose(false);
        }

        private void Dispose(bool fromDispose)
        {
            if (fromDispose)
            {
                // do we have managed resources that require cleanup ? 
                // not now
            }

            if (IntPtr.Zero != this._handle)
            {
                NativeWireless.WlanCloseHandle(this._handle, IntPtr.Zero);
                this._handle = IntPtr.Zero;
            }
        }
    }
}