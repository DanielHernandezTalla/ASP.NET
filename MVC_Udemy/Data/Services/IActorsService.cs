using MVC_Udemy.Data.Base;
using MVC_Udemy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Udemy.Data.Services
{
    public interface IActorsService: IEntityBaseRepository<Actor>
    {
        // This methods was replaced by entityBase 

        //Task<IEnumerable<Actor>> GetAllAsync();

        //Task<Actor> GetByIdAsync(int id);

        //Task AddAsync(Actor actor);

        //Task<Actor> UpdateAsync(int id, Actor actor);

        //Task DeleteAsync(int id);
    }
}
