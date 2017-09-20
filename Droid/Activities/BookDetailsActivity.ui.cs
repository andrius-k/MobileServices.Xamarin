using System;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.Widget;
namespace MobileServices.Droid.Activities
{
    public partial class BookDetailsActivity
    {
		private Toolbar _toolbar;
        private ProgressBar _progressBar;
        private ImageView _thumbnailImageView;
        private TextView _nameTextView;
        private TextView _authorsTextView;
        private TextView _ratingTextView;
        private TextView _subtitleTextView;
        private TextView _descriptionTextView;

		public Toolbar Toolbar => _toolbar
					?? (_toolbar = FindViewById<Toolbar>(Resource.Id.toolbar));

		public ProgressBar ProgressBar => _progressBar
                    ?? (_progressBar = FindViewById<ProgressBar>(Resource.Id.progress_bar));

		public ImageView ThumbnailImageView => _thumbnailImageView
                    ?? (_thumbnailImageView = FindViewById<ImageView>(Resource.Id.thumbnail_image_view));

		public TextView NameTextView => _nameTextView
                    ?? (_nameTextView = FindViewById<TextView>(Resource.Id.name_text_view));

		public TextView AuthorsTextView => _authorsTextView
					?? (_authorsTextView = FindViewById<TextView>(Resource.Id.authors_text_view));

		public TextView RatingTextView => _ratingTextView
					?? (_ratingTextView = FindViewById<TextView>(Resource.Id.rating_text_view));

		public TextView SubtitleTextView => _subtitleTextView
					?? (_subtitleTextView = FindViewById<TextView>(Resource.Id.subtitle_text_view));

		public TextView DescriptionTextView => _descriptionTextView
					?? (_descriptionTextView = FindViewById<TextView>(Resource.Id.description_text_view));
    }
}
