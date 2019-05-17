using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Class64Homework.Data;

namespace Class64Homework.Web
{
    public class JobHub : Hub
    {
        private string _connectionString;

        public JobHub(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }

        public void NewJob(Job job)
        {
            var repo = new JobRepository(_connectionString);
            job.JobStatus = JobStatus.Open;
            repo.AddJob(job);

            Clients.All.SendAsync("NewJob", job);
        }

        public void UpdateJob(Job job)
        {
            var authDb = new Authentication(_connectionString);
            var user = authDb.GetByEmail(Context.User.Identity.Name);
            
            if (job.User == null)
            {
                job.User = user;
            }

            var repo = new JobRepository(_connectionString);
            repo.UpdateJob(job);

            if (job.JobStatus == JobStatus.BeingWorkedOn)
            {
                Clients.All.SendAsync("JobInUse", job);
            }
            else if (job.JobStatus == JobStatus.Done)
            {
                Clients.All.SendAsync("JobDone", job);
            }
        }
    }
}
