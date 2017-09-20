using Microsoft.Practices.ServiceLocation;
using GalaSoft.MvvmLight.Ioc;
using MobileServices.Services;
namespace MobileServices.ViewModels
{
    public class ViewModelLocator
    {
		public const string BooksPageKey = "BooksPage";
		public const string BookDetailsPageKey = "BookDetailsPage";

		static ViewModelLocator()
		{
			ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

			// Inject services here
			SimpleIoc.Default.Register<IApiService, ApiService>();
            SimpleIoc.Default.Register<IBlobCacheService, BlobCacheService>();
            SimpleIoc.Default.Register<ISchedulerProvider, SchedulerProvider>();
            SimpleIoc.Default.Register<IBooksService, BooksService>();

            // Add view models
            SimpleIoc.Default.Register<BooksViewModel>();
            SimpleIoc.Default.Register<BookDetailsViewModel>(true);
		}

        public BooksViewModel BooksViewModel
		{
			get
			{
				return ServiceLocator.Current.GetInstance<BooksViewModel>();
			}
		}

        public BookDetailsViewModel BookDetailsViewModel
		{
			get
			{
				return ServiceLocator.Current.GetInstance<BookDetailsViewModel>();
			}
		}
    }
}
