using System;
using System.Collections.Generic;

namespace SimpleCastleSales.View.Utility
{
	public class ConsoleDisplay
	{
		private static readonly string GENERIC_CONTINUE = "Press any key to continue...";
		private static readonly string DESIGN_SEPARATOR = "--------------------------------";

		/// <summary>
		/// The static display methods for managing menus.
		/// </summary>
		#region Menus
		public static void DisplayWelcomeMenu ()
		{
			Console.Clear ();
			Console.WriteLine (DESIGN_SEPARATOR);
			Console.WriteLine ("Welcome to the Castle Sales App.");
			Console.WriteLine (DESIGN_SEPARATOR);

			Console.WriteLine ();
			Console.WriteLine ("To use the app simply enter the number in front of the line ");
			Console.WriteLine ("you want to select into the console.");
			Console.WriteLine ();
			
			Console.WriteLine ("Please select a value from the menu.");
			Console.WriteLine ("1. Login");
			Console.WriteLine ("2. Create an Account");
			Console.WriteLine ("3. Exit");
			Console.Write ("Enter: ");
		}

		public static void DisplayAccountMenu (string username)
		{
			Console.Clear ();
			Console.WriteLine (DESIGN_SEPARATOR);
			Console.WriteLine ("Welcome to your account {0}!", username);
			Console.WriteLine (DESIGN_SEPARATOR);
			Console.WriteLine ();
			Console.WriteLine ("What would you like to do? Please select a value below.");
			Console.WriteLine ("1. Add/Create Castle");
			Console.WriteLine ("2. Search for Castles");
			Console.WriteLine ("3. View Wishlist");
			Console.WriteLine ("4. Logout");
		}

		public static void DisplayCastleCreateMenu (
			string castleName,
			List<string> features, 
			List<string> histEvents, 
			List<Tuple<string, string>> rooms)
		{
			const string ADDITION_PREFIX = "   + {0}";

			Console.Clear ();
			Console.WriteLine (DESIGN_SEPARATOR);
			Console.WriteLine ("Castle Editor");
			Console.WriteLine (DESIGN_SEPARATOR);
			Console.WriteLine ("Current Castle Name: {0}", castleName);
			Console.WriteLine ();
			Console.WriteLine ("1. Edit Name");
			Console.WriteLine ("2. Add Room and Description");
			if (rooms.Count > 0)
			{
				foreach (Tuple<string, string> room in rooms)
				{
					Console.WriteLine (ADDITION_PREFIX, room.Item1);
					Console.WriteLine ("    - {0}", room.Item2);
				}
			}
			Console.WriteLine ("3. Add Feature");
			if (features.Count > 0)
			{
				foreach (string feat in features)
				{
					Console.WriteLine (ADDITION_PREFIX, feat);
				}
			}
			Console.WriteLine ("4. Add Historical Event");
			if (histEvents.Count > 0)
			{
				foreach (string historyEvent in histEvents)
				{
					Console.WriteLine (ADDITION_PREFIX, historyEvent);
				}
			}
			Console.WriteLine ("5. Save Castle");
			Console.WriteLine ("6. Exit Castle Editor");
			Console.Write ("Enter: ");
		}

		public static void DisplayCastleSearchMenu ()
		{
			Console.Clear ();
			Console.WriteLine (DESIGN_SEPARATOR);
			Console.WriteLine ("Castle Searching");
			Console.WriteLine (DESIGN_SEPARATOR);
			Console.WriteLine ();
			Console.WriteLine ("1. View All Castles");
			Console.WriteLine ("2. Search by Characteristics ('i.e. has a moat')");
			Console.WriteLine ("3. Exit Searching");
		}

		public static void ErrorIncorrectInput ()
		{
			Console.WriteLine ("Incorrect input, please select a correct value.");
			DisplayContinue (GENERIC_CONTINUE);
		}

		private static void DisplayContinue (string message)
		{
			Console.WriteLine (message);
			Console.ReadKey ();
		}
		#endregion

		/// <summary>
		/// The static display methods for managing user accounts.
		/// </summary>
		#region Account
		public static void DisplayCreateAccount ()
		{
			Console.Clear ();
			Console.WriteLine (DESIGN_SEPARATOR);
			Console.WriteLine ("Account Creation");
			Console.WriteLine (DESIGN_SEPARATOR);
		}

		public static void DisplayEnterUsername ()
		{
			Console.Write ("Please enter a username: ");
		}

		public static void DisplayEnterPassword ()
		{
			Console.Write ("Please enter a password: ");
		}

		public static void DisplayLogin ()
		{
			Console.Clear ();
			Console.WriteLine (DESIGN_SEPARATOR);
			Console.WriteLine ("Account Login");
			Console.WriteLine (DESIGN_SEPARATOR);
			Console.WriteLine ("Please Login with your Credentials.");
		}

		public static void ErrorUsernameTaken (string username)
		{
			Console.WriteLine ($"Username {username} is already taken please try again.");
			DisplayContinue ("Press any key to return to home page.");
		}

		public static void ErrorCouldNotLogin ()
		{
			Console.WriteLine ("Either username or password were incorrect, please try again.");
			DisplayContinue (GENERIC_CONTINUE);
		}
		#endregion

