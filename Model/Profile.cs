namespace Model
{
	/// <summary>
	///     Defines a wireless profile (index and name)
	/// </summary>
	public class Profile
	{
		public Profile(int index, string profileName)
		{
			this.Index = index;
			this.ProfileName = profileName;
		}

		public string ProfileName { get; private set; }
		public int Index { get; private set; }
	}
}