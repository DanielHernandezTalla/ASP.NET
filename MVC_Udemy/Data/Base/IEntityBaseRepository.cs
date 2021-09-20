using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Udemy.Data.Base
{
    // Interface base and generic for do to changes in a project
    public interface IEntityBaseRepository<T> where T: class, IEntityBase, new()
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetByIdAsync(int id);

        Task AddAsync(T entity);

        Task UpdateAsync(int id, T entity);

        Task DeleteAsync(int id);
    }
}
