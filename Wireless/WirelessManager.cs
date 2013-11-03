namespace Wireless
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
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

                // TODO set correct client version
                uint result = NativeWireless.WlanOpenHandle(2, IntPtr.Zero, out negotiatedVersion, out this._handle);

                if (NativeConstants.ERROR_SUCCESS != result)
                {
                    throw new Win32Exception((int) result);
                }
            }


            return this._handle;
        }

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

        public List<Profile> GetProfilesForWirelessInterface(Guid interfaceGuid)
        {
            IntPtr handle;

            uint result = NativeWireless.WlanGetProfileList(this.GetHandle(), ref interfaceGuid, IntPtr.Zero, out handle);

            result.ThrowIfNotSuccess();

            var wlanProfileInfoList = new WLAN_PROFILE_INFO_LIST(handle);

            NativeWireless.WlanFreeMemory(handle);

            List<Profile> profilesForWirelessInterface = wlanProfileInfoList.ProfileInfo.Select((wlanProfileInfo, index) => new Profile((uint) index, wlanProfileInfo.strProfileName)).ToList();

            return profilesForWirelessInterface;
        }

        public void DeleteProfile(Guid interfaceGuid, string profileName)
        {
            uint result = NativeWireless.WlanDeleteProfile(this.GetHandle(), ref interfaceGuid, profileName, IntPtr.Zero);

            result.ThrowIfNotSuccess();
        }

  

        public void SetProfilePosition(Guid interfaceGuid, string profileName, uint position)
        {
            NativeWireless.WlanSetProfilePosition(this.GetHandle(), ref interfaceGuid, profileName, position, IntPtr.Zero).ThrowIfNotSuccess();
        }

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