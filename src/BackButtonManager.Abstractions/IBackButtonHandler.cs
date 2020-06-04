using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chinook.BackButtonManager
{
	public interface IBackButtonHandler
	{
		/// <summary>
		/// Gets the name of this <see cref="IBackButtonHandler"/>.
		/// </summary>
		string Name { get; }

		/// <summary>
		/// Gets whether this <see cref="IBackButtonHandler"/> can handle a back request right now.
		/// </summary>
		/// <returns>True if this <see cref="IBackButtonHandler"/> can handle the back request. False otherwise.</returns>
		bool CanHandle();

		/// <summary>
		/// The method to execute when this <see cref="IBackButtonHandler"/> should handle the back request.
		/// </summary>
		/// <param name="ct"></param>
		/// <returns></returns>
		Task Handle(CancellationToken ct);
	}
}
