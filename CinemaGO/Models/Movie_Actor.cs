namespace CinemaGO.Models
{
    public class Movie_Actor
    {
        public int MovieId { get; set; }
        public Movie Movie { get; set; }

        public int ActorId { get; set; }
        public Actor Actor { get; set; }

        public Movie_Actor()
        {

        }

        public Movie_Actor(int movieId, int actorId)
        {
            this.MovieId = movieId;
            this.ActorId = actorId;
        }
    }
}
