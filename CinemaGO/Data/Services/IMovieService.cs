using CinemaGO.Models;
using System.Collections.Generic;

namespace CinemaGO.Data.Services
{
    public interface IMovieService
    {
        IEnumerable<Movie> GetAll();
        Movie GetMovieById(int id);

        void Add(Movie movie);
        List<Movie> GetMovieByTitle(string title);
        IEnumerable<Movie> GetMovieByGenre(string[] genres);
    }
}
