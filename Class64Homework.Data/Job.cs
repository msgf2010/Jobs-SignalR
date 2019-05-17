using System;
using System.Collections.Generic;
using System.Text;

namespace Class64Homework.Data
{
    public class Job
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public JobStatus JobStatus { get; set; }
        public int? UserId { get; set; }
        public User User { get; set; }
    }
}