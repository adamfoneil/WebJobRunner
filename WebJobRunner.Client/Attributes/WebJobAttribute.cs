using System;

namespace WebJobRunner.Client.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class WebJobAttribute : Attribute
    {
        public WebJobAttribute(string cronJobExpression)
        {
            CronJobExpression = cronJobExpression;
        }

        public string CronJobExpression { get; }
    }
}
