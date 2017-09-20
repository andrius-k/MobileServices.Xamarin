using Foundation;
using System;
using UIKit;
using MobileServices.ViewModels;
using GalaSoft.MvvmLight.Helpers;
using Splat;

namespace MobileServices.iOS
{
    public partial class BookDetailsController : BaseController<BookDetailsViewModel>
    {
        public override BookDetailsViewModel Vm => Application.Locator.BookDetailsViewModel;

        public BookDetailsController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
        }

        protected override void AddBindings()
        {
            base.AddBindings();

            Bindings.Add(this.SetBinding(() => Vm.Name, () => Title));
            Bindings.Add(this.SetBinding(() => Vm.Name, () => nameLabel.Text));
            Bindings.Add(this.SetBinding(() => Vm.Authors, () => authorsLabel.Text));
            Bindings.Add(this.SetBinding(() => Vm.Rating, () => ratingLabel.Text));
            Bindings.Add(this.SetBinding(() => Vm.Subtitle, () => subtitleLabel.Text));
			Bindings.Add(this.SetBinding(() => Vm.Description, () => descriptionLabel.Text));

            Bindings.Add(this.SetBinding(() => Vm.State).WhenSourceChanges(() =>
            {
                if (Vm.State.IsProcessingWaitingForData)
                    activityIndicator.StartAnimating();
                else
                    activityIndicator.StopAnimating();

                if (Vm.State.IsBusy == false)
                {
					thumbnailImageView.Layer.RemoveAllAnimations();
                    UIView.Animate(0.2, () => thumbnailImageView.Alpha = 1f, null);
                }
                else
                {
					UIView.Animate(0.5, 0,
							   UIViewAnimationOptions.Repeat | UIViewAnimationOptions.Autoreverse,
							   () => thumbnailImageView.Alpha = 0.5f, null);
                }

                if(Vm.State.IsFaulted)
                {
                    Console.WriteLine($"Unable to fetch new data: {Vm.State.ErrorMessage}");
                    Vm.State = AsyncState.Idle;
                }
            }));

            Bindings.Add(this.SetBinding(() => Vm.ThumbnailImage).WhenSourceChanges(() =>
			{
                thumbnailImageView.Image = Vm.ThumbnailImage?.ToNative() ?? UIImage.FromBundle("imagePlaceholder");
			}));
		}
    }
}