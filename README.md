The ultimate goal for this project is to provide a really easy way to create MVC actions to be invoked by a cron job service. Let's say I have an action I want called every day at 6AM. I'd like this to be as easy as adding an attribute to the action with a cron expression:

```csharp
public class DashboardController : Controller
{
    [SetCronJob("0 6 * * *")]
    public async Task<IActionResult> Refresh()
    {
        // do something useful
    }
}
```
There are a number of cron job services out there. I went looking, and found [SetCronJob](https://www.setcronjob.com/). I liked their price and straightforward presentation. They also have an API, which is important for my approach. In order to wire up MVC actions with a cron job service, I need something to find endpoints in my MVC app that have the `[SetCronJob]` attribute and create cron jobs in a backend service accordingly.

I'm still working on the endpoint discovery piece, but I do have a minimal API client working with SetCronJob:

[![Nuget](https://img.shields.io/nuget/v/SetCronJob.ApiClient)](https://www.nuget.org/packages/SetCronJob.ApiClient/)

This package has a `Client` object with this [interface](https://github.com/adamfoneil/WebJobRunner/blob/master/SetCronJob.ApiClient/ISetCronJobClient.cs)

This is a work in progress.
