using CinemaGO.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CinemaGO.Data.Services
{
    public class MovieService : IMovieService
    {
        private readonly CinemaDbContext _dbContext;

        public MovieService(CinemaDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        IEnumerable<Movie> IMovieService.GetAll()
        {
            return _dbContext.Movies;
        }

        Movie IMovieService.GetMovieById(int id)
        {
            return _dbContext.Movies.SingleOrDefault(x => x.Id == id);
        }

        void IMovieService.Add(Movie movie)
        {
            this._dbContext.Movies.Add(movie);
            this._dbContext.SaveChanges();
        }


		List<Movie> IMovieService.GetMovieByTitle(string title)
		{
			List<Movie> movie = _dbContext.Movies.Where(m => m.Title.Contains(title)).ToList();
            return movie;
		}

        IEnumerable<Movie> IMovieService.GetMovieByGenre(string[] genres)
        {
            List<Movie> movies = new List<Movie>();
            foreach (var movie in _dbContext.Movies)
            {
                string[] currGenres = JsonConvert.DeserializeObject<string[]>(movie.Genre);
                for (int i = 0; i < genres.Length; i++)
                {
                    for (int j = 0; j < currGenres.Length; j++)
                    {
                        if (genres[i] == currGenres[j])
                        {
                            if (!movies.Any(m => m.Id == movie.Id))
                            movies.Add(movie);
                        }
                    }
                }
            }

            return movies;
        }
    }
}
