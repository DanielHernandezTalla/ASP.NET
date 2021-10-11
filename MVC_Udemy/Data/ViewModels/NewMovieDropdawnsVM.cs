using MVC_Udemy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Udemy.Data.ViewModels
{
    public class NewMovieDropdawnsVM
    {
        public NewMovieDropdawnsVM()
        {
            producers = new List<Producer>();
            cinemas = new List<Cinema>();
            actors = new List<Actor>();
        }

        public List<Producer> producers  { get; set; }

        public List<Cinema> cinemas { get; set; }

        public List<Actor> actors { get; set; }

    }
}
