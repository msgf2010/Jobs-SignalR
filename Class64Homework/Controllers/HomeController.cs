﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Class64Homework.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Class64Homework.Data;

//Create an application that enabled multiple users to add and complete tasks.
//This application will have the standard login/signup and the home page should only be accessible to logged in users.

//On the home page, there should be a textbox and a button to add a new task.
//Beneath that, there should be a table with all the current outstanding(incomplete) tasks.

//If a given task has not been started by anyone yet, there should be a button next to the task(on the table) that says "I'm doing this one".
//When clicked, the button should change to "I'm done", however, only for THAT user.
//All other users should see a disabled button that says "{name of user} is doing this". 

//When the user that chose a task clicks on the "I'm done" button, that task should disappear from the table, for ALL users.

//(Use Entity Framework and SignalR)

namespace Class64Homework.Web.Controllers
{
    public class HomeController : Controller
    {
        private string _connectionString;

        public HomeController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }

        [Authorize]
        public IActionResult Index()
        {
            var authDb = new Authentication(_connectionString);
            var user = authDb.GetByEmail(User.Identity.Name);

            var repo = new JobRepository(_connectionString);

            JobsModelView mv = new JobsModelView();
            mv.Jobs = repo.GetJobs();
            mv.CurrentUserId = user.Id;

            return View(mv);
        }
    }
}
