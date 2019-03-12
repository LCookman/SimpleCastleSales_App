namespace SimpleCastleSales.Model.Structure
{
	/// <summary>
	/// Represents a castle feature. Such as a mote, or drawbridge.
	/// </summary>
	public class Feature : IStringable
	{
		public string Name { get; }

		public Feature (string feature)
		{
			Name = feature;
		}

		public override string ToString ()
		{
			return Name;
		}
	}
}
