# Introduction 
<!-- ALL-CONTRIBUTORS-BADGE:START - Do not remove or modify this section -->
[![All Contributors](https://img.shields.io/badge/all_contributors-1-orange.svg?style=flat-square)](#contributors-)
<!-- ALL-CONTRIBUTORS-BADGE:END -->
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

# Contribute
TODO: Explain how other users and developers can contribute to make your code better. 

If you want to learn more about creating good readme files then refer the following [guidelines](https://docs.microsoft.com/en-us/azure/devops/repos/git/create-a-readme?view=azure-devops). You can also seek inspiration from the below readme files:
- [ASP.NET Core](https://github.com/aspnet/Home)
- [Visual Studio Code](https://github.com/Microsoft/vscode)
- [Chakra Core](https://github.com/Microsoft/ChakraCore)

# Contributors



## Contributors ✨

Thanks goes to these wonderful people ([emoji key](https://allcontributors.org/docs/en/emoji-key)):

<!-- ALL-CONTRIBUTORS-LIST:START - Do not remove or modify this section -->
<!-- prettier-ignore-start -->
<!-- markdownlint-disable -->
<table>
  <tr>
    <td align="center"><a href="https://github.com/jeanplevesque"><img src="https://avatars3.githubusercontent.com/u/39710855?v=4" width="100px;" alt=""/><br /><sub><b>Jean-Philippe Lévesque</b></sub></a><br /><a href="https://github.com/nventive/Chinook.BackButtonManager/commits?author=jeanplevesque" title="Code">💻</a></td>
  </tr>
</table>

<!-- markdownlint-enable -->
<!-- prettier-ignore-end -->
<!-- ALL-CONTRIBUTORS-LIST:END -->

This project follows the [all-contributors](https://github.com/all-contributors/all-contributors) specification. Contributions of any kind welcome!
