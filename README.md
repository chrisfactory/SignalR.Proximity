# SignalR.Proximity

SignalR.Proximity is a .Net library 
 

## Usage 
Send message
```csharp
using SignalR.Proximity;

var cnx = endPointProvider.Connect<IMyNotificationContract>();
await cnx.StartAsync();

await cnx.Notifier.ToAll().NotifyAsync(t => t.MyNotification("hello word"));
or
await cnx.Notifier.ToGroups("My groupe").NotifyAsync(t => t.MyNotification("hello word"));

```
## Contributing
Working progress..

How to get it
--------------------------------
Use NuGet Package Manager to install the package or use any of the following commands in NuGet Package Manager Console.
 
[![Nuget.org client](http://img.shields.io/nuget/v/SignalR.Proximity.svg)](https://www.nuget.org/packages/SignalR.Proximity/)
```	
PM> Install-Package SignalR.Proximity
```
[![Nuget.org web app](http://img.shields.io/nuget/v/SignalR.Proximity.Hosting.svg)](https://www.nuget.org/packages/SignalR.Proximity.Hosting/)
```	 
PM> Install-Package SignalR.Proximity.Hosting
```
## Status
[![Build status](https://github.com/chrisfactory/SignalR.Proximity/workflows/.NET/badge.svg)](https://github.com/chrisfactory/SignalR.Proximity/actions?query=branch%3Amaster)

[![CodeQL status](https://github.com/chrisfactory/SignalR.Proximity/workflows/CodeQL/badge.svg)](https://github.com/chrisfactory/SignalR.Proximity/actions?query=branch%3Amaster)
## License
[MIT](https://choosealicense.com/licenses/mit/)