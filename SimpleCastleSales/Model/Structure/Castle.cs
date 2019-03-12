using System;
using System.Linq;
using System.Collections.Generic;

namespace SimpleCastleSales.Model.Structure
{
	public class Castle
	{
		private static int CID_Generator = 1;

		private readonly int mCreatorIdentifier;
		private readonly int mCID;

		public string Name { get; }
		public int Price { get; }

		private List<Feature> mFeatures;
		private List<Room> mRooms;
		private List<HistoricalEvent> mHistEvents;

		public Castle (int creatorIdentifier, string castleName, int cPrice)
		{
			mCID = CID_Generator++;
			mCreatorIdentifier = creatorIdentifier;
			Name = castleName;
			Price = cPrice;

			mFeatures = new List<Feature> ();
			mRooms = new List<Room> ();
			mHistEvents = new List<HistoricalEvent> ();
		}

		/// <summary>
		/// Add a room to the castle.
		/// </summary>
		/// <param name="room">The room to add to the castle.</param>
		public void AddRoom (Room room)
		{
			mRooms.Add (room);
		}

		/// <summary>
		/// Add a feature to the castle
		/// </summary>
		/// <param name="feat">The feature to add.</param>
		public void AddFeature (Feature feat)
		{
			mFeatures.Add (feat);
		}

		/// <summary>
		/// Add a historical event to the castle.
		/// </summary>
		/// <param name="evnt">The event to add to the castle.</param>
		public void AddHistEvent (HistoricalEvent evnt)
		{
			mHistEvents.Add (evnt);
		}
		
		/// <summary>
		/// Fetch the internal info from the castle to be sent to the
		/// front end.
		/// </summary>
		/// <returns>The data within this castle object.</returns>
		public Dictionary<string, List<string>> FetchCastleInfo ()
		{
			Dictionary<string, List<string>> dictList = new Dictionary<string, List<string>>
			{
				{ "Price", new List<string> () {Price.ToString ()} },
				{ "Rooms", new List<string> (mRooms.Select (room => room.ToString ())) },
				{ "Features", new List<string> (mFeatures.Select (feat => feat.ToString ())) },
				{ "Historical Events", new List<string> (mHistEvents.Select (evnts => evnts.ToString ())) }
			};
			return dictList;
		}

		/// <summary>
		/// Currently only handles "has a" characteristic searching.
		/// </summary>
		/// <param name="characteristic"></param>
		/// <returns>Whether or not the castle has the given characteristic</returns>
		public bool HasCharacteristic (string characteristic)
		{
			// Replace the 'has a' modifier and search for the characteristic
			string searchParam = characteristic.Replace ("has a ", "");
			return mFeatures.Any (feature => (feature.Name.Contains(searchParam, StringComparison.InvariantCultureIgnoreCase)));
		}
	}
}