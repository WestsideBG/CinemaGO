using CinemaGO.Models;
using Microsoft.EntityFrameworkCore;
namespace CinemaGO.Data
{
    public class CinemaDbContext : DbContext
    {
        public CinemaDbContext(DbContextOptions<CinemaDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie_Actor>().HasKey(am => new
            {
                am.ActorId,
                am.MovieId
            });

            modelBuilder.Entity<Movie_Actor>().HasOne(m => m.Movie).WithMany(am => am.Actors).HasForeignKey(f => f.MovieId);
            modelBuilder.Entity<Movie_Actor>().HasOne(a => a.Actor).WithMany(am => am.Movies).HasForeignKey(f => f.ActorId);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Movie_Actor> Movies_Actors { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Producer> Producers { get; set; }

    }
}
