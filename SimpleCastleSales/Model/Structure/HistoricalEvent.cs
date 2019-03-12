namespace SimpleCastleSales.Model.Structure
{
	/// <summary>
	/// Represents a historical event that a castle has gone
	/// through or is a part of.
	/// </summary>
	public class HistoricalEvent : IStringable
	{
		private string mHistoricalEvent;

		public HistoricalEvent (string histEvent)
		{
			mHistoricalEvent = histEvent;
		}

		public override string ToString ()
		{
			return mHistoricalEvent;
		}
	}
}
