using CinemaGO.Data;
using CinemaGO.Data.Services;
using CinemaGO.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaGO.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMovieService _service;

        public HomeController(ILogger<HomeController> logger, IMovieService service)
        {
            _logger = logger;
            _service = service;
        }

        public IActionResult Index()
        {
            var movies = _service.GetAll();
            return View(movies);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult AddActor()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
