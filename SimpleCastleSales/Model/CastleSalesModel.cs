using System;
using System.Linq;
using System.Collections.Generic;
using SimpleCastleSales.Model.Structure;

namespace SimpleCastleSales.Model
{
	/// <summary>
	/// The model for the Simple Castle Sales (SCS) Application.
	/// </summary>
	public class CastleSalesModel
	{
		private Account mLoggedInUser = null;
		private Dictionary<string, Account> mCreatedUsers;
		private List<Castle> mListedCastles;

		public CastleSalesModel ()
		{
			mCreatedUsers = new Dictionary<string, Account> ();
			mListedCastles = new List<Castle> ();
		}

		/// <summary>
		/// Login the specified user if their credentials are accurate. Obviously
		/// this is a very incorrect way to authenticate/login users, although I want to
		/// keep this as simple as possible.
		/// </summary>
		/// <param name="username">The username of the user logging in</param>
		/// <param name="password">The password of the logging in user.</param>
		public bool Login (string username, string password)
		{
			bool bAuthenticated = false;
			if (mCreatedUsers.Any (user => username.Equals (user.Key, StringComparison.InvariantCultureIgnoreCase)))
			{
				Account accnt = mCreatedUsers.GetValueOrDefault (username);
				if (accnt.Verify (password))
				{
					mLoggedInUser = accnt;
					bAuthenticated = true;
				}
			}
			return bAuthenticated;
		}

		/// <summary>
		/// Logs out the user from the model.
		/// </summary>
		public void Logout ()
		{
			mLoggedInUser = null;
		}

		/// <summary>
		/// Create a user and add them to the created users dictionary. Once a user creates
		/// their account, they are logged in automatically.
		/// </summary>
		/// <param name="username">The selected username of the user.</param>
		/// <param name="password">The secure password the use chose to create their account.</param>
		/// <returns>True or False depending on if the username is taken or not.</returns>
		public bool CreateUserAccount (string username, string password)
		{
			bool bIsCreated = false;
			Account newUser;

			if (!mCreatedUsers.Any (user => username.Equals (user.Key, StringComparison.InvariantCultureIgnoreCase)))
			{
				newUser = new Account (username, password);
				mCreatedUsers.Add (username, new Account (username, password));
				bIsCreated = true;

				mLoggedInUser = newUser;
			}
			return bIsCreated;
		}

		/// <summary>
		/// Creates a castle from the data provided to the method.
		/// </summary>
		/// <param name="castleName">The name of the castle</param>
		/// <param name="castleAddress">The address of the castle</param>
		/// <param name="features">The list of features to add to the castle</param>
		/// <param name="histEvents">The list of historical events of the castle</param>
		/// <param name="rooms">The list of rooms in a castle</param>
		public bool CreateCastleFromEditor (
			string castleName,
			int price,
			List<string> features,
			List<string> histEvents,
			List<Tuple<string, string>> rooms)
		{
			Castle newCastle;

			if (mListedCastles.Any (castle => string.Equals (castle.Name, castleName, StringComparison.InvariantCultureIgnoreCase)))
			{
				return false;
			}
			else
			{
				newCastle = new Castle (mLoggedInUser.UID, castleName, price);
			}

			foreach (Tuple<string, string> room in rooms)
			{
				newCastle.AddRoom (new Room (room.Item1, room.Item2));
			}

			foreach (string feature in features)
			{
				newCastle.AddFeature (new Feature (feature));
			}
			
			foreach (string histEvent in histEvents)
			{
				newCastle.AddHistEvent (new HistoricalEvent (histEvent));
			}

			mListedCastles.Add (newCastle);
			return true;
		}

		/// <summary>
		/// Saves the given named castle into the users wishlist.
		/// </summary>
		/// <param name="castleName">The name of the castle to save.</param>
		/// <returns>True or false depending on if the castle was found and added or not.</returns>
		public bool SaveToUserWishlist (string castleName)
		{
			bool bExists = false;
			Castle toSave;

			toSave = mListedCastles.Where (castle => castleName.Equals (castle.Name, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault ();

			if (toSave != null)
			{
				bExists = mLoggedInUser.AddCastleToWishlist (toSave);
			}
			return bExists;
		}

		/// <summary>
		/// Search the listed castles for a castle or castles that have the given
		/// characteristic.
		/// </summary>
		/// <param name="characteristic">The feature or characteristic of the castle to search for.</param>
		/// <returns>A list of found castles represented as their data.</returns>
		public Dictionary<string, Dictionary<string, List<string>>> SearchCastleByCharacteristic (string characteristic)
		{
			Dictionary<string, Dictionary<string, List<string>>> castlesData =
				new Dictionary<string, Dictionary<string, List<string>>> ();

			foreach (Castle castle in mListedCastles)
			{
				if (castle.HasCharacteristic (characteristic))
				{
					castlesData.Add (castle.Name, castle.FetchCastleInfo ());
				}
			}
			return castlesData;
		}

		/// <summary>
		/// Returns a list of every castle within the program in data form.
		/// </summary>
		public Dictionary<string, Dictionary<string, List<string>>> FetchAllCastles ()
		{
			Dictionary<string, Dictionary<string, List<string>>> castlesData =
				new Dictionary<string, Dictionary<string, List<string>>> ();

			foreach (Castle castle in mListedCastles)
			{
				castlesData.Add (castle.Name, castle.FetchCastleInfo ());
			}
			return castlesData;
		}

		/// <summary>
		/// Fetch the current logged in users wishlist of castles.
		/// </summary>
		/// <returns>The data of the current users castle wishlist to display.</returns>
		public Dictionary<string, Dictionary<string, List<string>>> FetchUserWishlist ()
		{
			Dictionary<string, Dictionary<string, List<string>>> castlesData =
				new Dictionary<string, Dictionary<string, List<string>>> ();

			foreach (Castle castle in mLoggedInUser.FetchWishlist ())
			{
				castlesData.Add (castle.Name, castle.FetchCastleInfo ());
			}

			return castlesData;
		}
	}
}
