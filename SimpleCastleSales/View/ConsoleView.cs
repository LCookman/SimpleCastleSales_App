using System;
using System.Collections.Generic;
using SimpleCastleSales.View.Utility;

namespace SimpleCastleSales.View
{
	public class ConsoleView : IView
	{
		public event LoginEventHandler OnLoginEvent;
		public event LogoutEventHandler OnLogoutEvent;
		public event CreateUserEventHandler OnCreateUserEvent;
		public event SaveCastleEventHandler OnCastleSaveEvent;
		public event SearchCastleByCharacteristicEventHandler OnCharacteristicSearch;
		public event SearchCastlesEventHandler OnSearchAllEvent;
		public event SaveToWishlistEventHandler OnSaveToWishlistEvent;
		public event FetchUserWishlistEventHandler OnFetchUserWishlistEvent;

		string mLoggedInUsername;

		public ConsoleView ()
		{
			mLoggedInUsername = "";
		}

		/// <summary>
		/// Display the Home Page and corresponding options.
		/// </summary>
		public void HomePage ()
		{
			const int MIN_ANSWER = 1;
			const int MAX_ANSWER = 3;
			const int LOGIN = 1;
			const int CREATE_ACCOUNT = 2;
			const int EXIT = 3;

			bool bExit = false;
			string userInput;
			int answer;

			while (!bExit)
			{
				do
				{
					ConsoleDisplay.DisplayWelcomeMenu ();
					userInput = Console.ReadLine ();
					answer = ConsoleUtil.TryUserInputConvert (userInput);
				} while ((answer < MIN_ANSWER) || (answer > MAX_ANSWER));

				switch (answer)
				{
					case LOGIN:
						if (Login ())
							AccountPage ();
						break;

					case CREATE_ACCOUNT:
						if (CreateAccount ())
							AccountPage ();
						break;

					case EXIT:
						bExit = true;
						break;
				}
			}
		}

		/// <summary>
		/// The login page for a user.
		/// </summary>
		public bool Login ()
		{
			bool bLoggedIn = false;
			string username, password;

			ConsoleDisplay.DisplayLogin ();

			ConsoleDisplay.DisplayEnterUsername ();
			username = Console.ReadLine ();

			ConsoleDisplay.DisplayEnterPassword ();
			password = ConsoleUtil.EnterPassword ();

			if (OnLoginEvent (username, password))
			{
				mLoggedInUsername = username;
				bLoggedIn = true;
			}
			else
			{
				ConsoleDisplay.ErrorCouldNotLogin ();
			}
			return bLoggedIn;
		}

		/// <summary>
		/// Logout the user from the app and return to home page.
		/// </summary>
		public void Logout ()
		{
			OnLogoutEvent ();
			mLoggedInUsername = "";
		}

		/// <summary>
		/// Create a new user account. If an account is already created
		/// with the given username, it will ask you to try another username.
		/// </summary>
		public bool CreateAccount ()
		{
			bool bCreated;
			string username, password;

			ConsoleDisplay.DisplayCreateAccount ();

			ConsoleDisplay.DisplayEnterUsername ();
			username = Console.ReadLine ();

			ConsoleDisplay.DisplayEnterPassword ();
			password = ConsoleUtil.EnterPassword ();

			bCreated = OnCreateUserEvent (username, password);

			if (!bCreated)
			{
				ConsoleDisplay.ErrorUsernameTaken (username);
			}
			else
			{
				mLoggedInUsername = username;
			}
			return bCreated;
		}

		/// <summary>
		/// Display the account page and it's options.
		/// </summary>
		public void AccountPage ()
		{
			const int MIN_ANSWER = 1;
			const int MAX_ANSWER = 4;

			bool bExit = false;
			string userInput;
			int answer;

			while (!bExit)
			{
				ConsoleDisplay.DisplayAccountMenu (mLoggedInUsername);
				do
				{
					Console.Write ("Enter: ");
					userInput = Console.ReadLine ();
					answer = ConsoleUtil.TryUserInputConvert (userInput);
				} while ((answer < MIN_ANSWER) || (answer > MAX_ANSWER));

				switch (answer)
				{
					case 1:
						CreateCastle ();
						break;

					case 2:
						SearchCastles ();
						break;

					case 3:
						ViewWishlist ();
						break;

					case 4:
						Logout ();
						bExit = true;
						break;
				}
			}
		}

