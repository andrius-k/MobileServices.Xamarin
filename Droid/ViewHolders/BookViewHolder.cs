using System;
using Android.Support.V7.Widget;
using Android.Widget;
using Android.Views;
namespace MobileServices.Droid.ViewHolders
{
    public class BookViewHolder : RecyclerView.ViewHolder
    {
		public TextView TitleTextView { get; private set; }
		public TextView SubtitleTextView { get; private set; }

		public BookViewHolder(View itemView, Action<int> listener) : base (itemView)
        {
            TitleTextView = itemView.FindViewById<TextView>(Resource.Id.book_title_text_view);
            SubtitleTextView = itemView.FindViewById<TextView>(Resource.Id.book_subtitle_text_view);

			itemView.Click += (sender, e) => listener?.Invoke(AdapterPosition);
		}

        public void SetData(string title, string subtitle)
        {
            TitleTextView.Text = title;
            SubtitleTextView.Text = subtitle;
        }
    }
}
