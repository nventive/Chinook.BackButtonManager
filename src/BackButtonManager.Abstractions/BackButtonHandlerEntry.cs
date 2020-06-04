using System;
using System.Collections.Generic;
using System.Text;

namespace Chinook.BackButtonManager
{
	/// <summary>
	/// This is a wrapper around <see cref="IBackButtonHandler"/> to add a priority property.
	/// </summary>
	public class BackButtonHandlerEntry
	{
		/// <summary>
		/// Creates a new instance of <see cref="BackButtonHandlerEntry"/>.
		/// </summary>
		public BackButtonHandlerEntry(IBackButtonHandler handler, int priority)
		{
			Handler = handler;
			Priority = priority;
		}

		public IBackButtonHandler Handler { get; }

		public int Priority { get; }
	}
}
