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

        /// <summary>
        /// Adding a new actor
        /// </summary>
        /// <param name="actor"></param>
        /// <returns></returns>
        public async Task AddAsync  (Actor actor)
        {
            await _context.AddAsync(actor);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Deleting an actor
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteAsync(int id)
        {
            var result = await _context.Actors.FirstOrDefaultAsync(n => n.Id == id);

            _context.Actors.Remove(result);

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Getting all actors
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Actor>> GetAllAsync()
        {
            var result = await _context.Actors.ToListAsync();
            return result;
        }

        /// <summary>
        /// Getting an actor by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Actor> GetByIdAsync(int id)
        {
            var result = await _context.Actors.FirstOrDefaultAsync(n => n.Id == id);

            return result;
        }

        /// <summary>
        /// Updating an actor by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="actor"></param>
        /// <returns></returns>
        public async Task<Actor> UpdateAsync(int id, Actor actor)
        {
            _context.Update(actor);
            await _context.SaveChangesAsync();
            return actor;
        }
    }
}