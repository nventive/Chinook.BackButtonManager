﻿using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Core;

namespace Chinook.BackButtonManager
{
	/// <summary>
	/// This is a <see cref="IBackButtonSource"/> implementation wrapping <see cref="SystemNavigationManager.BackRequested"/>.
	/// </summary>
	public class SystemNavigationBackButtonSource : IBackButtonSource
	{
		/// <summary>
		/// Creates a new instance of <see cref="SystemNavigationBackButtonSource"/>.
		/// You must invoke this on the main thread.
		/// </summary>
		public SystemNavigationBackButtonSource()
		{
			// Must run on UI thread.
			SystemNavigationManager.GetForCurrentView().BackRequested += OnNativeBackRequested;
		}

		private void OnNativeBackRequested(object sender, Windows.UI.Core.BackRequestedEventArgs e)
		{
			var args = new BackRequestedEventArgs();
			BackRequested?.Invoke(this, args);
			e.Handled = args.Handled;
		}

		/// <inheritdoc/>
		public string Name { get; } = "SystemNavigationBackButton";

		/// <inheritdoc/>
		public event BackRequestedEventHandler BackRequested;

		/// <inheritdoc/>
		public void Dispose()
		{
			BackRequested = null;

			// Must run on UI thread.
			SystemNavigationManager.GetForCurrentView().BackRequested -= OnNativeBackRequested;
		}
	}
}
