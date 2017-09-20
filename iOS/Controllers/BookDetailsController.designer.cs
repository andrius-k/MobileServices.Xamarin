// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace MobileServices.iOS
{
    [Register ("BookDetailsController")]
    partial class BookDetailsController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIActivityIndicatorView activityIndicator { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel authorsLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel authorsTitleLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel descriptionLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel descriptionTitleLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel nameLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel nameTitleLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel ratingLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel ratingTitleLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel subtitleLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel subtitleTitleLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView thumbnailImageView { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (activityIndicator != null) {
                activityIndicator.Dispose ();
                activityIndicator = null;
            }

            if (authorsLabel != null) {
                authorsLabel.Dispose ();
                authorsLabel = null;
            }

            if (authorsTitleLabel != null) {
                authorsTitleLabel.Dispose ();
                authorsTitleLabel = null;
            }

            if (descriptionLabel != null) {
                descriptionLabel.Dispose ();
                descriptionLabel = null;
            }

            if (descriptionTitleLabel != null) {
                descriptionTitleLabel.Dispose ();
                descriptionTitleLabel = null;
            }

            if (nameLabel != null) {
                nameLabel.Dispose ();
                nameLabel = null;
            }

            if (nameTitleLabel != null) {
                nameTitleLabel.Dispose ();
                nameTitleLabel = null;
            }

            if (ratingLabel != null) {
                ratingLabel.Dispose ();
                ratingLabel = null;
            }

            if (ratingTitleLabel != null) {
                ratingTitleLabel.Dispose ();
                ratingTitleLabel = null;
            }

            if (subtitleLabel != null) {
                subtitleLabel.Dispose ();
                subtitleLabel = null;
            }

            if (subtitleTitleLabel != null) {
                subtitleTitleLabel.Dispose ();
                subtitleTitleLabel = null;
            }

            if (thumbnailImageView != null) {
                thumbnailImageView.Dispose ();
                thumbnailImageView = null;
            }
        }
    }
}