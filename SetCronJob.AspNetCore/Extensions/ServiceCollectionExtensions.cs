using Microsoft.Extensions.DependencyInjection;
using SetCronJob.ApiClient;

namespace SetCronJob.AspNetCore.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSetCronJobClient(this IServiceCollection services, string apiToken)
        {
            services.AddSingleton<ISetCronJobClient>(new Client(apiToken));
            return services;
        }
    }
}
