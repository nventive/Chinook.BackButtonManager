using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chinook.BackButtonManager
{
	/// <summary>
	/// This is the default implementation of <see cref="IBackButtonHandler"/>.
	/// </summary>
	public class BackButtonHandler : IBackButtonHandler
	{
		private readonly Func<bool> _canHandle;
		private readonly Func<CancellationToken, Task> _handle;

		/// <summary>
		/// Creates a new instance of <see cref="BackButtonHandler"/>.
		/// </summary>
		public BackButtonHandler(string name, Func<bool> canHandle, Func<CancellationToken, Task> handle)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentException("The parameter must have a value.", nameof(name));
			}

			Name = name;
			_canHandle = canHandle ?? throw new ArgumentNullException(nameof(canHandle));
			_handle = handle ?? throw new ArgumentNullException(nameof(handle));
		}

		/// <inheritdoc/>
		public string Name { get; }

		/// <inheritdoc/>
		public bool CanHandle()
		{
			return _canHandle();
		}

		/// <inheritdoc/>
		public Task Handle(CancellationToken ct)
		{
			return _handle(ct);
		}
	}
}
