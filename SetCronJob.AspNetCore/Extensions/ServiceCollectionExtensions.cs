using Microsoft.Extensions.DependencyInjection;
using SetCronJob.ApiClient;

namespace SetCronJob.AspNetCore.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSetCronJob(this IServiceCollection services, string apiToken)
        {
            services.AddSingleton((sp) => new Client(apiToken));
            return services;
        }
    }
}
