using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Chinook.BackButtonManager;
using Moq;
using FluentAssertions;
using System.Threading;

namespace BackButtonManager.Tests
{
	public class BackButtonManagerShould
	{
		[Fact]
		public void Be_empty_by_default()
		{
			// Arrange
			var manager = new Chinook.BackButtonManager.BackButtonManager();

			// Assert
			Assert.Empty(manager.Sources);
			Assert.Empty(manager.Handlers);
		}

		[Fact]
		public void Fail_to_add_null_sources()
		{
			// Arrange
			var manager = new Chinook.BackButtonManager.BackButtonManager();

			// Act & Assert
			Assert.Throws<ArgumentNullException>(() => manager.AddSource(null));
		}

		[Fact]
		public void Fail_to_add_null_handlers()
		{
			// Arrange
			var manager = new Chinook.BackButtonManager.BackButtonManager();

			// Act & Assert
			Assert.Throws<ArgumentNullException>(() => manager.AddHandler(null));
		}

		[Fact]
		public void Add_and_remove_handler_using_RegisterHandler()
		{
			// Arrange
			var manager = new Chinook.BackButtonManager.BackButtonManager();
			var handler = new Mock<IBackButtonHandler>().Object;

			// Act
			var disposable = manager.RegisterHandler(handler);

			// Assert
			manager.Handlers.Single().Handler.Should().Be(handler);

			// Act
			disposable.Dispose();

			// Assert
			manager.Handlers.Should().BeEmpty();
		}

		[Fact]
		public void Order_handlers_by_priority()
		{
			// Arrange
			var manager = new Chinook.BackButtonManager.BackButtonManager();
			var handler1 = new Mock<IBackButtonHandler>().Object;
			var handler2 = new Mock<IBackButtonHandler>().Object;
			var handler3 = new Mock<IBackButtonHandler>().Object;

			// Act
			manager.AddHandler(handler1, 10);
			manager.AddHandler(handler2, 100);
			manager.AddHandler(handler3, 1);

			// Assert
			var handlers = manager.Handlers.ToArray();

			handlers[0].Priority.Should().Be(100);
			handlers[0].Handler.Should().Be(handler2);

			handlers[1].Priority.Should().Be(10);
			handlers[1].Handler.Should().Be(handler1);
			
			handlers[2].Priority.Should().Be(1);
			handlers[2].Handler.Should().Be(handler3);
		}

		[Fact]
		public void Auto_assign_priority()
		{
			// Arrange
			var manager = new Chinook.BackButtonManager.BackButtonManager();
			var handler1 = new Mock<IBackButtonHandler>().Object;
			var handler2 = new Mock<IBackButtonHandler>().Object;
			var handler3 = new Mock<IBackButtonHandler>().Object;

			// Act
			manager.AddHandler(handler1);
			manager.AddHandler(handler2);
			manager.AddHandler(handler3);

			// Assert
			var handlers = manager.Handlers.ToArray();

			handlers[0].Priority.Should().Be(Chinook.BackButtonManager.BackButtonManager.DefaultPriorityIncrement * 3);
			handlers[0].Handler.Should().Be(handler3);

			handlers[1].Priority.Should().Be(Chinook.BackButtonManager.BackButtonManager.DefaultPriorityIncrement * 2);
			handlers[1].Handler.Should().Be(handler2);

			handlers[2].Priority.Should().Be(Chinook.BackButtonManager.BackButtonManager.DefaultPriorityIncrement);
			handlers[2].Handler.Should().Be(handler1);
		}

		[Fact]
		public async Task Dispatch_to_the_correct_handler()
		{
			// Arrange
			var manager = new Chinook.BackButtonManager.BackButtonManager();
			var handler1 = new TestBackButtonHandler();
			var handler2 = new TestBackButtonHandler();

			var source = new TestBackButtonSource();

			manager.AddSource(source);
			manager.AddHandler(handler1);

			// Act
			source.RequestBack();
			var calledHandler1 = await handler1.WaitForHandling();

			// Assert
			calledHandler1.Should().BeTrue();

			// Arrange
			manager.AddHandler(handler2);

			// Act
			source.RequestBack();
			var calledHandler2 = await handler2.WaitForHandling();

			// Assert
			calledHandler2.Should().BeTrue();
		}
	}
}
