using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SetCronJob.ApiClient.Models;

namespace SetCronJob.ApiClient.Test
{
    [TestClass]
    public class CrudTests
    {
        [TestMethod]
        public void CreateAndDeleteJob()
        {
            var token = Config["SetCronJob:ApiKey"];
            var client = new Client(token);
            
            var job = client.CreateJobAsync(new CronJob()
            {
                Name = "whatever",
                Url = "https://myjob.com/whatever"
            }).Result;

            client.DeleteJob(job.Id).Wait();
        }

        private IConfiguration Config => new ConfigurationBuilder().AddJsonFile("Config/config.json").Build();
    }
}
