using SimpleCastleSales.View;
using SimpleCastleSales.Presenter;
using SimpleCastleSales.Model;

namespace SimpleCastleSales
{
	class SimpleCastleSales
	{
		/// <summary>
		/// Initialize the Model->Presenter->View workflow
		/// and start accepting events from the user.
		/// </summary>
		static void Main (string[] args)
		{
			ConsoleView consoleView = new ConsoleView ();
			SCSPresenter presenter = new SCSPresenter (consoleView, new CastleSalesModel ());
			consoleView.HomePage ();
		}
	}
}
