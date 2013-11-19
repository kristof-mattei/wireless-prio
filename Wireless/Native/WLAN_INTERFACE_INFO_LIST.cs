namespace Wireless.Native
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    ///     Contains an array of NIC information
    /// </summary>
    public struct WLAN_INTERFACE_INFO_LIST
    {
        /// <summary>
        ///     Array of WLAN interfaces.
        /// </summary>
        public WLAN_INTERFACE_INFO[] InterfaceInfo;

        /// <summary>
        ///     This member is not used by the wireless service. Applications can use this member when processing individual
        ///     interfaces.
        /// </summary>
        public int dwIndex;

        /// <summary>
        ///     Length of <see cref="InterfaceInfo" /> array
        /// </summary>
        public int dwNumberOfItems;

        /// <summary>
        ///     Constructor for WLAN_INTERFACE_INFO_LIST.
        ///     Constructor is needed because the InterfaceInfo member varies based on how many adapters are in the system.
        /// </summary>
        /// <param name="pointerToWlanInterfaceInfoList">the unmanaged pointer containing the list.</param>
        public WLAN_INTERFACE_INFO_LIST(IntPtr pointerToWlanInterfaceInfoList)
        {
            // The first 4 bytes are the number of WLAN_INTERFACE_INFO structures.
            this.dwNumberOfItems = Marshal.ReadInt32(pointerToWlanInterfaceInfoList, 0);

            // The next 4 bytes are the index of the current item in the unmanaged API.
            this.dwIndex = Marshal.ReadInt32(pointerToWlanInterfaceInfoList, 4);

            // Construct the array of WLAN_INTERFACE_INFO structures.
            this.InterfaceInfo = new WLAN_INTERFACE_INFO[this.dwNumberOfItems];

            // start pointer
            long start = pointerToWlanInterfaceInfoList.ToInt64() + 8; // we skip 8 for the first and second int in the structure

            // get the size of WLAN_INTERFACE_INFO
            int sizeOfWlanInterfaceInfo = Marshal.SizeOf(typeof (WLAN_INTERFACE_INFO));

            // we know there are dwNumberOfItems in the struct
            // so we can take each of those memory pieces and marshal them to WLAN_INTERFACE_INFO

            for (int i = 0; i <= this.dwNumberOfItems - 1; i++)
            {
                var pItemList = new IntPtr(start + (i * sizeOfWlanInterfaceInfo));

                this.InterfaceInfo[i] = (WLAN_INTERFACE_INFO) Marshal.PtrToStructure(pItemList, typeof (WLAN_INTERFACE_INFO));
            }
        }
    }
}