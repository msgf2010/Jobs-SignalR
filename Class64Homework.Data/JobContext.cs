using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Class64Homework.Data
{
    public class JobContext : DbContext
    {
        private string _connectionString;

        public JobContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DbSet<Job> Jobs { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
