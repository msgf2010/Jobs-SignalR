using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Class64Homework.Data;

namespace Class64Homework.Web.Models
{
    public class JobsModelView
    {
        public IEnumerable<Job> Jobs { get; set; }
        public int CurrentUserId { get; set; }
    }
}