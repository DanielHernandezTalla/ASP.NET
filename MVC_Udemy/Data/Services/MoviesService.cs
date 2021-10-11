using Microsoft.EntityFrameworkCore;
using MVC_Udemy.Data.Base;
using MVC_Udemy.Data.ViewModels;
using MVC_Udemy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Udemy.Data.Services
{
    public class MoviesService: EntityBaseRepository<Movie>, IMoviesService
    {
        private readonly AppDbContext _context;
        public MoviesService(AppDbContext context) : base(context) 
        {
            _context = context;
        }

        public async Task AddNewMovieAsync(NewMovieVM data)
        {
            var newMovie = new Movie()
            {
                Name = data.Name,
                Description = data.Description,
                Price = data.Price,
                ImageURL = data.ImageURL,
                CinemaId = data.CinemaId,
                StartDate = data.StartDate,
                EndDate = data.EndDate,
                MovieCategory = data.MovieCategory,
                ProducerId = data.ProducerId
            };

            await _context.Movies.AddAsync(newMovie);
            await _context.SaveChangesAsync();

            // Add Movie Actors
            foreach (var actorId in data.ActorsIds)
            {
                var newActorMovie = new Actor_Movie()
                {
                    MovieId = newMovie.Id,
                    ActorId = actorId
                };

                await _context.Actors_Movies.AddAsync(newActorMovie);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            var movie = await _context.Movies
                .Include(c => c.Cinema)
                .Include(p => p.Producer)
                .Include(am => am.Actor_Movies).ThenInclude(a => a.Actor)
                .FirstOrDefaultAsync(n => n.Id == id);

            return movie;
        }

        public async Task<NewMovieDropdawnsVM> GetNewMovieDropdawnsValues()
        {
            var response = new NewMovieDropdawnsVM()
            {
                actors = await _context.Actors.OrderBy(n => n.FullName).ToListAsync(),
                cinemas = await _context.Cinemas.OrderBy(n => n.Name).ToListAsync(),
                producers = await _context.Producers.OrderBy(n => n.FullName).ToListAsync()
            };

            return response;
        }
    }
}
