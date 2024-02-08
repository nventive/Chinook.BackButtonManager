using Microsoft.UI.Input;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Input;

namespace Chinook.BackButtonManager
{
	/// <summary>
	/// This is a <see cref="IBackButtonSource"/> implementation using the XButton1 of a pointer device.
	/// This uses the <see cref="UIElement.PointerReleased"/> event and checks for <see cref="PointerPointProperties.PointerUpdateKind"/> to be <see cref="PointerUpdateKind.XButton1Released"/>.
	/// </summary>
	public sealed class PointerXButton1BackButtonSource : IBackButtonSource
	{
		private readonly UIElement _root;

		/// <summary>
		/// Creates a new instance of <see cref="PointerXButton1BackButtonSource"/>.
		/// You must invoke this on the main thread.
		/// </summary>
		/// <param name="windowContent">The content of your window on which the <see cref="UIElement.PointerReleased"/> event will be subscribed to.</param>
		public PointerXButton1BackButtonSource(UIElement windowContent)
		{
			_root = windowContent;
			_root.PointerReleased += OnPointerReleased;
		}

		private void OnPointerReleased(object sender, PointerRoutedEventArgs e)
		{
			var pointerPoint = e.GetCurrentPoint(_root);
			var pointerProperties = pointerPoint.Properties;

			if (pointerProperties.PointerUpdateKind == PointerUpdateKind.XButton1Released)
			{
				BackRequested?.Invoke(this, new BackRequestedEventArgs());
			}
		}

		/// <inheritdoc/>
		public string Name { get; } = "PointerXButton1BackButtonSource";

		/// <inheritdoc/>
		public event BackRequestedEventHandler BackRequested;

		/// <inheritdoc/>
		public void Dispose()
		{
			_root.PointerReleased -= OnPointerReleased;
		}
	}
}
