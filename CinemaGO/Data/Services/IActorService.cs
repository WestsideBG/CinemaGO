using CinemaGO.Models;
using System.Collections.Generic;

namespace CinemaGO.Data.Services
{
    public interface IActorService
    {
        IEnumerable<Actor> GetAll();
        Actor GetActorByID(int id);
        void Add(Actor actor);
    }
}
