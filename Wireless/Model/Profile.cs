namespace Wireless.Model
{
    /// <summary>
    ///     Defines a wireless profile (index and name)
    /// </summary>
    public class Profile
    {
        public Profile(uint index, string profileName)
        {
            this.Index = index;
            this.ProfileName = profileName;
        }

        public string ProfileName { get; private set; }

        /// <summary>
        ///     Used to define the order in the list, this one is also used when you want to reorder an item, we pass this index
        ///     onto the 'backend'
        ///     To define it's new order
        /// </summary>
        public uint Index { get; private set; }

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
            return other != null && this.Equals(other);
        }

        public override int GetHashCode()
        {
            return (!ReferenceEquals(null, this.ProfileName) ? this.ProfileName.GetHashCode() : 0);
        }
    }
}