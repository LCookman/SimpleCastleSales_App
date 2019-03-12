using SimpleCastleSales.View;
using SimpleCastleSales.Presenter;

namespace SimpleCastleSales
{
	class SimpleCastleSales
	{
		static void Main (string[] args)
		{
			ConsoleView consoleView = new ConsoleView ();
			SCSPresenter presenter = new SCSPresenter (consoleView);
			consoleView.HomePage ();
		}
	}
}
