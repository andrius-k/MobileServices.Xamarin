using GalaSoft.MvvmLight;
namespace MobileServices.ViewModels
{
    public class ViewModel : ViewModelBase
    {
		private AsyncState _state = AsyncState.Idle;
		public AsyncState State
		{
			get { return _state; }
			set { Set(ref _state, value); }
		}

		private string _message;
		/// <summary>
		/// When setter is called, PropertyChanged event will be fired even if value hasn't actually changed.
		/// This means that two way binding is impossible for this property.
		/// </summary>
		public string Message
		{
			get { return _message; }
			set
			{
				_message = value;
				RaisePropertyChanged(nameof(Message));
			}
		}

        // Lifecycle methods

        /// <summary>
        /// Called after view appears
        /// </summary>
        public virtual void ViewAppeared()
        {
            
        }

		/// <summary>
		/// Called after view disappears
		/// </summary>
		public virtual void ViewDisappeared()
		{

		}
    }
}
