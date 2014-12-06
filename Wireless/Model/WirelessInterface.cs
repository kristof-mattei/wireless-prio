namespace Wireless.Model
{
    using System;

    /// <summary>
    ///     Simple class to represent a wireless interface and its guid in the UI
    /// </summary>
    public class WirelessInterface
    {
        internal WirelessInterface(Guid interfaceGuid, string interfaceDescription)
        {
            this.InterfaceGuid = interfaceGuid;

            this.InterfaceDescription = interfaceDescription;
        }

        /// <summary>
        ///     Defines the description of the interface, e.g.:
        /// </summary>
        // ReSharper disable once MemberCanBePrivate.Global, it's bound on in XAML
        public string InterfaceDescription { get; set; }

        /// <summary>
        ///     The interface's Guid
        /// </summary>
        public readonly Guid InterfaceGuid;

        private bool Equals(WirelessInterface other)
        {
            return this.InterfaceGuid.Equals(other.InterfaceGuid);
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
            var other = obj as WirelessInterface;
            return other != null && this.Equals(other);
        }

        /// <summary>
        ///     Generates a hash for the current WirelessInterface, this is based on the <see cref="InterfaceGuid" />.
        /// </summary>
        /// <returns>
        ///     A hash code for the current object.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override int GetHashCode()
        {
            return this.InterfaceGuid.GetHashCode();
        }
    }
}