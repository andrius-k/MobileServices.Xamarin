using System;
using System.Threading.Tasks;
using MobileServices.Services;
using UIKit;
using System.Collections.Generic;
namespace MobileServices.iOS.Services
{
    public class NavigationService : INavigationService
    {
        private readonly Dictionary<string, string> _pagesByKey = new Dictionary<string, string>();

        private readonly UINavigationController _navigationController;

        public NavigationService(UINavigationController navigationController)
        {
            _navigationController = navigationController ?? throw new ArgumentNullException(nameof(navigationController));
        }

        public void Configure(string pageKay, string storyboardId)
        {
            if (_pagesByKey.ContainsKey(pageKay))
                throw new ArgumentException($"Key {pageKay} is already registered", nameof(pageKay));
            
            _pagesByKey.Add(pageKay, storyboardId);
        }

		#region INavigationService implementation

		public void Push(string pageKey)
		{
            lock (_pagesByKey)
            {
				if (!_pagesByKey.ContainsKey(pageKey))
					throw new ArgumentException($"Key {pageKey} is not registered", nameof(pageKey));

				var controller = CreateViewController(_pagesByKey[pageKey]);

				_navigationController.PushViewController(controller, true);
            }
		}

        public void Back()
        {
            lock (_pagesByKey)
            {
                _navigationController.PopViewController(true);
            }
        }

        #endregion

        private UIViewController CreateViewController(string storyboardId)
        {
            return _navigationController.Storyboard.InstantiateViewController(storyboardId);
        }
    }
}
