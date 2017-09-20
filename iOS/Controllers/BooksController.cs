using Foundation;
using System;
using UIKit;
using MobileServices.ViewModels;
using GalaSoft.MvvmLight.Helpers;
using MobileServices.DTOs;
using MobileServices.iOS.Delegates;

namespace MobileServices.iOS
{
    public partial class BooksController : BaseController<BooksViewModel>
    {
        private const string CELL_ID = "BookCell";

        public override BooksViewModel Vm => Application.Locator.BooksViewModel;

        private UIRefreshControl _refresher;
        
        public BooksController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Title = "Books";

			tableView.Source = Vm.Books.GetTableViewSource(
                CreateCell, BindCell, factory: () => new BooksObservableTableViewSource());
            (tableView.Source as BooksObservableTableViewSource).ItemSelected += Handle_ItemSelected;

            _refresher = new UIRefreshControl();
            tableView.RefreshControl = _refresher;
            _refresher.ValueChanged += (sender, e) => 
            {
                if (_refresher.Refreshing)
                    Vm.GetBooksCommand.Execute(null);
            };
        }

        private UITableViewCell CreateCell(NSString cellIdentifier)
        {
            var cell = tableView.DequeueReusableCell(CELL_ID);
            if (cell == null)
                cell = new UITableViewCell(UITableViewCellStyle.Subtitle, CELL_ID);
            return cell;
        }

        private void BindCell(UITableViewCell cell, Book book, NSIndexPath path)
        {
            cell.TextLabel.Text = book.Info.Title;
            cell.DetailTextLabel.Text = book.Info.Subtitle;
        }

		private void Handle_ItemSelected(object sender, TableViewEventArgs args)
		{
            Vm.NavigateToDetailsCommand.Execute(args.Row);
        }

        protected override void AddBindings()
        {
            base.AddBindings();

			Bindings.Add(this.SetBinding(() => Vm.State).WhenSourceChanges(() =>
			{
                if (Vm.State.IsBusy && !_refresher.Refreshing)
                    _refresher.BeginRefreshing();
				else if(_refresher.Refreshing)
	                _refresher.EndRefreshing();
			}));
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            tableView.DeselectRow(tableView.IndexPathForSelectedRow, true);
        }
    }
}