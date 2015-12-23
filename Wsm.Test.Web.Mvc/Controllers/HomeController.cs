﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wsm.Contracts.Database;
using Wsm.Contracts.Logger;
using Wsm.Contracts.Models;

namespace Wsm.Test.Web.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDataBaseEntryPoint _dataBaseEntryPoint;
        private readonly ILogger _logger;

        public HomeController(IDataBaseEntryPoint dataBaseEntryPoint, ILogger logger)
        {
            _dataBaseEntryPoint = dataBaseEntryPoint;
            _logger = logger;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            var userRepo = _dataBaseEntryPoint.RepositoryFactory.UserRepository;

            userRepo.Create(new User
            {
                id = new Guid(),
                Username = "Wiljan",
                LastName = "Ruizendaal",
                Email = "wruizendaal@gmail.com"
            });

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}