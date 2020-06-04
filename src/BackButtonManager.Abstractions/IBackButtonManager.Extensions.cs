using System;
using System.Collections.Generic;
using System.Text;

namespace Chinook.BackButtonManager
{
	/// <summary>
	/// This class provides extensions methods on <see cref="IBackButtonManager"/>.
	/// </summary>
	public static class BackButtonManagerExtensions
	{
		/// <summary>
		/// Adds a <see cref="IBackButtonHandler"/> and return an <see cref="IDisposable"/> to conveniently remove it later.
		/// </summary>
		/// <param name="backButtonManager"></param>
		/// <param name="handler">The handler to add.</param>
		/// <param name="priority">The optional priority.</param>
		/// <returns>A disposable that will remove the handler when <see cref="IDisposable.Dispose"/> is invoked.</returns>
		public static IDisposable RegisterHandler(this IBackButtonManager backButtonManager, IBackButtonHandler handler, int? priority = null)
		{
			backButtonManager.AddHandler(handler, priority);

			return new ActionDisposable(RemoveHandler);

			void RemoveHandler()
			{
				backButtonManager.RemoveHandler(handler);
			}
		}

		private class ActionDisposable : IDisposable
		{
			private readonly Action _action;

			public ActionDisposable(Action action)
			{
				_action = action;
			}

			public void Dispose()
			{
				_action();
			}
		}
	}
}
