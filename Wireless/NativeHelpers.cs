namespace Wireless
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Linq;
	using Model;
	using Native;

	/// <summary>
	/// This class acts as a facade to use the native code
	/// </summary>
	public class NativeHelper : IDisposable
	{
		private IntPtr _handle;

		private IntPtr GetHandle()
		{
			if (IntPtr.Zero == this._handle)
			{
				// don't need this
				uint negotiatedVersion;

				// TODO set correct client version
				uint result = NativeWireless.WlanOpenHandle(2, IntPtr.Zero, out negotiatedVersion, out this._handle);

				if (NativeConstants.ERROR_SUCCESS != result)
					throw new Win32Exception((int) result);
			}


			return this._handle;
		}

		public List<WirelessInterface> GetAvailableWirelessInterfaces()
		{
			IntPtr handle;

			uint result = NativeWireless.WlanEnumInterfaces(this.GetHandle(), IntPtr.Zero, out handle);

			result.ThrowIfNotSuccess();

			WLAN_INTERFACE_INFO_LIST wlanInterfaceInfoList = new WLAN_INTERFACE_INFO_LIST(handle);

			NativeWireless.WlanFreeMemory(handle);

			// convert the 'native' type to something we can pass on to the front
			List<WirelessInterface> wirelessInterfaces = wlanInterfaceInfoList.InterfaceInfo.Select(e => new WirelessInterface(e.InterfaceGuid, e.strInterfaceDescription)).ToList();


			return wirelessInterfaces;
		}

		public List<Profile> GetProfilesForWirelessInterface(Guid interfaceGuid)
		{
			IntPtr handle;

			uint result = NativeWireless.WlanGetProfileList(this.GetHandle(), ref interfaceGuid, IntPtr.Zero, out handle);

			result.ThrowIfNotSuccess();

			WLAN_PROFILE_INFO_LIST wlanProfileInfoList = new WLAN_PROFILE_INFO_LIST(handle);

			NativeWireless.WlanFreeMemory(handle);

			List<Profile> profilesForWirelessInterface = wlanProfileInfoList.ProfileInfo.Select((wlanProfileInfo, index) => new Profile(index, wlanProfileInfo.strProfileName)).ToList();
			
			return profilesForWirelessInterface;
		}

		public void DeleteProfile(Guid interfaceGuid, string profileName)
		{
			uint result = NativeWireless.WlanDeleteProfile(GetHandle(), ref interfaceGuid, profileName, IntPtr.Zero);

			result.ThrowIfNotSuccess();
		}

		public void Dispose()
		{
			// close the handle
		}

		~NativeHelper()
		{
			
		}
	}
}