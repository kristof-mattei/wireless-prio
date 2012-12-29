namespace Model
{
	using System;

	public class Interface
	{
		public Interface(Guid interfaceGuid, string interfaceDescription)
		{
			this.InterfaceGuid = interfaceGuid;

			this.InterfaceDescription = interfaceDescription;
		}

		public string InterfaceDescription { get; private set; }

		public Guid InterfaceGuid { get; private set; }
	}
}