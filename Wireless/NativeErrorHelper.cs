namespace Wireless
{
	using System.ComponentModel;
	using Native;

	public static class NativeErrorHelper
	{
		public static void ThrowIfNotSuccess(this uint resultCode)
		{
			if (NativeConstants.ERROR_SUCCESS != resultCode)
			{
				throw new Win32Exception((int) resultCode);
			}
		}
	}
}