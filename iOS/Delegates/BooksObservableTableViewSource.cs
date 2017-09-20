using MobileServices.DTOs;
using GalaSoft.MvvmLight.Helpers;
using UIKit;
using Foundation;
namespace MobileServices.iOS.Delegates
{
    public class BooksObservableTableViewSource : ObservableTableViewSource<Book>
    {
		public delegate void TableViewEventHandler(object sender, TableViewEventArgs args);
		public event TableViewEventHandler ItemSelected;

		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			base.RowSelected(tableView, indexPath);
			ItemSelected?.Invoke(this, new TableViewEventArgs(indexPath.Section, indexPath.Row));
		}
    }
}
