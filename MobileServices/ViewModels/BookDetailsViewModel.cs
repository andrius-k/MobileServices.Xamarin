using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using MobileServices.Messages;
using System.Diagnostics;
using MobileServices.Services;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using MobileServices.Exceptions;
using MobileServices.DTOs;
using Splat;
using System.Threading.Tasks;
using System.Net.Http;
using ModernHttpClient;
namespace MobileServices.ViewModels
{
    public class BookDetailsViewModel : ViewModel
    {
        private string _name;
        public string Name 
        {
            get { return _name; }
            set { Set(ref _name, value); }
        }

		private string _authors;
		public string Authors
		{
			get { return _authors; }
			set { Set(ref _authors, value); }
		}

		private string _rating;
		public string Rating
		{
			get { return _rating; }
			set { Set(ref _rating, value); }
		}

		private string _subtitle;
		public string Subtitle
		{
			get { return _subtitle; }
			set { Set(ref _subtitle, value); }
		}

		private string _description;
		public string Description
		{
			get { return _description; }
			set { Set(ref _description, value); }
		}

		private IBitmap _thumbnailImage;
		public IBitmap ThumbnailImage
		{
			get { return _thumbnailImage; }
			set { Set(ref _thumbnailImage, value); }
		}

		private bool _isDownloadingThumbnail;
		public bool IsDownloadingThumbnail
		{
			get { return _isDownloadingThumbnail; }
			set { Set(ref _isDownloadingThumbnail, value); }
		}

		private readonly SerialDisposable _getBookSubscription = new SerialDisposable();
        private readonly SerialCancellationTokenSource _getbBookScts = new SerialCancellationTokenSource();
        private readonly SerialCancellationTokenSource _getThumbnailScts = new SerialCancellationTokenSource();
        private readonly HttpClient httpClient = new HttpClient(new NativeMessageHandler());
        private string _thumbnailUrl;

		private readonly IBooksService _booksService;
		private readonly ISchedulerProvider _schedulerProvider;

		public BookDetailsViewModel(
			IBooksService booksService,
			ISchedulerProvider schedulerProvider)
		{
			_booksService = booksService;
			_schedulerProvider = schedulerProvider;

			Messenger.Default.Register<NavigationMessage<string, BooksViewModel, BookDetailsViewModel>>(
				this, m => GetBookDetails(m.Parameter));
		}

        private void GetBookDetails(string id)
        {
            SetPlaceholders();

			State = AsyncState.ProcessingWaitingForData;

            _getBookSubscription.Disposable = _booksService.GetBook(id, _getbBookScts.NextToken)
				.SubscribeOn(_schedulerProvider.Background)
				.ObserveOn(_schedulerProvider.Foreground)
				.Subscribe(
					book =>
					{
						State = AsyncState.Processing;
						PopulateBookInfo(book);
					},
					ex => State = AsyncState.Faulted(ex is ApiException ? ex.Message : "Unknown error happened"),
					async () => 
		            {
                        await DownloadImage();
                        State = AsyncState.Idle;
		            });
        }

        private void PopulateBookInfo(BookDetails book)
        {
            if (book?.Info == null)
                return;

            Name = book.Info.Title;
            Authors = string.Join(",", book.Info.Authors);
            Rating = $"{book.Info.Rating} / 5";
            Subtitle = book.Info.Subtitle;
            Description = book.Info.Description;
            _thumbnailUrl = book.Info.ImageLinks?.SmallThumbnail;
            _thumbnailUrl = _thumbnailUrl?.Replace("http://", "https://");
        }

        private async Task DownloadImage()
        {
            if (_thumbnailUrl != null)
            {
				try
				{
					var response = await httpClient.GetAsync(_thumbnailUrl, _getThumbnailScts.NextToken);
					ThumbnailImage = await BitmapLoader.Current.Load(await response.Content.ReadAsStreamAsync(), null, null);
				}
				catch
				{
					ThumbnailImage = null;
				}
            }
        }

        private void SetPlaceholders()
        {
			ThumbnailImage = null;
			_thumbnailUrl = null;
			Name = "-";
			Authors = "-";
			Rating = "-";
			Subtitle = "-";
			Description = "-";
		}

   //     public override void ViewDisappeared()
   //     {
   //         base.ViewDisappeared();

   //         // Cancel any possibly ongoing web requests
   //         _getbBookScts.Cancel();
			//_getThumbnailScts.Cancel();
   //         _getBookSubscription.Disposable?.Dispose();

   //         ThumbnailImage = null;
   //         _thumbnailUrl = null;
			//Name = "-";
			//Authors = "-";
			//Rating = "-";
			//Subtitle = "-";
			//Description = "-";
        //}
    }
}
