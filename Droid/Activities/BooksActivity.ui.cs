using System;
using Android.Support.V7.Widget;
using Android.Support.V4.Widget;

namespace MobileServices.Droid.Activities
{
    public partial class BooksActivity
    {
        private Toolbar _toolbar;
        private SwipeRefreshLayout _swipeRefreshLayout;
        private RecyclerView _recyclerView;

		public Toolbar Toolbar => _toolbar
					?? (_toolbar = FindViewById<Toolbar>(Resource.Id.toolbar));

		public SwipeRefreshLayout SwipeRefreshLayout => _swipeRefreshLayout
                    ?? (_swipeRefreshLayout = FindViewById<SwipeRefreshLayout>(Resource.Id.swipe_refresh_layout));

        public RecyclerView RecyclerView => _recyclerView
                    ?? (_recyclerView = FindViewById<RecyclerView>(Resource.Id.recycler_view));
    }
}
