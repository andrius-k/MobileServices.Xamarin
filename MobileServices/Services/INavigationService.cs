using System;
using System.Threading.Tasks;
namespace MobileServices.Services
{
    public interface INavigationService
    {
        /// <summary>
        /// Push view to the navigation stack.
        /// </summary>
        void Push(string pageKey);

		/// <summary>
		/// Go back the navigation stack.
		/// </summary>
        void Back();
    }
}
