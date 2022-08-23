using System;
using System.Collections.Generic;
using System.Text;

namespace Chinook.BackButtonManager
{
	/// <summary>
	/// This represents an object that can request a back operation.
	/// </summary>
	public interface IBackButtonSource : IDisposable
	{
		/// <summary>
		/// The name representing this <see cref="IBackButtonSource"/>.
		/// </summary>
		string Name { get; }

		/// <summary>
		/// Event raised when this <see cref="IBackButtonSource"/> requests a back operation.
		/// </summary>
		event BackRequestedEventHandler BackRequested;
	}

	/// <summary>
	/// The event arguments for <see cref="BackRequestedEventHandler"/>.
	/// </summary>
	public class BackRequestedEventArgs : EventArgs
	{
		/// <summary>
		/// Handlers can set this to true to mark the event as handled.
		/// </summary>
		public bool Handled { get; set; }
	}

	/// <summary>
	/// The event handler delegate for <see cref="IBackButtonSource.BackRequested"/>.
	/// </summary>
	/// <param name="sender">The back button source that raises the event.</param>
	/// <param name="eventArgs">The event parameters.</param>
	public delegate void BackRequestedEventHandler(IBackButtonSource sender, BackRequestedEventArgs eventArgs);
}
