using System;
using UIKit;
using System.Collections.Generic;
using GalaSoft.MvvmLight.Helpers;
using MobileServices.ViewModels;
namespace MobileServices.iOS
{
    public abstract class BaseController<T> : UIViewController where T : ViewModel
    {
        public abstract T Vm { get; }

        // Keep bindings to make sure they are not garbage collected
		protected readonly List<Binding> Bindings = new List<Binding>();

		public BaseController(IntPtr handle) : base(handle)
		{
		}

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);

			AddBindings();
		}

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            Vm.ViewAppeared();
        }

		public override void ViewWillDisappear(bool animated)
		{
			base.ViewWillDisappear(animated);

			Bindings.ForEach(b => b.Detach());
			Bindings.Clear();
		}

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);

            Vm.ViewDisappeared();
        }

		/// <summary>
		/// Use this method to add bindings. Add all bindings to <see cref="Bindings"/> list.
		/// All bindings will be removed in <see cref="ViewWillDisappear"/> method.
		/// </summary>
		protected virtual void AddBindings()
		{
			// Add bindings here
		}
    }
}
