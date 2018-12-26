# Quartz Admin Core

Quartz Admin allows you to fire up a UI web interface to manage your Quartz scheduler. It's plug and play all you have to do is pass it on your scheduler instance and it will pull all  your jobs from it.

- View all the jobs together with the schedule
- Execute jobs on demand
- List triggers

Some more features coming soon.

## Installation

QuartzAdmin is available via NuGet:

`Install-Package QuartzAdmin`

Or

`dotnet add package QuartzAdmin`


## Setup

After adding the NuGet package you now need to bootstrap the API and the UI. Typically you will be hosting your Quartz scheduler either in a Console Application or a Windows Service, in either case you can start the QuartzAdmin API in your `Program.cs`:


```csharp

//Set up your scheulder instance here so you can pass it on to QuartzAdmin

// Initialize QuartzAdmin endpoint
QuartzApiCore.API.QuartzApiCore.Start(scheduler);

//Now you can open http://localhost:5000/Admin
```


For more information see the sample app in the `src` directory.


