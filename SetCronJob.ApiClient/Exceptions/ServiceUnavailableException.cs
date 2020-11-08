using System;

namespace SetCronJob.ApiClient.Exceptions
{
    public class ServiceUnavailableException : Exception
    {
        public ServiceUnavailableException() : base("Too many API calls in too short a time. A one-second delay between calls is recommended.")
        {
        }
    }
}
