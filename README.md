# Introduction 
BackButtonManager allows you to customize how your app reacts to the back button being pressed.

# Getting Started

## Uno projects

BackButtonManager is especially well-integrated with [Uno](https://platform.uno/). Here is how to use it in a project which includes the Uno platform:

* Add to your project the `Chinook.BackButtonManager.Uno` nuget package and its dependencies
* Create a single instance of a `BackButtonManager` which you will use throughout your project.

```
var manager = new BackButtonManager();
```

* In your app's Startup, add the source which BackButtonManager uses to detect back button presses:

```
// This must be executed on the dispatcher
var source = new SystemNavigationBackButtonSource();
backButtonManager.AddSource(source);
```

* Add handlers for each action you want to take when the back button is pressed:

```
manager.AddHandler(new BackButtonHandler(
	name: "TODO handler name",
	canHandle: () => CanYourMethodBeCalled(),
	handle: async ct => await YourMethod(ct)
));
```

## Other projects

If your project does not use Uno, you can certainly use BackButtonManager! Here's how:

* Add to your project the `Chinook.BackButtonManager` nuget package and its dependencies
* Create a single instance of a `BackButtonManager` which you will use throughout your project.

```
var manager = new BackButtonManager();
```

* You will need to create a source which implements the `IBackButtonSource` interface. In your app's Startup, add this source so that BackButtonManager can use it to detect back button presses. 

```
// This must be executed on the dispatcher
var source = new MyBackButtonSource();
backButtonManager.AddSource(source);
```

* Add handlers for each action you want to take when the back button is pressed:

```
manager.AddHandler(new BackButtonHandler(
  name: "TODO handler name",
  canHandle: () => CanYourMethodBeCalled(),
  handle: async ct => await YourMethod(ct)
));
```

## Contributing

Please read [CONTRIBUTING.md](CONTRIBUTING.md) for details on the process for
contributing to this project.

Be mindful of our [Code of Conduct](CODE_OF_CONDUCT.md).