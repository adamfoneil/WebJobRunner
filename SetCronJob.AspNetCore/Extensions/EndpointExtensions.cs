using Microsoft.AspNetCore.Routing;
using SetCronJob.ApiClient;
using SetCronJob.ApiClient.Models;
using System.Linq;

namespace SetCronJob.AspNetCore.Extensions
{
    public static class EndpointExtensions
    {
        /// <summary>
        /// thanks to https://github.com/kobake/AspNetCore.RouteAnalyzer/issues/28
        /// for the idea
        /// </summary>        
        public static void CreateCronJobs(
            this IEndpointRouteBuilder endpoints, 
            ISetCronJobClient cronJobClient,
            string baseUrl)
        {
            var cronJobEndPoints = endpoints
                .DataSources
                .SelectMany(dataSource => dataSource
                    .Endpoints.OfType<RouteEndpoint>()
                    .Select(ep => new
                    {
                        Endpoint = ep,
                        CronJobs = ep.Metadata.OfType<SetCronJobAttribute>()
                    }).Where(jobs => jobs.CronJobs.Any()));

            foreach (var ep in cronJobEndPoints)
            {
                // just spitballing here
                cronJobClient.CreateJobAsync(new CronJob()
                {
                    Url = baseUrl + ep.Endpoint.RoutePattern.RawText,
                    Name = ep.Endpoint.DisplayName
                }).Wait();
            }
        }
    }
}
