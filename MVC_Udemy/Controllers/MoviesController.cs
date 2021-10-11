using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_Udemy.Data;
using MVC_Udemy.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Udemy.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMoviesService _service;

        public MoviesController(IMoviesService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var movies = await _service.GetAllAsync(n => n.Cinema   );
            return View(movies);
        }

        // Get: Movies/Details
        public async Task<IActionResult> Details(int id)
        {
            var movieDetail = await _service.GetMovieByIdAsync(id);
            
            return View(movieDetail);
        }

        public async Task<IActionResult> Create()
        {
            var movieDropdawnData = await _service.GetNewMovieDropdawnsValues();

            ViewBag.Cinemas = new SelectList(movieDropdawnData.cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(movieDropdawnData.producers, "Id", "FullName");
            ViewBag.ActorIds = new SelectList(movieDropdawnData.actors, "Id", "FullName");

            return View();
        }

    }
}
