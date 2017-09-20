using System;
using MobileServices.Services;
using GalaSoft.MvvmLight.Command;
using MobileServices.DTOs;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Collections.Generic;
using MobileServices.Exceptions;
using GalaSoft.MvvmLight.Messaging;
using MobileServices.Messages;
namespace MobileServices.ViewModels
{
    public class BooksViewModel : ViewModel
    {
        private RelayCommand _getBooksCommand;
        public RelayCommand GetBooksCommand
		{
			get
			{
				return _getBooksCommand ??
					(_getBooksCommand = new RelayCommand(GetBooks,
                                                         () => !State.IsBusy));
			}
		}

		private RelayCommand<int> _navigateToDetailsCommand;
		public RelayCommand<int> NavigateToDetailsCommand
		{
			get
			{
				return _navigateToDetailsCommand ??
					(_navigateToDetailsCommand = new RelayCommand<int>(ShowDetails));
			}
		}

        public BatchObservableCollection<Book> Books = new BatchObservableCollection<Book>();

        private readonly SerialDisposable _getBooksSubscription = new SerialDisposable();
        private readonly SerialCancellationTokenSource _getBooksScts = new SerialCancellationTokenSource();

        private readonly IBooksService _booksService;
        private readonly ISchedulerProvider _schedulerProvider;
        private readonly INavigationService _navigationService;

        public BooksViewModel(
            IBooksService booksService, 
            ISchedulerProvider schedulerProvider,
            INavigationService navigationService)
        {
            _booksService = booksService;
            _schedulerProvider = schedulerProvider;
            _navigationService = navigationService;
        }

        public override void ViewAppeared()
        {
            base.ViewAppeared();
            GetBooksCommand.Execute(null);
        }

		private void GetBooks()
		{
			State = AsyncState.ProcessingWaitingForData;

            _getBooksSubscription.Disposable = _booksService.GetAllBooks(_getBooksScts.NextToken)
				.SubscribeOn(_schedulerProvider.Background)
				.ObserveOn(_schedulerProvider.Foreground)
				.Subscribe(
                    books => 
                    { 
                        State = AsyncState.Processing; 
                        PopulateBooks(books); 
                    },
                    ex => State = AsyncState.Faulted(ex is ApiException ? ex.Message : "Unknown error happened"),
                    () => State = AsyncState.Idle);
		}

        private void PopulateBooks(List<Book> books)
        {
            Books.ReplaceWithRange(books);
        }

        private void ShowDetails(int index)
        {
            string id = Books[index].Id;
            Messenger.Default.Send(new NavigationMessage<string, BooksViewModel, BookDetailsViewModel>(id));
            _navigationService.Push(ViewModelLocator.BookDetailsPageKey);
        }

        public override void ViewDisappeared()
        {
            base.ViewDisappeared();

			// Cancel possibly ongoing web request
			_getBooksScts.Cancel();
        }
    }
}
