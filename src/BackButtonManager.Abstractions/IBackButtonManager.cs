using System;
using System.Collections.Generic;
using System.Text;

namespace Chinook.BackButtonManager
{
	/// <summary>
	/// <see cref="IBackButtonManager"/> is a mediator between <see cref="IBackButtonSource"/> and <see cref="IBackButtonHandler"/>.
	/// It subscribes to <see cref="IBackButtonSource"/>s and invokes <see cref="IBackButtonHandler"/>s.
	/// </summary>
	public interface IBackButtonManager : IDisposable
	{
		/// <summary>
		/// The collection of <see cref="IBackButtonSource"/>s added via <see cref="AddSource(IBackButtonSource)"/>.
		/// </summary>
		IEnumerable<IBackButtonSource> Sources { get; }

		/// <summary>
		/// Adds a <see cref="IBackButtonSource"/> to <see cref="Sources"/> and subscribes to it.
		/// </summary>
		/// <param name="source">The source to add.</param>
		void AddSource(IBackButtonSource source);

		/// <summary>
		/// Unsubscribes and remove a <see cref="IBackButtonSource"/> from <see cref="Sources"/>.
		/// </summary>
		/// <param name="source">The source to remove.</param>
		void RemoveSource(IBackButtonSource source);

		/// <summary>
		/// The collection of <see cref="IBackButtonHandler"/>s added via <see cref="AddHandler(IBackButtonHandler, int?)"/>.
		/// Each item is wrapped by a <see cref="BackButtonHandlerEntry"/> to add the Priority.
		/// </summary>
		IEnumerable<BackButtonHandlerEntry> Handlers { get; }

		/// <summary>
		/// Adds a <see cref="IBackButtonHandler"/> to <see cref="Handlers"/>.
		/// </summary>
		/// <param name="handler">The handler to add.</param>
		/// <param name="priority">The desired priority. When null is passed, priority higher than the previous highest priority is automatically assigned.</param>
		void AddHandler(IBackButtonHandler handler, int? priority = null);

		/// <summary>
		/// Removes a <see cref="IBackButtonHandler"/> from <see cref="Handlers"/>.
		/// </summary>
		/// <param name="handler">The handler to remove.</param>
		void RemoveHandler(IBackButtonHandler handler);
	}
}