		/// <summary>
		/// Displays and handles user inputs to create all the data needed
		/// for a castle. This method calls the OnCastleSaveEvent in order
		/// to save the user input data.
		/// </summary>
		public void CreateCastle ()
		{
			int answer;
			bool bCreating = true;
			string userInput, castleName;
			string tempRoomName, tempRoomDescription;
			List<string> features, histEvents;
			List<Tuple<string, string>> rooms;

			features = new List<string> ();
			histEvents = new List<string> ();
			rooms = new List<Tuple<string, string>> ();

			ConsoleDisplay.DisplayNameCastle ();
			castleName = Console.ReadLine ();

			while (bCreating)
			{
				ConsoleDisplay.DisplayCastleCreateMenu (
					castleName,
					features,
					histEvents,
					rooms);

				userInput = Console.ReadLine ();
				answer = ConsoleUtil.TryUserInputConvert (userInput);

				switch (answer)
				{
					case 1:
						// Edit Name Here
						ConsoleDisplay.DisplayNameCastle ();
						castleName = Console.ReadLine ();
						break;

					case 2:
						//Add Room + Description
						ConsoleDisplay.DisplayRoomAddition ();
						tempRoomName = Console.ReadLine ();
						ConsoleDisplay.DisplayRoomDescription ();
						tempRoomDescription = Console.ReadLine ();

						rooms.Add (new Tuple<string, string> (tempRoomName, tempRoomDescription));
						break;

					case 3:
						//Add Feature
						ConsoleDisplay.DisplayFeatureAddition ();
						features.Add (Console.ReadLine ());
						break;

					case 4:
						//Add Historical Event
						ConsoleDisplay.DisplayHistoricalEventAddition ();
						histEvents.Add (Console.ReadLine ());
						break;

					case 5:
						//Save Castle
						if (!OnCastleSaveEvent (castleName, features, histEvents, rooms))
						{
							ConsoleDisplay.ErrorCastleNameAlreadyTaken ();
						}
						else
						{
							bCreating = false;
						}
						break;

					case 6:
						bCreating = false;
						break;
				}
			}
		}

		/// <summary>
		/// Displays the menu and handles the inputs for Searching
		/// castles. Either displaying all the castles or searching
		/// based on a specific characteristic of a castle.
		/// </summary>
		public void SearchCastles ()
		{
			const int MIN_ANSWER = 1;
			const int MAX_ANSWER = 3;

			int answer;
			bool bExit = false;
			string userInput, searchParams;

			Dictionary<string, Dictionary<string, List<string>>> castleList;

			while (!bExit)
			{
				ConsoleDisplay.DisplayCastleSearchMenu ();

				do
				{
					Console.Write ("Enter: ");
					userInput = Console.ReadLine ();
					answer = ConsoleUtil.TryUserInputConvert (userInput);
				} while ((answer < MIN_ANSWER) || (answer > MAX_ANSWER));

				switch (answer)
				{
					case 1:
						// View all castles
						castleList = OnSearchAllEvent ();
						if (!ConsoleUtil.CheckEmptyCastleCollection (castleList.Count))
						{
							WishlistAdditionCastleView (castleList);
						}
						break;

					case 2:
						// search by caracteristics
						ConsoleDisplay.DisplayCastleSearchByCharacteristic ();
						searchParams = Console.ReadLine ();
						castleList = OnCharacteristicSearch (searchParams);
						if (!ConsoleUtil.CheckEmptyCastleCollection (castleList.Count))
						{
							WishlistAdditionCastleView (castleList);
						}
						break;

					case 3:
						bExit = true;
						break;
				}
			}
		}

		/// <summary>
		/// Display and handle user inputs for viewing a users wishlist of
		/// castles. This method simply displays the wishlist and any button
		/// pressed can exit the wishlist back to the account page.
		/// </summary>
		public void ViewWishlist ()
		{
			var castleList = OnFetchUserWishlistEvent ();

			if (castleList.Count < 1)
			{
				ConsoleDisplay.ErrorNoCastlesInWishlist ();
			}
			else
			{
				ConsoleDisplay.FormatAndDisplayCastles ("Wishlist View", castleList);
				ConsoleDisplay.WaitOnWishlistView ();
			}
		}

		/// <summary>
		/// While within the Castle Display View bring up a menu that asks if
		/// you would like to add a specific castle to the wishlist. By inputing 
		/// the castles name into the console the user can add that castle to their
		/// wishlist.
		/// </summary>
		/// <param name="castlesInfo">The list of castles to display to the user.</param>
		public void WishlistAdditionCastleView (Dictionary<string, Dictionary<string, List<string>>> castlesInfo)
		{
			bool bExit = false;
			string userInput, castleName;
			int answer;

			while (!bExit)
			{
				ConsoleDisplay.FormatAndDisplayCastles ("Castle List View", castlesInfo);

				do
				{
					ConsoleDisplay.DisplayAddToWishlist ();
					userInput = Console.ReadLine ();
					answer = ConsoleUtil.TryUserInputConvert (userInput);
				} while (answer < 1 || answer > 2);

				switch (answer)
				{
					case 1:
						// Add to wishlist
						ConsoleDisplay.DisplayEnterToWishlist ();
						castleName = Console.ReadLine ();
						if (!OnSaveToWishlistEvent (castleName))
						{
							ConsoleDisplay.ErrorCastleNotFound (castleName);
						}
						else
						{
							ConsoleDisplay.DisplayAddedToWishlist (castleName);
						}
						break;

					case 2:
						// Exit back to searching
						bExit = true;
						break;
				}
			}
		}
	}
}
