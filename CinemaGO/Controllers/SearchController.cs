using CinemaGO.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CinemaGO.Controllers
{
    public class SearchController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Search(string type, string searchbar)
        {
            MovieViewModel movieViewModel = new MovieViewModel();
            movieViewModel.Movies = new List<Movie>();
            movieViewModel.Movies.Add(new Movie());
            movieViewModel.CurrentPage = 11;
            if (type == "movie")
            {
                TempData["title"] = searchbar;
                return RedirectToAction("Index", "Movies");
            }
            return RedirectToAction("Index");
        }
    }
}
