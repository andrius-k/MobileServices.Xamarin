using System;
namespace MobileServices.iOS.Delegates
{
	public class TableViewEventArgs : EventArgs
	{
		public int Section { get; private set; }
		public int Row { get; private set; }

		public TableViewEventArgs(int section, int row)
		{
			Row = row;
			Section = section;
		}
	}
}
