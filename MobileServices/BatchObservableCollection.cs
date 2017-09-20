using System;
using System.Collections.ObjectModel;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Specialized;
namespace MobileServices
{
    /// <summary>
    /// Observable collection that allows to insert or replace multiple items.
    /// </summary>
	public class BatchObservableCollection<T> : ObservableCollection<T>
	{
		private bool _suppressNotification = false;

		protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
		{
			if (!_suppressNotification)
				base.OnCollectionChanged(e);
		}

		public void DisableChangedEvents()
		{
			_suppressNotification = true;
		}

		public void EnableChangedEvents()
		{
			_suppressNotification = false;
			OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
		}

		public void InsertRange(IEnumerable<T> items)
		{
			if (items == null)
				return;

			DisableChangedEvents();
			foreach (var item in items)
				Items.Add(item);
			EnableChangedEvents();
		}

		public void ReplaceWithRange(IEnumerable<T> items)
		{
			if (items == null)
				return;

			DisableChangedEvents();
			Items.Clear();
			InsertRange(items);
		}

		/// <summary>
		/// Orders collection by in place without creating new instance.
		/// </summary>
		public void OrderByInPlace<TKey>(Func<T, TKey> keySelector)
		{
			var ordered = this.OrderBy(keySelector).ToList();

			for (int i = 0; i < ordered.Count; i++)
			{
				Move(IndexOf(ordered[i]), i);
			}
		}
	}
}
