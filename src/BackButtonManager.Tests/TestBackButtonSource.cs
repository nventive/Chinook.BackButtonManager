using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chinook.BackButtonManager;

namespace BackButtonManager.Tests
{
	public class TestBackButtonSource : IBackButtonSource
	{
		public string Name => nameof(TestBackButtonSource);

		public event BackRequestedEventHandler BackRequested;

		public void RequestBack()
		{
			BackRequested?.Invoke(this, new BackRequestedEventArgs());
		}

		public void Dispose()
		{
			BackRequested = null;
		}
	}
}
