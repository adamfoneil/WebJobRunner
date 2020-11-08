using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SetCronJob.ApiClient.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SetCronJob.ApiClient.Test
{
    [TestClass]
    public class CrudTests
    {
        [TestMethod]
        public void CreateAndDeleteJob()
        {
            var client = GetClient();            
            var job = client.CreateJobAsync(SampleJob()).Result;
            client.DeleteJobAsync(job.Id).Wait();
        }

        [TestMethod]
        public void ListJobs()
        {
            var client = GetClient();
            Enumerable.Range(1, 5).ToList().ForEach(i =>
            {
                Task.Delay(1000).Wait();
                client.CreateJobAsync(SampleJob(i)).Wait();
            });

            var jobs = client.ListJobsAsync().Result;

            Assert.IsTrue(jobs.Any());

            foreach (var job in jobs)
            {
                Task.Delay(1000).Wait(); // you need a little delay here to prevent "service unavailable" errors
                client.DeleteJobAsync(job.Id).Wait();                
            }
        }

        [TestMethod]
        public void DeleteJobByName()
        {
            var client = GetClient();
            var job = client.CreateJobAsync(SampleJob()).Result;
            client.DeleteJobAsync(job.Name).Wait();
        }

        [TestMethod]
        public void EditJob()
        {
            var client = GetClient();
            var job = client.CreateJobAsync(SampleJob()).Result;

            int id = job.Id;
            Task.Delay(1000).Wait();

            const string newUrl = "https://updated.com/whatever1";
           
            job.Url = newUrl;
            job = client.UpdateJobAsync(job).Result;

            // url should be the new one
            Assert.IsTrue(job.Url.Equals(newUrl));

            // make sure job Id didn't change
            Assert.IsTrue(job.Id == id);

            client.DeleteJobAsync(id).Wait();
        }

        private CronJob SampleJob(int index = 1) => new CronJob()
        {
            Name = $"whatever{index}",
            Url = $"https://myjob.com/whatever{index}",
            Status = JobStatus.Disabled
        };

        private Client GetClient()
        {
            var token = Config["SetCronJob:ApiToken"];
            return new Client(token);
        }

        private IConfiguration Config => new ConfigurationBuilder().AddJsonFile("Config/config.json").Build();
    }
}
