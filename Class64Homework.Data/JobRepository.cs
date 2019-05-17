using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Class64Homework.Data
{
    public class JobRepository
    {
        private string _connectionString;

        public JobRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Job> GetJobs()
        {
            using (var context = new JobContext(_connectionString))
            {
                return context.Jobs.Include(u => u.User).ToList();
            }
        }

        public void AddJob(Job job)
        {
            using (var context = new JobContext(_connectionString))
            {
                context.Jobs.Add(job);
                context.SaveChanges();
            }
        }

        public void UpdateJob(Job job)
        {
            using (var context = new JobContext(_connectionString))
            {
                context.Database.ExecuteSqlCommand(
                    "UPDATE Jobs SET UserId = @userId, JobStatus = @jobStatus WHERE Id = @id",
                    new SqlParameter("@userId", job.User.Id),
                    new SqlParameter("@id", job.Id),
                    new SqlParameter("@jobStatus", job.JobStatus)
                    );
            }
        }
    }
}