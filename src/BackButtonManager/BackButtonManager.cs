using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Chinook.BackButtonManager
{
	/// <summary>
	/// This is the default implementation of <see cref="IBackButtonManager"/>.
	/// </summary>
	public class BackButtonManager : IBackButtonManager
	{
		/// <summary>
		/// The increment used to assign the new priority when null is passed in <see cref="AddHandler(IBackButtonHandler, int?)"/>.
		/// </summary>
		public const int DefaultPriorityIncrement = 1000;

		private readonly List<IBackButtonSource> _sources = new List<IBackButtonSource>();
		private readonly List<BackButtonHandlerEntry> _handlers = new List<BackButtonHandlerEntry>();
		private readonly List<IBackButtonHandler> _handlersCurrentlyHandling = new List<IBackButtonHandler>();
		private readonly ILogger _logger;

		/// <summary>
		/// Creates a new instance of <see cref="BackButtonManager"/>.
		/// </summary>
		public BackButtonManager()
		{
			_logger = this.Log();
		}

		/// <inheritdoc/>
		public IEnumerable<IBackButtonSource> Sources => _sources;

		/// <inheritdoc/>
		public IEnumerable<BackButtonHandlerEntry> Handlers => _handlers;

		/// <inheritdoc/>
		public void AddSource(IBackButtonSource source)
		{
			source = source ?? throw new ArgumentNullException(nameof(source));

			_sources.Add(source);
			source.BackRequested += OnBackRequested;
		}

		/// <inheritdoc/>
		public void RemoveSource(IBackButtonSource source)
		{
			source = source ?? throw new ArgumentNullException(nameof(source));

			source.BackRequested -= OnBackRequested;
			_sources.Remove(source);
		}

		private void OnBackRequested(IBackButtonSource sender, BackRequestedEventArgs eventArgs)
		{
			eventArgs.Handled = TryHandleBack();
		}

		/// <inheritdoc/>
		public void AddHandler(IBackButtonHandler handler, int? priority = null)
		{
			handler = handler ?? throw new ArgumentNullException(nameof(handler));
			int entryPriority = priority ?? (_handlers.Any() ? _handlers.Max(e => e.Priority) + DefaultPriorityIncrement : DefaultPriorityIncrement);

			var entry = new BackButtonHandlerEntry(handler, entryPriority);
			_handlers.Add(entry);
			_handlers.Sort(comparison: CompareEntries);

			// Sorts by Priority, high to low.
			int CompareEntries(BackButtonHandlerEntry left, BackButtonHandlerEntry right)
			{
				return right.Priority.CompareTo(left.Priority);
			}
		}

		/// <inheritdoc/>
		public void RemoveHandler(IBackButtonHandler handler)
		{
			handler = handler ?? throw new ArgumentNullException(nameof(handler));

			var entry = _handlers.FirstOrDefault(e => e.Handler == handler);
			if (entry == null)
			{
				throw new KeyNotFoundException($"The specified handler named '{handler.Name}' was not found.");
			}

			_handlers.Remove(entry);
		}

		private bool TryHandleBack()
		{
			foreach (var handler in _handlers.Select(e => e.Handler))
			{
				if (handler.CanHandle())
				{
					if (!_handlersCurrentlyHandling.Contains(handler))
					{
						// We only call Handle() if that handler is not already handling the back.
						_handlersCurrentlyHandling.Add(handler);
						// TODO deal with missing warning for unobserved task
						Task.Run(async () =>
						{
							try
							{
								await handler.Handle(CancellationToken.None);
							}
							catch (Exception e)
							{
								if (_logger.IsEnabled(LogLevel.Error))
								{
									_logger.LogError(e, $"Caught unhandled exception from the '{handler.Name}' handler.");
								}
							}
							finally
							{
								_handlersCurrentlyHandling.Remove(handler);
							}
						});
					}

					// We break the loop as soon as we find a handler.
					return true;
				}
			}

			// If no handler was found, return false.
			return false;
		}

		/// <inheritdoc/>
		public void Dispose()
		{
			foreach (var source in _sources)
			{
				source.Dispose();
			}
		}
	}
}
