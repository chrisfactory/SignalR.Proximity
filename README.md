# SignalR.Proximity

SignalR.Proximity is a .Net library 
 

## Usage 
Instantiate connexions
```csharp
using SignalR.Proximity;

var cnx = endPointProvider.Connect<IMyNotificationContract>();
await cnx.StartAsync();
```
Send messages
```csharp
await cnx.Notifier.ToAll().NotifyAsync(t=> t.MyNotification("hello word"));
//or
await cnx.Notifier
         //Define here your target scope
         .ToGroups("My groupe","Auther Groupe","...") 
         //'t' is an proxy instance of IMyNotificationContract 
         .NotifyAsync(t => t.MyNotification("hello word"));
```
Receive messages
```csharp
using SignalR.Proximity;

public class SampleClass : IMyNotificationContract
{
  ...
  cnx.Client.Attach(this);
  await cnx.Client.JoinGroupsAsync("My groupe");
  ..
  public void MyNotification(string message)
  {
      //Receive message callback here!
  }
}
```
Go to [samples](https://github.com/chrisfactory/SignalR.Proximity/tree/master/sample) for more details..
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
[![CI status](https://github.com/chrisfactory/SignalR.Proximity/workflows/CI/badge.svg)](https://github.com/chrisfactory/SignalR.Proximity/actions/workflows/ci-build-analysis.yml?query=branch%3Amaster)

[![Publish status](https://github.com/chrisfactory/SignalR.Proximity/workflows/publish-nuget/badge.svg)](https://github.com/chrisfactory/SignalR.Proximity/actions/workflows/release.yml)
## License
[MIT](https://choosealicense.com/licenses/mit/)