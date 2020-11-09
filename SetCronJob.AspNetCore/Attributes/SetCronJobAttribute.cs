using System;

namespace SetCronJob.AspNetCore.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class SetCronJobAttribute : Attribute
    {
        public SetCronJobAttribute(string cronExpression)
        {
            CronExpression = cronExpression;
        }

        public string CronExpression { get; }

        public string AppendQueryString { get; set; }
    }
}
