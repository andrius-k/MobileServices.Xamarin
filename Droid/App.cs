using System;
using MobileServices.ViewModels;
namespace MobileServices.Droid
{
	public static class App
	{
		private static ViewModelLocator _locator;

		public static ViewModelLocator Locator
		{
			get
			{
				if (_locator == null)
				{
					// Inject platform specific services
					//SimpleIoc.Default.Register<INativeLocalizationUtil, NativeLocalizationUtil>();

					_locator = new ViewModelLocator();
				}

				return _locator;
			}
		}
	}
}
