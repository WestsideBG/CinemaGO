namespace CinemaGO.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Actor
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Avatar")]
        public string AvatarURL { get; set; }

        public string Name { get; set; }

        public string Bio { get; set; }

        public List<Movie_Actor> Movies { get; set; }


        public Actor()
        {

        }
        
        public Actor(string avatar, string name, string bio)
        {
            this.AvatarURL = avatar;
            this.Name = name;
            this.Bio = bio;
        }


        public Movie GetLastMovie()
        {
            //var movie = MovieService.GetMovieById(this.Movies[Movies.Count - 1].MovieId);
            throw new System.Exception();
        }
    }
}
