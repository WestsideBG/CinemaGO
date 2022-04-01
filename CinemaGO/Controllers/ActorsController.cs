using CinemaGO.Data.Services;
using CinemaGO.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;


namespace CinemaGO.Controllers
{
    public class ActorsController : Controller
    {
        private readonly IActorService _service;

        public ActorsController(IActorService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            var result = _service.GetAll().ToList();
            return View(result);
        }

        public IActionResult Details()
        {
            //var id = Url.ActionContext.RouteData.Values["id"].ToString();
            var id = Request.Form["actorId"];
            Actor actor = _service.GetActorByID(int.Parse(id));
            return View(actor);
        }

        public IActionResult CreateActor()
        {
            return View();
        }
        public IActionResult AddActor()
        {
            return View("Index");
        }
    }
}
