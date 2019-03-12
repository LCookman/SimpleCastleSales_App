using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleCastleSales.Model.Structure
{
	public interface IStringable
	{
		/// <summary>
		/// This ToString is used to bundle up and send data
		/// back up into the view. Depending on how the view wants to
		/// display this information it can parse the string apart and
		/// display it how it wants to.
		/// </summary>
		/// <returns>The Serialized Info String</returns>
		string ToString ();
	}
}
