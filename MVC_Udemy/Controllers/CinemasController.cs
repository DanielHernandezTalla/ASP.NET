using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_Udemy.Data;
using MVC_Udemy.Data.Services;
using MVC_Udemy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Udemy.Controllers
{
    public class CinemasController : Controller
    {
        private readonly ICinemasService _service;

        public CinemasController(ICinemasService service)
        {
            _service = service;
        }

        // Get: Cinemas/Index
        public async Task<IActionResult> Index()
        {
            var cinemas = await _service.GetAllAsync();
            return View(cinemas);
        }

        // Get: Cinemas/Details
        public async Task<IActionResult> Details(int id)
        {
            var cinema = await _service.GetByIdAsync(id);

            if (cinema == null) return View("NotFound");

            return View(cinema);
        }

        // Get: Cinemas/Edit
        public async Task<IActionResult> Edit(int id)
        {
            var cinema = await _service.GetByIdAsync(id);

            if (cinema == null) return View("NotFound");

            return View(cinema);
        }

        // Post: Cinemas/Edit
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Logo, Name, Description")] Cinema cinema)
        {
            if (!ModelState.IsValid) return View(cinema);

            await _service.UpdateAsync(id, cinema);

            return RedirectToAction(nameof(Index));
        }

        // Get: Cinemas/Delete
        public async Task<IActionResult> Delete(int id)
        {
            var cinema = await _service.GetByIdAsync(id);

            if (cinema == null) return View("NotFound");

            return View(cinema);
        }

        // Post: Cinemas/Delete
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cinema = await _service.GetByIdAsync(id);

            if (cinema == null) return View("NotFound");

            await _service.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }

        // Get: Cinemas/Create
        public IActionResult Create()
        {
            return View();
        }

        // Post: Cinemas/Create
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Logo, Name, Description")] Cinema cinema)
        {
            if (!ModelState.IsValid) return View(cinema);

            await _service.AddAsync(cinema);

            return RedirectToAction(nameof(Index));
        }
    }
}