﻿using CinemaGO.Data.Enums;
using CinemaGO.Data.Services;
using CinemaGO.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            var title = TempData["title"];
            IEnumerable<Movie> movies;
            if (title != null)
            {
                movies = _service.GetMovieByTitle(title.ToString());
            }
            else
			{
                movies = _service.GetAll();
            }

            MovieViewModel model = new MovieViewModel();
            var request = Request.Path.ToString().Split('/', StringSplitOptions.RemoveEmptyEntries);
            int page = 1;
            if (request.Length > 2)
            {
                page = int.Parse(request[2]);
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

            model.Movies = movies.ToList();


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
                                    string genre, 
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
            Genres parsedGenre = (Genres)Enum.Parse(typeof(Genres), genre);
            Movie movie = new Movie(title, parsedGenre,releaseDate, parsedDuration, description,coverURL,bgURL,image1,image2,image3,image4);
            _service.Add(movie);
            return RedirectToAction("Index");
        }

        //public IActionResult AddMovie()
        //{
        //    var title = Request.Form["title"].ToString();
        //    var firstName = Request.Form["firstName"].ToString();
        //    var lastName = Request.Form["lastName"].ToString();
        //    var producerName = firstName + " " + lastName;
        //    Genres genre = (Genres)Enum.Parse(typeof(Genres), Request.Form["genre"].ToString());

        //    var coverURL = Request.Form["coverURL"].ToString();
        //    var price = double.Parse(Request.Form["price"].ToString());
        //    var description = Request.Form["description"].ToString();
        //    var startDate = DateTime.Parse(Request.Form["startDate"].ToString());
        //    var endDate = DateTime.Parse(Request.Form["endDate"].ToString());

        //    var movie = new Movie(title, description, startDate, endDate, price, genre, coverURL);
        //    _service.Add(movie);

        //    return RedirectToAction("Index");
        //}
        //[HttpPost]
        //public IActionResult AddMovie(string firstName)
        //{
        //    var title = Request.Form["title"].ToString();
        //    return RedirectToAction("Index");
        //}


    }
}
