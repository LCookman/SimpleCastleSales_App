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
		private static int UID_Generator = 0;

		private readonly string mPassword;
		private List<Castle> mUserWishlist;

		public int UID { get; }
		public string Username { get; }

		public Account (string username, string password)
		{
			UID = UID_Generator++;
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
		public bool AddCastleToWishlist (Castle castle)
		{
			bool bContains = mUserWishlist.Contains (castle);
			if (!bContains)
			{
				mUserWishlist.Add (castle);
			}
			return !bContains;
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
