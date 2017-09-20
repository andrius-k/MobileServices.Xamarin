
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MobileServices.ViewModels;
using GalaSoft.MvvmLight.Helpers;
using Splat;
using Android.Support.V4.Widget;
using Android.Views.Animations;
using Android.Support.Design.Widget;

namespace MobileServices.Droid.Activities
{
    [Activity(Label = "BookDetailsActivity")]
    public partial class BookDetailsActivity : BaseActivity<BookDetailsViewModel>
    {
        public override BookDetailsViewModel Vm => App.Locator.BookDetailsViewModel;

        protected override int ContentViewResId
        {
            get => Resource.Layout.activity_book_details;
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetSupportActionBar(Toolbar);
		}

		protected override void AddBindings()
		{
			base.AddBindings();

			Bindings.Add(this.SetBinding(() => Vm.Name, () => Title));
            Bindings.Add(this.SetBinding(() => Vm.Name, () => NameTextView.Text));
            Bindings.Add(this.SetBinding(() => Vm.Authors, () => AuthorsTextView.Text));
            Bindings.Add(this.SetBinding(() => Vm.Rating, () => RatingTextView.Text));
            Bindings.Add(this.SetBinding(() => Vm.Subtitle, () => SubtitleTextView.Text));
            Bindings.Add(this.SetBinding(() => Vm.Description, () => DescriptionTextView.Text));

			Bindings.Add(this.SetBinding(() => Vm.State).WhenSourceChanges(() =>
			{
                if (Vm.State.IsProcessingWaitingForData)
                    ProgressBar.Visibility = ViewStates.Visible;
				else
                    ProgressBar.Visibility = ViewStates.Gone;

				if (Vm.State.IsBusy == false)
				{
                    ThumbnailImageView.ClearAnimation();

                    var animation = new AlphaAnimation(ThumbnailImageView.Alpha, 1.0f);
                    animation.Duration = 200;
                    animation.FillAfter = true;
                    ThumbnailImageView.StartAnimation(animation);
				}
				else
				{
                    var animation = AnimationUtils.LoadAnimation(this, Resource.Animation.tween);
                    ThumbnailImageView.StartAnimation(animation);
				}

				if (Vm.State.IsFaulted)
				{
                    var parentLayout = FindViewById<ViewGroup>(Android.Resource.Id.Content);
					Snackbar
					  .Make(parentLayout, $"Unable to fetch new data: {Vm.State.ErrorMessage}", Snackbar.LengthLong)
					  .Show();
					Vm.State = AsyncState.Idle;
				}
			}));

			Bindings.Add(this.SetBinding(() => Vm.ThumbnailImage).WhenSourceChanges(() =>
			{
                ThumbnailImageView.SetImageDrawable(Vm.ThumbnailImage?.ToNative() 
                                                    ?? Resources.GetDrawable(Resource.Drawable.image_placeholder, Theme));
			}));
		}
    }
}
