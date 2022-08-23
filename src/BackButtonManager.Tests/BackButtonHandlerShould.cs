using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Chinook.BackButtonManager;
using Xunit;

namespace BackButtonManager.Tests
{
	public class BackButtonHandlerShould
	{
		static BackButtonHandlerShould()
		{
			Func<bool> canHandle = () => true;
			Func<CancellationToken, Task> handle = ct => Task.CompletedTask;

			InvalidCtorParameters = new object[][]
			{
				new object[] { null, canHandle, handle },
				new object[] { string.Empty, canHandle, handle },
				new object[] { "name", null, handle },
				new object[] { "name", canHandle, null },
			};
		}
		public static IEnumerable<object[]> InvalidCtorParameters { get; }

		[Theory]
		[MemberData(nameof(InvalidCtorParameters))]
		public void Throw_when_created_invalid_parameters(string name, Func<bool> canHandle, Func<CancellationToken, Task> handle)
		{
			// Act & Assert
			Assert.ThrowsAny<ArgumentException>(() => new BackButtonHandler(name, canHandle, handle));
		}

		[Fact]
		public void Rely_on_func_value_for_CanHandle()
		{
			// Arrange
			var canHandle = false;
			var handler = new BackButtonHandler("test", () => canHandle, ct => Task.CompletedTask);

			// Act & Assert
			Assert.False(handler.CanHandle());

			canHandle = true;
			Assert.True(handler.CanHandle());
		}

		[Fact]
		public async Task Invoke_the_provided_Handle_function()
		{
			// Arrange
			var invoked = false;
			var handler = new BackButtonHandler("test", () => true, Handle);

			// Act
			await handler.Handle(CancellationToken.None);

			// Assert
			Assert.True(invoked);

			Task Handle(CancellationToken ct)
			{
				invoked = true;
				return Task.CompletedTask;
			}
		}
	}
}
