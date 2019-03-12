using SimpleCastleSales.View;
using SimpleCastleSales.Model;

namespace SimpleCastleSales.Presenter
{
	/// <summary>
	/// The presenter for the Simple Castle Sales (SCS) Application
	/// </summary>
	public class SCSPresenter
	{
		private IView mcView;
		private CastleSalesModel mcModel;
	
		public SCSPresenter (IView view)
		{
			mcView = view;
			mcModel = new CastleSalesModel ();

			RegisterEvents ();
		}

		/// <summary>
		/// Bind the view event delegates to the corresponding model
		/// implementations. 
		/// </summary>
		private void RegisterEvents ()
		{
			mcView.OnLoginEvent += mcModel.Login;
			mcView.OnLogoutEvent += mcModel.Logout;
			mcView.OnCreateUserEvent += mcModel.CreateUserAccount;
			mcView.OnCastleSaveEvent += mcModel.CreateCastleFromEditor;
			mcView.OnSearchAllEvent += mcModel.FetchAllCastles;
			mcView.OnCharacteristicSearch += mcModel.SearchCastleByCharacteristic;
			mcView.OnSaveToWishlistEvent += mcModel.SaveToUserWishlist;
			mcView.OnFetchUserWishlistEvent += mcModel.FetchUserWishlist;
		}
	}
}
