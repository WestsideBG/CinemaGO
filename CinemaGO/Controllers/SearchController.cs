using CinemaGO.Data.Services;
using CinemaGO.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace CinemaGO.Controllers
{
    public class SearchController : Controller
    {
        private readonly IMovieService _service;

        public SearchController(IMovieService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Search(string type, string searchbar)
        {
            MovieViewModel movieViewModel = new MovieViewModel();

            if (type == "movie")
            {
                movieViewModel.Movies = _service.GetMovieByTitle(searchbar);
                TempDataExtensions.Put(TempData, "Model", movieViewModel);
                return RedirectToAction("Index", "Movies");
            }
            return RedirectToAction("Index");
        }

        public IActionResult SearchGrid(string type, string searchbar)
        {
            MovieViewModel movieViewModel = new MovieViewModel();

            if (type == "movie")
            {
                movieViewModel.Movies = _service.GetMovieByTitle(searchbar);
                TempData["Model"] = null;
                TempDataExtensions.Put(TempData, "Model", movieViewModel);
                return RedirectToAction("IndexGrid", "Movies");
            }
            return RedirectToAction("Index");
        }

        public IActionResult FilterSearch(string title, string[] genres)
        {
            MovieViewModel movieViewModel = new MovieViewModel();
            bool hasTitle = title != null;
            bool hasGenres = genres.Length > 0;

            if (hasTitle && hasGenres)
            {
                movieViewModel.Movies = _service.GetMovieByGenre(genres).Where(m => m.Title.Contains(title)).ToList();
            }
            else if (hasTitle)
            {
                movieViewModel.Movies = _service.GetMovieByTitle(title);
            }
            else if (hasGenres)
            {
                movieViewModel.Movies = _service.GetMovieByGenre(genres).ToList();
            }

            TempDataExtensions.Put(TempData, "Model", movieViewModel);
            return RedirectToAction("Index", "Movies");
        }
    }

    public static class TempDataExtensions
    {
        public static void Put<T>(this ITempDataDictionary tempData, string key, T value) where T : class
        {
            tempData[key] = JsonConvert.SerializeObject(value);
        }

        public static T Get<T>(this ITempDataDictionary tempData, string key) where T : class
        {
            object o;
            tempData.TryGetValue(key, out o);
            return o == null ? null : JsonConvert.DeserializeObject<T>((string)o);
        }
    }
}
