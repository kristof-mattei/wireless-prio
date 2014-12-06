namespace Wireless.Model
{
    /// <summary>
    ///     Defines a wireless profile (index and name)
    /// </summary>
    public class Profile
    {
        internal Profile(uint index, string profileName)
        {
            this.Index = index;
            this.ProfileName = profileName;
        }

        /// <summary>
        ///     The name of the profile
        /// </summary>
        public string ProfileName { get; private set; }

        /// <summary>
        ///     Used to define the order in the list, this one is also used when you want to reorder an item, we pass this index
        ///     onto the 'backend'
        ///     To define it's new order
        /// </summary>
        public uint Index { get; private set; }

        /// <summary>
        ///     Determines whether the specified <paramref name="other" /> is equal to the current profile.
        ///     This is based on the <see cref="ProfileName" />
        /// </summary>
        /// <param name="other">Profile to compare to</param>
        /// <returns><code>true</code> if they're equal, <code>false</code> if they're not.</returns>
        private bool Equals(Profile other)
        {
            return string.Equals(this.ProfileName, other.ProfileName);
        }


        /// <summary>
        ///     Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <returns>
        ///     true if the specified object  is equal to the current object; otherwise, false.
        /// </returns>
        /// <param name="obj">The object to compare with the current object. </param>
        /// <filterpriority>2</filterpriority>
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


        /// <summary>
        ///     Creates a hashcode for the current profile
        /// </summary>
        /// <returns>
        ///     A hash code for the current object.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override int GetHashCode()
        {
            return (!ReferenceEquals(null, this.ProfileName) ? this.ProfileName.GetHashCode() : 0);
        }
    }
}