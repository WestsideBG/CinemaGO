using CinemaGO.Models;
using Microsoft.EntityFrameworkCore;
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
	}
}
