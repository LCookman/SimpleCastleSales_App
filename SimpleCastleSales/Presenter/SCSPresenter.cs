using SimpleCastleSales.View;
using SimpleCastleSales.Model;

namespace SimpleCastleSales.Presenter
{
	/// <summary>
	/// The presenter for the Simple Castle Sales (SCS) Application
	/// </summary>
	public class SCSPresenter
	{
		private IView mView;
		private CastleSalesModel mModel;
	
		public SCSPresenter (IView view, CastleSalesModel model)
		{
			mView = view;
			mModel = model;

			RegisterEvents ();
		}

		/// <summary>
		/// Bind the view event delegates to the corresponding model
		/// implementations. 
		/// </summary>
		private void RegisterEvents ()
		{
			mView.OnLoginEvent += mModel.Login;
			mView.OnLogoutEvent += mModel.Logout;
			mView.OnCreateUserEvent += mModel.CreateUserAccount;
			mView.OnCastleSaveEvent += mModel.CreateCastleFromEditor;
			mView.OnSearchAllEvent += mModel.FetchAllCastles;
			mView.OnCharacteristicSearch += mModel.SearchCastleByCharacteristic;
			mView.OnSaveToWishlistEvent += mModel.SaveToUserWishlist;
			mView.OnFetchUserWishlistEvent += mModel.FetchUserWishlist;
		}
	}
}
