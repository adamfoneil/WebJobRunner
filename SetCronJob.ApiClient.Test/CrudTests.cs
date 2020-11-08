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
            var token = Config["SetCronJob:ApiToken"];
            var client = new Client(token);
            
            var job = client.CreateJobAsync(new CronJob()
            {
                Name = "whatever",
                Url = "https://myjob.com/whatever",
                Status = JobStatus.Disabled
            }).Result;

            client.DeleteJob(job.Id).Wait();
        }

        private IConfiguration Config => new ConfigurationBuilder().AddJsonFile("Config/config.json").Build();
    }
}
