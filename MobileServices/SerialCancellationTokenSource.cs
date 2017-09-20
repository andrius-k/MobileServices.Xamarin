using System;
using System.Threading;
namespace MobileServices
{
    public class SerialCancellationTokenSource
    {
        private CancellationTokenSource _cts;

		/// <summary>
		/// Cancels previous CancellationToken, creates new CancellationTokenSource and returns its CancellationToken.
		/// </summary>
		public CancellationToken NextToken
        {
            get
            {
                _cts?.Cancel();
                _cts = new CancellationTokenSource();
                return _cts.Token;
            }
        }

        public void Cancel()
        {
            _cts?.Cancel();
        }
    }
}
