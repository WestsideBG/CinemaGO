using CinemaGO.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CinemaGO.Models
{
    public class Movie
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public string CoverURL { get; set; }
        public string BgURL { get; set; }
        public string Image1URL { get; set; }
        public string Image2URL { get; set; }
        public string Image3URL { get; set; }
        public string Image4URL { get; set; }
        public string Duration { get; set; }
        public DateTime Release { get; set; }
        public string Genre { get; set; } 

        //DbRelations
        public List<Movie_Actor> Actors { get; set; }
        public Cinema Cinema { get; set; }
        
        public int? CinemaId { get; set; }
        public Producer Producer { get; set; }
        public int? ProducerId { get; set; }

        public Movie()
        {

        }

        public Movie(string title,
                     string genre,
                     DateTime release,
                     string duration,
                     string description,
                     string coverURL,
                     string bgURL,
                     string image1 = "defaultImage",
                     string image2 = "defaultImage",
                     string image3 = "defaultImage",
                     string image4 = "defaultImage")
        {
            this.Title = title;
            this.Description = description;
            this.Release = release;
            this.Genre = genre;
            this.CoverURL = coverURL;
            this.Duration = duration;

            if (image1 != "defaultImage")
            {
                this.Image1URL = image1;
            }
            if (image2 != "defaultImage")
            {
                this.Image2URL = image2;
            }
            if (image3 != "defaultImage")
            {
                this.Image3URL = image3;
            }
            if (image4 != "defaultImage")
            {
                this.Image4URL = image4;
            }
        }

    }
}
