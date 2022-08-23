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

		/// <summary>
		/// Gets the wrapped handler.
		/// </summary>
		public IBackButtonHandler Handler { get; }

		/// <summary>
		/// Gets the priority.
		/// </summary>
		public int Priority { get; }
	}
}
