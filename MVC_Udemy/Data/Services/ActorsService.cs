using Microsoft.EntityFrameworkCore;
using MVC_Udemy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Udemy.Data.Services
{
    public class ActorsService : IActorsService
    {
        private readonly AppDbContext _context;

        public ActorsService(AppDbContext context)
        {
            _context = context;
        }

        // Adding a new actor
        public async Task AddAsync  (Actor actor)
        {
            await _context.AddAsync(actor);
            await _context.SaveChangesAsync();
        }

        // Deleting an actor
        public async Task DeleteAsync(int id)
        {
            var result = await _context.Actors.FirstOrDefaultAsync(n => n.Id == id);

            _context.Actors.Remove(result);

            await _context.SaveChangesAsync();
        }

        // Getting all actors
        public async Task<IEnumerable<Actor>> GetAllAsync()
        {
            var result = await _context.Actors.ToListAsync();
            return result;
        }

        // Getting an actor by id
        public async Task<Actor> GetByIdAsync(int id)
        {
            var result = await _context.Actors.FirstOrDefaultAsync(n => n.Id == id);

            return result;
        }

        // Updating an actor by id
        public async Task<Actor> UpdateAsync(int id, Actor actor)
        {
            _context.Update(actor);
            await _context.SaveChangesAsync();
            return actor;
        }
    }
}