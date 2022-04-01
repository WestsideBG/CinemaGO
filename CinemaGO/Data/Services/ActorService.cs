using CinemaGO.Models;
using System.Collections.Generic;
using System.Linq;

namespace CinemaGO.Data.Services
{
    public class ActorService : IActorService
    {
        private readonly CinemaDbContext _dbContext;

        public ActorService(CinemaDbContext dbContext)
        {
            this._dbContext = dbContext;
            this._dbContext.Actors.Add(new Actor(avatar: "/img/jason.jpg", name: "Ivan Tomov", bio: "Biography"));
            this._dbContext.SaveChanges();
        }

        void IActorService.Add(Actor actor)
        {
            _dbContext.Actors.Add(actor);
            _dbContext.SaveChanges();
        }

        Actor IActorService.GetActorByID(int id)
        {
            return _dbContext.Actors.SingleOrDefault(a => a.Id == id);
        }

        IEnumerable<Actor> IActorService.GetAll()
        {
            return _dbContext.Actors.ToList();
        }
    }
}
