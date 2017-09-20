using System;
namespace MobileServices.ViewModels
{
	public sealed class AsyncState
	{
		public static readonly AsyncState Idle = new AsyncState(false, false, null);
		public static readonly AsyncState Processing = new AsyncState(true, false, null);
		public static readonly AsyncState ProcessingWaitingForData = new AsyncState(true, true, null);

        private AsyncState(bool isBusy, bool waitingForData, string errorMessage)
		{
            IsBusy = isBusy;
			IsProcessing = !waitingForData && isBusy;
			IsProcessingWaitingForData = waitingForData && isBusy;
			IsFaulted = !string.IsNullOrEmpty(errorMessage);
			ErrorMessage = errorMessage;
		}

		public static AsyncState Faulted(string errorMessage)
		{
			if (string.IsNullOrEmpty(errorMessage))
                throw new ArgumentException("You must provide an error message.", nameof(errorMessage));

			return new AsyncState(false, false, errorMessage);
		}

		/// <summary>
		/// This is true if either IsProcessingWaitingForData or IsProcessing is true.
        /// This indicates that some network action is happening at the moment.
		/// </summary>
		public bool IsBusy { get; }

		/// <summary>
		/// This is usually set when UI is empty and we are processing to retrieve data and populate UI.
		/// We might want to show preloader on the screen to indicate that data will soon appear.
		/// </summary>
		public bool IsProcessingWaitingForData { get; }

		/// <summary>
		/// This is usually set when we are handling action but UI is populated.
		/// We might not want to present preloader in the middle of the screen. 
		/// Instead we might want to inform user about ongoing action in more subtile way.
		/// </summary>
		public bool IsProcessing { get; }

		/// <summary>
		/// This indicates that web action was not successful.
		/// </summary>
		public bool IsFaulted { get; }

		/// <summary>
		/// This is the message about the error. This is set when <see cref="IsFaulted"/> is true.
		/// </summary>
		public string ErrorMessage { get; }
	}
}
