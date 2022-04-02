using System.Collections.Generic;

namespace CinemaGO.Models
{
    public class MovieViewModel
    {
        public List<Movie> Movies { get; set; }
		public int MovieIndex { get; set; }
		public int MovieEndIndex { get; set; }
		public int CurrentPage { get; set; }

	}
}