		/// <summary>
		/// The static display methods for Creating a Castle.
		/// </summary>
		#region CastleCreation
		public static void DisplayNameCastle ()
		{
			Console.Clear ();
			Console.WriteLine (DESIGN_SEPARATOR);
			Console.WriteLine ("Edit Castle Name");
			Console.WriteLine (DESIGN_SEPARATOR);
			Console.Write ("Enter a castle name: ");
		}

		public static void DisplayRoomAddition ()
		{
			Console.Clear ();
			Console.WriteLine (DESIGN_SEPARATOR);
			Console.WriteLine ("Room Addition");
			Console.WriteLine (DESIGN_SEPARATOR);
			Console.Write ("Enter the name of the room: ");
		}

		public static void DisplayRoomDescription ()
		{
			Console.WriteLine ();
			Console.WriteLine (DESIGN_SEPARATOR);
			Console.WriteLine ("Room Description");
			Console.WriteLine (DESIGN_SEPARATOR);
			Console.WriteLine ("Please provide the description of the room.");
			Console.Write ("Enter: ");
		}

		public static void DisplayFeatureAddition ()
		{
			Console.Clear ();
			Console.WriteLine (DESIGN_SEPARATOR);
			Console.WriteLine ("Feature Addition");
			Console.WriteLine (DESIGN_SEPARATOR);
			Console.WriteLine ("Please enter a feature.");
			Console.Write ("Enter: ");
		}


		public static void DisplayHistoricalEventAddition ()
		{
			Console.Clear ();
			Console.WriteLine (DESIGN_SEPARATOR);
			Console.WriteLine ("Historical Event Addition");
			Console.WriteLine (DESIGN_SEPARATOR);
			Console.WriteLine ("Please enter a historical event description.");
			Console.Write ("Enter: ");
		}

		public static void ErrorCastleNameAlreadyTaken ()
		{
			Console.WriteLine ("Castle name already taken, please modify the current name.");
			DisplayContinue ("Press any key to go back to castle modification.");
		}
		#endregion

		/// <summary>
		/// The Display methods for viewing castles.
		/// </summary>
		#region CastleViewing
		public static void DisplayCastleSearchByCharacteristic ()
		{
			Console.WriteLine (DESIGN_SEPARATOR);
			Console.WriteLine ("Search Characteristic");
			Console.WriteLine (DESIGN_SEPARATOR);
			Console.WriteLine ();
			Console.WriteLine ("What Characteristic/Feature would you like to search by?");
			Console.WriteLine ("Examples: 'has a moat', 'has a drawbridge'");
			Console.Write ("Enter: ");
		}

		public static void FormatAndDisplayCastles (string menuTitle, Dictionary<string, Dictionary<string, List<string>>> castlesInfo)
		{
			const string INDENT = "  ";
			const string SEPERATOR = "---------------------------------";

			Console.Clear ();
			Console.WriteLine (SEPERATOR);
			Console.WriteLine (menuTitle);
			Console.WriteLine (SEPERATOR);
			foreach (var castle in castlesInfo)
			{
				Console.WriteLine ("Castle Name: " + castle.Key);
				foreach (var infoList in castle.Value)
				{
					if (infoList.Value.Count == 0)
					{
						Console.WriteLine (infoList.Key + ": None");
					}
					else
					{
						Console.WriteLine (infoList.Key + ":");
					}
					foreach (var info in infoList.Value)
					{
						if (infoList.Key.Equals ("Rooms", StringComparison.InvariantCultureIgnoreCase))
						{
							string[] roomArray = info.Split (':');
							Console.WriteLine (INDENT + "- Room Name: " + roomArray[0]);
							Console.WriteLine (INDENT + "- Room Description: " + roomArray[1]);
							Console.WriteLine ();
						}
						else
						{
							Console.WriteLine (INDENT + "- " + info);
						}
					}
				}
				Console.WriteLine (SEPERATOR);
				Console.WriteLine ();
			}
		}

		public static void DisplayAddToWishlist ()
		{
			Console.WriteLine ("1. Input Castle to Wishlist");
			Console.WriteLine ("2. Return to Search Page");
			Console.Write ("Enter: ");
		}

		public static void DisplayEnterToWishlist ()
		{
			Console.WriteLine ("Enter the name of the castle you wish to add to your wishlist.");
			Console.Write ("Enter: ");
		}

		public static void WaitOnWishlistView ()
		{
			DisplayContinue ("Press any key to return to account page...");
		}

		public static void DisplayAddedToWishlist (string name)
		{
			Console.WriteLine ($"The castle {name} has been added to your wishlist!");
			DisplayContinue ("Press any key to return to castle view...");
		}

		public static void ErrorCastleNotFound (string name)
		{
			Console.WriteLine ($"Castle with name, {name} could not be added to wishlist.");
			DisplayContinue (GENERIC_CONTINUE);
		}

		public static void ErrorNoCastlesInWishlist ()
		{
			Console.WriteLine ("You have no castles in your wishlist to display.");
			DisplayContinue ("Press any key to return to account page...");
		}

		public static void ErrorNoCastlesToDisplay ()
		{
			Console.WriteLine ("There are no castles to view.");
			DisplayContinue (GENERIC_CONTINUE);
		}
		#endregion
	}
}
