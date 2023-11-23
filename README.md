# Chinook.BackButtonManager 

[![License](https://img.shields.io/badge/License-Apache%202.0-blue.svg?style=flat-square)](LICENSE) ![Version](https://img.shields.io/nuget/v/Chinook.BackButtonManager.Abstractions?style=flat-square) ![Downloads](https://img.shields.io/nuget/dt/Chinook.BackButtonManager.Abstractions?style=flat-square)

The `Chinook.BackButtonManager` packages provide recipes to ease the handling of back buttons in .Net applications. It was designed for MVVM applications, but should work with other patterns too.

## Cornerstones

- **Highly Extensible**
  - Everything is interface-based to easily allow more implementations.
  - A single framework can't cover everything. Our architecture is designed in a way that allows you to extend this foundation to support more use-cases.
- **Simple**
  - The recipes from these packages are ultra-simple but are still complete enough to support edge cases.

### More like this
The Chinook namespace has other recipes for .Net MVVM applications.
- [Chinook.DynamicMvvm](https://github.com/nventive/Chinook.DynamicMvvm): MVVM libraries for extensible and declarative ViewModels.
- [Chinook.Navigation](https://github.com/nventive/Chinook.Navigation): Navigators for ViewModel-first navigation.
- [Chinook.DataLoader](https://github.com/nventive/Chinook.DataLoader): Customizable async data loading recipes.

## Getting Started

### Uno projects

BackButtonManager is especially well-integrated with [Uno](https://platform.uno/). Here is how to use it in a project which includes the Uno platform:

1. Add a package reference to `Chinook.BackButtonManager.Uno.WinUI` (for WinUI or MAUI apps).
1. Create a single instance of a `BackButtonManager` which you will use throughout your project.
   ```csharp
   var manager = new BackButtonManager();
   ```

1. In your app's Startup, add the source that the manager uses to detect back button presses:
   ```csharp
   // This must be executed on the dispatcher
   var source = new SystemNavigationBackButtonSource();
   manager.AddSource(source);
   ```

   > ❗ Note that `SystemNavigationManager` no longer works on Windows applications that are not UWP. On WinUI, you can no longer have a global back button in the title bar of the application.

1. Add handlers for each action you want to take when the back button is pressed:
   ```csharp
   manager.AddHandler(new BackButtonHandler(
	   name: "TODO handler name",
   	canHandle: () => CanYourMethodBeCalled(),
   	handle: async ct => await YourMethod(ct)
   ));
   ```

### Other projects

If your project does not use Uno, you can certainly use BackButtonManager! Here's how:

1. Add a package reference to `Chinook.BackButtonManager`.
1. Create a single instance of a `BackButtonManager` which you will use throughout your project.

   ```csharp
   var manager = new BackButtonManager();
   ```

1. You will need to create a source which implements the `IBackButtonSource` interface. In your app's Startup, add this source so that BackButtonManager can use it to detect back button presses. 

   ```csharp
   var source = new MyBackButtonSource();
   manager.AddSource(source);
   ```

1. Add handlers for each action you want to take when the back button is pressed:

   ```csharp
   manager.AddHandler(new BackButtonHandler(
     name: "TODO handler name",
     canHandle: () => CanYourMethodBeCalled(),
     handle: async ct => await YourMethod(ct)
   ));
   ```

## Features
### Create back button sources
Using `IBackButtonSource`, you can implement a back button source. You can see that as an abstraction of a button. You could create sources for things like the following.
- The hardware back button on Android.
- The escape key from your keyboard.
- The back button from your mouse.

Once you have a back button source, simply add it to a `IBackButtonManager` using `AddSource`.

#### Create a custom back button source
The interface `IBackButtonSource` is very simple. You can implement your own sources easily.

### Create back button handlers
Using `IBackButtonHandler`, you can create the objects that react to back requests. Handlers can be added to (or removed from) a `IBackButtonManager` at any point.

#### Create global handlers
That's one of the main use-case of this recipe. You likely want to create a default action to perform when a back is requested.

Here is some code showing how to setup a `IBackButtonManager` with a default handler in a context using Microsoft.Extensions.DependencyInjection and [Chinook.Navigation](https://github.com/nventive/Chinook.Navigation).
```csharp
public static IServiceCollection AddDefaultBackHandler(this IServiceCollection services)
{
  return services
    .AddSingleton<IBackButtonManager>(s =>
    {
      var manager = new BackButtonManager();

      var sectionsNavigator = s.GetRequiredService<ISectionsNavigator>();
      manager.AddHandler(new BackButtonHandler(
        name: "DefaultSectionsNavigatorHandler",
        canHandle: () => sectionsNavigator.CanNavigateBackOrCloseModal(),
        handle: async ct => await sectionsNavigator.NavigateBackOrCloseModal(ct)));

      return manager;
    });
}
```

#### Create temporary handlers
This can be useful when your page has advances states or secondary views. Imagine having a drawer or side menu. When that secondary view is active, it is likely that you want your back button to dismiss that secondary view rather than navigating to the previous page.

```csharp
public MainPageViewModel(IBackButtonManager manager)
{
	var customHandler = new BackButtonHandler("CustomBackHandler",
		// The handler will only be invoked when the side panel is open.
		canHandle: () => IsSidePanelOpen,
		
		// Close the side panel when a back is requested.
		handle: async ct => IsSidePanelOpen = false
	);
	var subscription = manager.RegisterHandler(customHandler);

	// Automatically remove the handler when this page gets disposed.
	this.AddDisposable(subscription);
}

public bool IsSidePanelOpen
{
	get => this.Get<bool>(initialValue: false);
	set => this.Set(value);
}
```
> 💡 This sample shows a ViewModel written using [Chinook.DynamicMvvm](https://github.com/nventive/Chinook.DynamicMvvm).

#### Specify an handler's priority
It is possible to specify a priority when calling `IBackButtonManager.AddHandler`. The highest priority handlers will be evaluated first.

## Breaking Changes

Please consult [BREAKING_CHANGES.md](BREAKING_CHANGES.md) for more information about migration.

## License

This project is licensed under the Apache 2.0 license - see the
[LICENSE](LICENSE) file for details.

## Contributing

Please read [CONTRIBUTING.md](CONTRIBUTING.md) for details on the process for
contributing to this project.

Be mindful of our [Code of Conduct](CODE_OF_CONDUCT.md).

## Legacy

#### Create a source from `SystemNavigationManager`
This can be useful for UWP or Uno.UI applications. The source is based on the `SystemNavigationManager.BackRequested` event.
```csharp
// This must be executed on the dispatcher
var source = new SystemNavigationBackButtonSource();
```