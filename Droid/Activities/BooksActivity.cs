using System;
using Android.App;
using Android.OS;
using MobileServices.ViewModels;
using Android.Support.V7.Widget;
using Android.Views;
using MobileServices.DTOs;
using GalaSoft.MvvmLight.Helpers;
using MobileServices.Droid.ViewHolders;

namespace MobileServices.Droid.Activities
{
    [Activity(Label = "BooksActivity", MainLauncher = true, Icon = "@mipmap/icon")]
    public partial class BooksActivity : BaseActivity<BooksViewModel>
    {
        public override BooksViewModel Vm => App.Locator.BooksViewModel;

		protected override int ContentViewResId
		{
			get => Resource.Layout.activity_books;
		}

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetSupportActionBar(Toolbar);
            Title = "Books";

            var adapter = Vm.Books.GetRecyclerAdapter(BindViewHolder, CreateViewHolder, null);
            var layoutManager = new LinearLayoutManager(this);
			RecyclerView.SetLayoutManager(layoutManager);
			RecyclerView.SetAdapter(adapter);

            // Add item separator
            var itemDecoration = new DividerItemDecoration(RecyclerView.Context, layoutManager.Orientation);
            RecyclerView.AddItemDecoration(itemDecoration);

            SwipeRefreshLayout.Refresh += (sender, e) => 
            {
                Vm.GetBooksCommand.Execute(null);
            };
		}

		private RecyclerView.ViewHolder CreateViewHolder(ViewGroup parent, int viewType)
		{
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.item_book, parent, false);
			var holder = new BookViewHolder(itemView, ItemClick);
			return holder;
		}

		private void BindViewHolder(RecyclerView.ViewHolder holder, Book item, int position)
		{
			var cleanerHolder = (BookViewHolder)holder;
            cleanerHolder.SetData(item.Info.Title, item.Info.Subtitle);
		}

		private void ItemClick(int position)
		{
            Vm.NavigateToDetailsCommand.Execute(position);
        }

		protected override void AddBindings()
		{
			base.AddBindings();

			Bindings.Add(this.SetBinding(() => Vm.State).WhenSourceChanges(() =>
			{
                if (Vm.State.IsBusy)
                    SwipeRefreshLayout.Refreshing = true;
                else
                    SwipeRefreshLayout.Refreshing = false;
			}));
		}
    }
}
