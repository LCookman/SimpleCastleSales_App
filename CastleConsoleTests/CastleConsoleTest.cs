using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using SimpleCastleSales.View;
using SimpleCastleSales.Presenter;
using SimpleCastleSales.Model;
using System;

namespace CastleConsoleTests
{
	[TestClass]
	public class CastleConsoleTest : IView
	{
		// Include the events we need to test.
		public event LoginEventHandler OnLoginEvent;
		public event LogoutEventHandler OnLogoutEvent;
		public event CreateUserEventHandler OnCreateUserEvent;
		public event SaveCastleEventHandler OnCastleSaveEvent;
		public event SearchCastleByCharacteristicEventHandler OnCharacteristicSearch;
		public event SearchCastlesEventHandler OnSearchAllEvent;
		public event SaveToWishlistEventHandler OnSaveToWishlistEvent;
		public event FetchUserWishlistEventHandler OnFetchUserWishlistEvent;

		private SCSPresenter mcPresenter;
		private CastleSalesModel mcModel;

		public void HomePage () { }

		[TestInitialize]
		public void Initialize ()
		{
			mcModel = new CastleSalesModel ();
			mcPresenter = new SCSPresenter (this, mcModel);
		}

		[TestMethod]
		public void TestAccountCreationPass ()
		{
			Assert.IsTrue (OnCreateUserEvent ("Logan", "test"));
		}

		[TestMethod]
		public void TestAccountCreationFail ()
		{
			Assert.IsTrue (OnCreateUserEvent ("Logan", "passtest"));
			Assert.IsFalse (OnCreateUserEvent ("logan", "failtest"));
		}

		[TestMethod]
		public void TestAccountLoginPassAndFail ()
		{
			// Load some users into the model
			OnCreateUserEvent ("Logan", "test");
			OnCreateUserEvent ("Shmitty", "apass");

			// Try login with correct values.
			Assert.IsTrue (OnLoginEvent ("Logan", "test"));
			Assert.IsTrue (OnLoginEvent ("Shmitty", "apass"));

			// Try logins with incorrect password
			Assert.IsFalse (OnLoginEvent ("Logan", "badpass"));
			Assert.IsFalse (OnLoginEvent ("Shmitty", "nopass"));

			// Try login with no user account.
			Assert.IsFalse (OnLoginEvent ("Becca", "temppass"));
		}

		[TestMethod]
		[ExpectedException (typeof (NullReferenceException))]
		public void TestCastleCreationWithoutUser ()
		{
			string castleName = "Testing Castle";
			List<string> features = new List<string> () { "Feature1", "Feature2", "Feature3" };
			List<string> histEvents = new List<string> () { "HistEvent1", "HistEvent2" };
			List<Tuple<string, string>> rooms = new List<Tuple<string, string>> ();

			rooms.Add (new Tuple<string, string> ("Reading Room", "A great place to read"));
			rooms.Add (new Tuple<string, string> ("Room 404", "This room cannot be found!"));

			// Create a castle without being logged in or having an account.
			// Since the castle takes the UID of the user who created it, it
			// makes sense that creating a castle while not logged in will
			// throw a Null Reference Exception.
			OnCastleSaveEvent (castleName, features, histEvents, rooms);
		}

		[TestMethod]
		[ExpectedException (typeof (NullReferenceException))]
		public void TestCastleCreationAfterLogout ()
		{
			string castleName = "Testing Castle";
			List<string> features = new List<string> () { "Feature1", "Feature2", "Feature3" };
			List<string> histEvents = new List<string> () { "HistEvent1", "HistEvent2" };
			List<Tuple<string, string>> rooms = new List<Tuple<string, string>> ();

			rooms.Add (new Tuple<string, string> ("Reading Room", "A great place to read"));
			rooms.Add (new Tuple<string, string> ("Room 404", "This room cannot be found!"));

			// Create an account.
			OnCreateUserEvent ("Logan", "test");

			// Save a new castle on that account.
			Assert.IsTrue (OnCastleSaveEvent (castleName, features, histEvents, rooms));

			// Logout the user
			OnLogoutEvent ();

			// Try and create a new castle while logged out.
			OnCastleSaveEvent ("New Castle Name", features, histEvents, rooms);
		}

		[TestMethod]
		public void TestCastleCreationWithUser ()
		{
			string castleName = "Testing Castle";
			List<string> features = new List<string> () { "Feature1", "Feature2", "Feature3" };
			List<string> histEvents = new List<string> () { "HistEvent1", "HistEvent2" };
			List<Tuple<string, string>> rooms = new List<Tuple<string, string>> ();

			rooms.Add (new Tuple<string, string> ("Reading Room", "A great place to read"));
			rooms.Add (new Tuple<string, string> ("Room 404", "This room cannot be found!"));

			// Create the user and get logged in.
			OnCreateUserEvent ("Logan", "test");

			// Test if we can create a castle now.
			Assert.IsTrue (OnCastleSaveEvent (castleName, features, histEvents, rooms));

			// Try creating the same exact castle - this will fail.
			Assert.IsFalse (OnCastleSaveEvent (castleName, features, histEvents, rooms));

			// Create the same castle with a different name - this will pass.
			Assert.IsTrue (OnCastleSaveEvent ("New Castle Name", features, histEvents, rooms));
		}

