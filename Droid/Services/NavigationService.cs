using System;
using MobileServices.Services;
using System.Collections.Generic;
using Plugin.CurrentActivity;
using Android.Content;
namespace MobileServices.Droid.Services
{
    public class NavigationService : INavigationService
    {
        private readonly Dictionary<string, Type> _pagesByKey = new Dictionary<string, Type>();

		public void Configure(string pageKay, Type activityType)
		{
			if (_pagesByKey.ContainsKey(pageKay))
				throw new ArgumentException($"Key {pageKay} is already registered", nameof(pageKay));

			_pagesByKey.Add(pageKay, activityType);
		}

		public void Push(string pageKey)
		{
            lock (_pagesByKey)
            {
				if (CrossCurrentActivity.Current.Activity == null)
				{
					throw new InvalidOperationException("No CurrentActivity found");
				}
				if (!_pagesByKey.ContainsKey(pageKey))
				{
					throw new ArgumentException(string.Format("No such page: {0}. Did you forget to call NavigationService.Configure?", pageKey), nameof(pageKey));
				}

                var intent = new Intent(CrossCurrentActivity.Current.Activity, _pagesByKey[pageKey]);
                CrossCurrentActivity.Current.Activity.StartActivity(intent);
            }
		}

        public void Back()
        {
            CrossCurrentActivity.Current.Activity.Finish();
        }
    }
}
