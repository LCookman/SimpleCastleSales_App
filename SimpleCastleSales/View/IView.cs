using System;
using System.Collections.Generic;

namespace SimpleCastleSales.View
{
	public delegate bool LoginEventHandler (string username, string password);
	public delegate bool CreateUserEventHandler (string username, string password);
	public delegate Dictionary<string, Dictionary<string, List<string>>> SearchCastleByCharacteristicEventHandler (string input);
	public delegate Dictionary<string, Dictionary<string, List<string>>> SearchCastlesEventHandler ();
	public delegate Dictionary<string, Dictionary<string, List<string>>> FetchUserWishlistEventHandler ();
	public delegate bool SaveToWishlistEventHandler (string castleName);
	public delegate void LogoutEventHandler ();
	public delegate bool SaveCastleEventHandler (
		string castleName,
		List<string> features, 
		List<string> histEvents,
		List<Tuple<string, string>> rooms);

	/// <summary>
	/// The interface for any view implementation
	/// </summary>
	public interface IView
	{
		void HomePage ();

		event LoginEventHandler OnLoginEvent;
		event LogoutEventHandler OnLogoutEvent;
		event CreateUserEventHandler OnCreateUserEvent;
		event SaveCastleEventHandler OnCastleSaveEvent;
		event SearchCastleByCharacteristicEventHandler OnCharacteristicSearch;
		event SearchCastlesEventHandler OnSearchAllEvent;
		event SaveToWishlistEventHandler OnSaveToWishlistEvent;
		event FetchUserWishlistEventHandler OnFetchUserWishlistEvent;
	}
}
