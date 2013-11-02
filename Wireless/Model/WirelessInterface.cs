namespace Wireless.Model
{
    using System;

    /// <summary>
    ///     Simple class to represent a wireless interface and its guid in the UI
    /// </summary>
    public class WirelessInterface
    {
        public WirelessInterface(Guid interfaceGuid, string interfaceDescription)
        {
            this.InterfaceGuid = interfaceGuid;

            this.InterfaceDescription = interfaceDescription;
        }

        public string InterfaceDescription { get; private set; }

        public Guid InterfaceGuid { get; private set; }

        protected bool Equals(WirelessInterface other)
        {
            return this.InterfaceGuid.Equals(other.InterfaceGuid);
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
            var other = obj as WirelessInterface;
            return other != null && this.Equals(other);
        }

        public override int GetHashCode()
        {
            return this.InterfaceGuid.GetHashCode();
        }
    }
}