using UIKit;
using MobileServices.ViewModels;

namespace MobileServices.iOS
{
    public class Application
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

        // This is the main entry point of the application.
        static void Main(string[] args)
        {
            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.
            UIApplication.Main(args, null, "AppDelegate");
        }
    }
}
