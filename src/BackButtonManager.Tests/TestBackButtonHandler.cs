using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Chinook.BackButtonManager;

namespace BackButtonManager.Tests
{
	public class TestBackButtonHandler : IBackButtonHandler
	{
		private readonly TaskCompletionSource<bool> _tcs = new TaskCompletionSource<bool>();

		public TestBackButtonHandler()
		{
			Name = Guid.NewGuid().ToString();
		}
		public string Name { get; }

		public bool CanHandle { get; set; } = true;

		bool IBackButtonHandler.CanHandle()
		{
			return CanHandle;
		}

		Task IBackButtonHandler.Handle(CancellationToken ct)
		{
			_tcs.TrySetResult(true);

			return Task.CompletedTask;
		}

		/// <summary>
		/// Waits for the <see cref="IBackButtonHandler.Handle(CancellationToken)"/> to be called.
		/// This times out after 100ms.
		/// </summary>
		/// <returns></returns>
		public async Task<bool> WaitForHandling()
		{
			await Task.WhenAny(_tcs.Task, Task.Delay(100));

			if (!_tcs.Task.IsCompleted)
			{
				return false;
			}

			return _tcs.Task.Result;
		}
	}
}
