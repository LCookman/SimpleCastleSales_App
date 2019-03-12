namespace SimpleCastleSales.Model.Structure
{
	/// <summary>
	/// Represents a room and it's description that resides
	/// within a castle.
	/// </summary>
	public class Room : IStringable
	{
		private readonly string mRoomName;
		private readonly string mRoomDescription;

		public Room (string name, string description)
		{
			mRoomName = name;
			mRoomDescription = description;
		}

		public override string ToString ()
		{
			return $"{mRoomName}:{mRoomDescription}";
		}
	}
}
