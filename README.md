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
There are a number of cron job services out there. I went looking, and found [SetCronJob](https://www.setcronjob.com/). I liked their price and clean presentation. They also have an API, which is important for my approach.

