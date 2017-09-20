using System;
using Android.Support.V7.App;
using System.Collections.Generic;
using GalaSoft.MvvmLight.Helpers;
using Android.OS;
using MobileServices.ViewModels;
namespace MobileServices.Droid.Activities
{
    public abstract class BaseActivity<T> : AppCompatActivity where T : ViewModel
    {
		public abstract T Vm { get; }

		// Keep bindings to make sure they are not garbage collected
		protected readonly List<Binding> Bindings = new List<Binding>();

        protected abstract int ContentViewResId 
        {
            get;
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(ContentViewResId);

            AddBindings();
        }

        protected override void OnResume()
        {
            base.OnResume();

            Vm.ViewAppeared();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

			foreach (var binding in Bindings)
				binding.Detach();

			Bindings.Clear();

            Vm.ViewDisappeared();
        }

		/// <summary>
		/// Use this method to add bindings. Add all bindings to <see cref="Bindings"/> list.
		/// All bindings will be removed in <see cref="OnDestroy"/> method.
		/// </summary>
		protected virtual void AddBindings()
		{
			// Add bindings here
		}
    }
}
