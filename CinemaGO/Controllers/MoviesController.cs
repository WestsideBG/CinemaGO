using CinemaGO.Data.Enums;
using CinemaGO.Data.Services;
using CinemaGO.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using MySqlX.XDevAPI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace CinemaGO.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovieService _service;

        public MoviesController(IMovieService service)
        {
            _service = service;
        }

		public IActionResult Index()
        {
            var moviesTemp = TempDataExtensions.Get<MovieViewModel>(TempData, "Model");
            object order = TempData["Order"];
            string orderBy = "";
            TempData["order"] = null;
            if (order != null)
            {
                orderBy = order.ToString();
            }
            //moviesTemp = null;

            IEnumerable<Movie> movies = null;
            MovieViewModel model = new MovieViewModel();
            if (moviesTemp != null)
            {
                if (orderBy == "Desc")
                {
                    model.Movies = moviesTemp.Movies.OrderByDescending(m => m.Title).ToList();
                }
                else
                {
                    model.Movies = moviesTemp.Movies.OrderBy(m => m.Title).ToList();
                }
            }
            else
            {
                if (orderBy == "Desc")
                {
                    model.Movies = _service.GetAll().OrderByDescending(m => m.Title).ToList();
                }
                else
                {
                    model.Movies = _service.GetAll().OrderBy(m => m.Title).ToList();
                }
            }
             
            var request = Request.Path.ToString().Split('/', StringSplitOptions.RemoveEmptyEntries);
            int page = 1;
            if (request.Length > 2)
            {
                bool isCompleted;
                isCompleted = int.TryParse(request[2], out page);

                if (!isCompleted)
				{
                    return RedirectToAction("NotFound", "Error");
				}
            }
            
            model.CurrentPage = page;
            if (page > 1)
            {
                model.MovieIndex = (page * 10) - 10;
                model.MovieEndIndex = model.MovieIndex + 10;
            }
            else
            {
                model.MovieIndex = 0;
                model.MovieEndIndex = 10;
            }


            return View(model);
        }

        public IActionResult IndexGrid()
        {
            object order = TempData["Order"];
            string orderBy = "";
            TempData["order"] = null;
            if (order != null)
            {
                orderBy = order.ToString();
            }

            MovieViewModel model = new MovieViewModel();
            var moviesTemp = TempDataExtensions.Get<MovieViewModel>(TempData, "Model");
            if (moviesTemp != null)
            {
                if (orderBy == "Desc")
                {
                    model.Movies = moviesTemp.Movies.OrderByDescending(m => m.Title).ToList();
                }
                else
                {
                    model.Movies = moviesTemp.Movies.OrderBy(m => m.Title).ToList();
                }
            }
            else
            {
                if (orderBy == "Desc")
                {
                    model.Movies = _service.GetAll().OrderByDescending(m => m.Title).ToList();
                }
                else
                {
                    model.Movies = _service.GetAll().OrderBy(m => m.Title).ToList();
                }
            }

            var request = Request.Path.ToString().Split('/', StringSplitOptions.RemoveEmptyEntries);
            int page = 1;
            if (request.Length > 2)
            {
                bool isCompleted;
                isCompleted = int.TryParse(request[2], out page);

                if (!isCompleted)
                {
                    return RedirectToAction("NotFound", "Error");
                }
            }

            model.CurrentPage = page;
            if (page > 1)
            {
                model.MovieIndex = (page * 10) - 10;
                model.MovieEndIndex = model.MovieIndex + 10;
            }
            else
            {
                model.MovieIndex = 0;
                model.MovieEndIndex = 10;
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

  //      public IActionResult Search(string searchbar)
		//{
  //          var movies = _service.GetMovieByTitle(searchbar);
  //          MovieViewModel model = new MovieViewModel();
  //          model.Movies = movies;
  //          model.CurrentPage = 1;
  //          model.MovieIndex = 0;
  //          model.MovieEndIndex = 10;
  //          return Index(model);
		//}

        [HttpPost]
        public IActionResult Create(string title,
                                    string[] genres,
                                    string release, 
                                    int durationMins,
                                    string description, 
                                    string coverURL, 
                                    string bgURL, 
                                    string image1 = "defaultImage", 
                                    string image2 = "defaultImage",
                                    string image3 = "defaultImage",
                                    string image4 = "defaultImage")
        {
            TimeSpan ts = TimeSpan.FromMinutes(durationMins);
            string parsedDuration = string.Format("{0:00}:{1:00}", (int)ts.TotalHours, ts.Minutes);
            DateTime releaseDate = DateTime.Parse(release);
            string genresJSON = JsonConvert.SerializeObject(genres);
            Movie movie = new Movie(title, genresJSON, releaseDate, parsedDuration, description,coverURL,bgURL,image1,image2,image3,image4);
            _service.Add(movie);
            return RedirectToAction("Index");
        }

        public IActionResult OrderBy(string order)
        {
            if (order == "asc")
            {
                TempData["Order"] = "Asc";
            }
            else if (order == "desc")
            {
                TempData["Order"] = "Desc";
            }

            return RedirectToAction("Index");
        }

        public IActionResult OrderByGrid(string order)
        {
            if (order == "asc")
            {
                TempData["Order"] = "Asc";
            }
            else if (order == "desc")
            {
                TempData["Order"] = "Desc";
            }

            return RedirectToAction("IndexGrid");
        }
    }
}
