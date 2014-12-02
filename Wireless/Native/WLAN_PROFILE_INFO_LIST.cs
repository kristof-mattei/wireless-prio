namespace Wireless.Native
{
    using System;
    using System.Runtime.InteropServices;

    public struct WLAN_PROFILE_INFO_LIST
    {
        public WLAN_PROFILE_INFO[] ProfileInfo;
        public uint dwIndex;
        public uint dwNumberOfItems;

        public WLAN_PROFILE_INFO_LIST(IntPtr ppProfileList)
        {
            this.dwNumberOfItems = (uint) Marshal.ReadInt32(ppProfileList);

            this.dwIndex = (uint) Marshal.ReadInt32(ppProfileList, (1 * Marshal.SizeOf(typeof(int))));

            this.ProfileInfo = new WLAN_PROFILE_INFO[this.dwNumberOfItems];

            long start = ppProfileList.ToInt64() + (2 * Marshal.SizeOf(typeof(int)));

            int sizeOfWlanProfileInfo = Marshal.SizeOf(typeof (WLAN_PROFILE_INFO));

            for (int i = 0; i < this.dwNumberOfItems; i++)
            {
                var pItemList = new IntPtr(start + (i * sizeOfWlanProfileInfo));

                this.ProfileInfo[i] = (WLAN_PROFILE_INFO) Marshal.PtrToStructure(pItemList, typeof (WLAN_PROFILE_INFO));
            }
        }
    }
}
