﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MVC_Udemy.Data.Base
{
    public class EntityBaseRepository<T> : IEntityBaseRepository<T> where T : class, IEntityBase, new()
    {
        private readonly AppDbContext _context;
        public EntityBaseRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adding new Entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Deleting a Entity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Set<T>().FirstOrDefaultAsync(n => n.Id == id);

            EntityEntry entityEntry =  _context.Entry<T>(entity);

            entityEntry.State = EntityState.Deleted;

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Getting All
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetAllAsync() =>await _context.Set<T>().ToListAsync();

        public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();

            query = includeProperties.Aggregate(query, (current, includeProperties) => current.Include(includeProperties));

            return await query.ToListAsync();
        }

        /// <summary>
        /// Get By ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T> GetByIdAsync(int id) => await _context.Set<T>().FirstOrDefaultAsync(n => n.Id == id);

        /// <summary>
        /// Updating an Entity
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task UpdateAsync(int id, T entity)
        {
            EntityEntry entityEntry = _context.Entry<T>(entity);

            entityEntry.State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }
    }
}