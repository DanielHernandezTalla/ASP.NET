using MVC_Udemy.Data.Base;
using MVC_Udemy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Udemy.Data.Services
{
    public class CinemasService: EntityBaseRepository<Cinema>, ICinemasService
    {
        public CinemasService(AppDbContext context) :base(context) { }
    }
}
