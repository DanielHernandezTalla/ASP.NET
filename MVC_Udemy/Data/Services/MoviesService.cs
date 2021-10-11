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
