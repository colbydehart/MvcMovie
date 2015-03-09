using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;

namespace MvcMovie.Models
{
    public class Movie
    {
        public int ID { get; set; }

        [Required, StringLength(60, MinimumLength=3)]
        public string Title { get; set; }
        [Display(Name = "ReleaseDate"),DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        [Required]
        public string Genre { get; set; }
        [Range(1, 100), DataType(DataType.Currency)]
        public decimal Price { get; set; }
        [Required,StringLength(5)]
        public string Rating { get; set; }
        public bool Razzie { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }

        public double ratingAverage() 
        {
            return Ratings.Average(x => x.OneToFive);

        }
    }

    public class MovieDBContext : DbContext 
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Rating> Ratings { get; set; }
    }
}