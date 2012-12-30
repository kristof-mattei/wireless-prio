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

		protected bool Equals(Profile other)
		{
			return string.Equals(this.ProfileName, other.ProfileName);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj))
			{
				return false;
			}
			if (ReferenceEquals(this, obj))
			{
				return true;
			}
			var other = obj as Profile;
			return other != null && Equals(other);
		}

		public override int GetHashCode()
		{
			return (!ReferenceEquals(null, this.ProfileName) ? this.ProfileName.GetHashCode() : 0);
		}
	}
}