﻿using System;
using System.Text;

namespace SimpleCastleSales.View.Utility
{
	/// <summary>
	/// The console utility class for the command line application SimpleCastleSales.
	/// </summary>
	public class ConsoleUtil
	{
		/// <summary>
		/// Create a secure password when entered at the command line.
		/// </summary>
		/// <returns>A secured password.</returns>
		public static string EnterPassword ()
		{
			StringBuilder pass = new StringBuilder ();
			ConsoleKeyInfo info = Console.ReadKey (true);
			while (info.Key != ConsoleKey.Enter)
			{
				if (info.Key == ConsoleKey.Backspace)
				{
					if (pass.Length > 0)
					{
						pass.Remove (pass.Length - 1, 1);
						Console.Write (info.KeyChar);
						Console.Write (" ");
						Console.Write (info.KeyChar);
					}
				}
				else
				{
					pass.Append (info.KeyChar);
					Console.Write ("*");
				}
				info = Console.ReadKey (true);
			}
			Console.WriteLine ();
			return pass.ToString ();
		}

		/// <summary>
		/// Attempt to convert the users input from a string into
		/// an integer 32.
		/// </summary>
		/// <param name="userInput">The user input to convert to an integer</param>
		/// <returns>The userInput converted into an integer if the userInput is valid.</returns>
		public static int TryUserInputConvert (string userInput)
		{
			int answer;

			try
			{
				answer = Convert.ToInt32 (userInput);
			}
			catch (FormatException)
			{
				ConsoleDisplay.ErrorIncorrectInput ();
				answer = 0;
			}
			return answer;
		}

		/// <summary>
		/// Check if the collection of castles has more than 0 elements.
		/// </summary>
		/// <param name="castleInfoSize">The size of the castle info collection</param>
		/// <returns>True if the collection has zero elements false if there are elements.</returns>
		public static bool CheckEmptyCastleCollection (int castleInfoSize)
		{
			bool bHasNoElements = castleInfoSize < 1;
			if (bHasNoElements)
			{
				ConsoleDisplay.ErrorNoCastlesToDisplay ();
			}
			return bHasNoElements;
		}

		/// <summary>
		/// Loop on the name castle display until a valid name is given.
		/// </summary>
		/// <returns>The new name of the castle.</returns>
		public static string UserInputNewCastleName ()
		{
			string name = "";

			do
			{
				ConsoleDisplay.DisplayNameCastle ();
				name = Console.ReadLine ();
			} while (name.Equals (""));

			return name;
		}

		/// <summary>
		/// Loop until an accurate price is read.
		/// </summary>
		/// <returns>The new price of the castle.</returns>
		public static int UserInputNewPrice ()
		{
			int price = 0;

			do
			{
				ConsoleDisplay.DisplayPriceOfCastle ();
				price = ConsoleUtil.TryUserInputConvert (Console.ReadLine ());
			} while (price <= 0);

			return price;
		}
	}
}
