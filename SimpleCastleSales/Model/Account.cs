using System;
using System.Collections.Generic;
using SimpleCastleSales.Model.Structure;

namespace SimpleCastleSales.Model
{
	/// <summary>
	/// This class represents a user account.
	/// </summary>
	public class Account
	{
		private static int CurrentNewUserID = 0;

		private string mPassword;

		// The User Account Properties.
		public int UID { get; }
		public string Username { get; }

		List<Castle> mUserWishlist;

		public Account (string username, string password)
		{
			UID = CurrentNewUserID++;
			Username = username;
			mPassword = password;

			mUserWishlist = new List<Castle> ();
		}
		
		/// <summary>
		/// Verify's a users password. This is not the correct
		/// way to verify/store/manage a password, but for this
		/// sample I wanted to keep this simple.
		/// </summary>
		/// <param name="password">The password to check</param>
		/// <returns>True or false depending of if the parameterized password is correct.</returns>
		public bool Verify (string password)
		{
			return String.Equals (password, mPassword);
		}

		/// <summary>
		/// Adds a castle to the users wishlist.
		/// </summary>
		/// <param name="castle">The castle to add to the wishlist.</param>
		public void AddCastleToWishlist (Castle castle)
		{
			mUserWishlist.Add (castle);
		}

		/// <summary>
		/// Retrieves the users wishlist of castles.
		/// </summary>
		/// <returns>The users wishlist of castles</returns>
		public List<Castle> FetchWishlist ()
		{
			return mUserWishlist;
		}
	}
}
