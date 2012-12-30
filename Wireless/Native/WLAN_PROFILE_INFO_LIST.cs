namespace Wireless.Native
{
	using System;
	using System.Runtime.InteropServices;

	public struct WLAN_PROFILE_INFO_LIST
	{
		public uint dwNumberOfItems;
		public uint dwIndex;
		public WLAN_PROFILE_INFO[] ProfileInfo;

		public WLAN_PROFILE_INFO_LIST(IntPtr ppProfileList)
		{
			this.dwNumberOfItems = (uint)Marshal.ReadInt32(ppProfileList);

			this.dwIndex = (uint)Marshal.ReadInt32(ppProfileList, 4);

			this.ProfileInfo = new WLAN_PROFILE_INFO[this.dwNumberOfItems];

			long start = ppProfileList.ToInt64() + 8;

			int sizeOfWlanProfileInfo = Marshal.SizeOf(typeof(WLAN_PROFILE_INFO));

			for (int i = 0; i < this.dwNumberOfItems; i++)
			{
				IntPtr pItemList = new IntPtr(start + (i * sizeOfWlanProfileInfo));

				this.ProfileInfo[i] = (WLAN_PROFILE_INFO)Marshal.PtrToStructure(pItemList, typeof(WLAN_PROFILE_INFO));
			}
		}
	}
}