using System;
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
	}
}