		[TestMethod]
		public void TestCastleSearching ()
		{
			string castleName = "Testing Castle";
			List<string> features = new List<string> () { "Feature1" };
			List<string> histEvents = new List<string> () { "HistEvent1" };
			List<Tuple<string, string>> rooms = new List<Tuple<string, string>> ();

			var room1 = new Tuple<string, string> ("Room 404", "This room cannot be found!");

			rooms.Add (room1);

			Dictionary<string, Dictionary<string, List<string>>> castleData;

			// Create the user and get logged in.
			OnCreateUserEvent ("Logan", "test");

			// Test if we can create a castle now.
			Assert.IsTrue (OnCastleSaveEvent (castleName, features, histEvents, rooms));

			// Since we only insert one castle it's all we will pull out.
			castleData = OnSearchAllEvent ();

			foreach (var castle in castleData)
			{
				Assert.AreEqual (castleName, castle.Key);
				foreach (var infoList in castle.Value)
				{
					Console.WriteLine (infoList.Key + ":");
					foreach (var info in infoList.Value)
					{
						if (infoList.Key.Equals ("Rooms", StringComparison.InvariantCultureIgnoreCase))
						{
							string[] roomArray = info.Split (':');
							Assert.AreEqual (roomArray[0], room1.Item1);
							Assert.AreEqual (roomArray[1], room1.Item2);
						}
						else if (infoList.Key.Equals ("Features", StringComparison.InvariantCultureIgnoreCase))
						{
							Assert.AreEqual (info, "Feature1");
						}
						else if (infoList.Key.Equals ("Historical Events", StringComparison.InvariantCultureIgnoreCase))
						{
							Assert.AreEqual (info, "HistEvent1");
						}
					}
				}
			}
		}

		[TestMethod]
		public void TestCastleSearchByCharacteristic ()
		{
			string castleName = "Testing Castle";
			List<string> features1 = new List<string> () { "Feature1" };
			List<string> features2 = new List<string> () { "Feature2" };
			List<string> histEvents = new List<string> () { "HistEvent1" };
			List<Tuple<string, string>> rooms = new List<Tuple<string, string>> ();

			rooms.Add (new Tuple<string, string> ("Reading Room", "A great place to read"));
			rooms.Add (new Tuple<string, string> ("Room 404", "This room cannot be found!"));

			Dictionary<string, Dictionary<string, List<string>>> castleData = null;

			// Create the user and get logged in.
			OnCreateUserEvent ("Logan", "test");

			Assert.IsTrue (OnCastleSaveEvent (castleName, features1, histEvents, rooms));
			Assert.IsTrue (OnCastleSaveEvent ("New Castle Name", features2, histEvents, rooms));
			Assert.IsTrue (OnCastleSaveEvent ("Castle Name 2.0", features1, histEvents, rooms));

			// Perform a search on Feature1
			castleData = OnCharacteristicSearch ("has a Feature1");
			Assert.AreNotEqual (castleData, null);

			// There should only be 2 castles that match Feature1 as a characteristic
			Assert.AreEqual (castleData.Count, 2);
			castleData = null;

			// Now there should only be one castle that has Feature2
			castleData = OnCharacteristicSearch ("has a Feature2");
			Assert.AreEqual (castleData.Count, 1);
		}

		[TestMethod]
		public void TestUserSaveCastleToWishlist ()
		{
			string castleName = "Testing Castle";
			List<string> features = new List<string> () { "Feature1" };
			List<string> histEvents = new List<string> () { "HistEvent1" };
			List<Tuple<string, string>> rooms = new List<Tuple<string, string>> ();

			rooms.Add (new Tuple<string, string> ("Room 404", "This room cannot be found!"));

			Dictionary<string, Dictionary<string, List<string>>> castleData = null;

			// Create the user and get logged in.
			OnCreateUserEvent ("Logan", "test");

			Assert.IsTrue (OnCastleSaveEvent (castleName, features, histEvents, rooms));
			Assert.IsTrue (OnCastleSaveEvent ("New Castle Name", features, histEvents, rooms));
			Assert.IsTrue (OnCastleSaveEvent ("Castle Name 2.0", features, histEvents, rooms));

			// Save some castles to the wishlist.
			Assert.IsTrue (OnSaveToWishlistEvent ("New Castle Name"));
			Assert.IsFalse (OnSaveToWishlistEvent ("New Castle Name"));
			Assert.IsTrue (OnSaveToWishlistEvent (castleName));

			// Fetch the current users wishlist.
			castleData = OnFetchUserWishlistEvent ();
			Assert.AreEqual (castleData.Count, 2);
		}
	}
}
