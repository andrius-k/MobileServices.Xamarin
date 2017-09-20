using System;
using MobileServices.ViewModels;
namespace MobileServices.Messages
{
	/// <summary>
	/// Send out this message when navigating to another view (or view model).
	/// TParam is the type of navigation parameter.
	/// TFromVM and TToVM are there just to make sure only correct VM will receive the message.
	/// </summary>
	public class NavigationMessage<TParam, TFromVM, TToVM>
        where TFromVM : ViewModel
        where TToVM : ViewModel
	{
		public NavigationMessage(TParam paramter)
		{
			Parameter = paramter;
		}

		public TParam Parameter { get; private set; }
	}
}
