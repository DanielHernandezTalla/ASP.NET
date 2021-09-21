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
    public class ProducersController : Controller
    {
        private readonly IProducerService _service;

        public ProducersController(IProducerService service)
        {
            _service = service;
        }

        // Get: Producer/Index
        public async Task<IActionResult> Index()
        {
            var allProducers = await _service.GetAllAsync();
            return View(allProducers);
        }

        // Get: Producer/Details
        public async Task<IActionResult> Details(int id)
        {
            var detailsProducer = await _service.GetByIdAsync(id);

            if (detailsProducer == null) return View("NotFound");

            return View(detailsProducer);
        }

        // Get: Producer/Create
        public IActionResult Create()
        {
            return View();
        }

        // Post: Producer/Create
        [HttpPost]
        public async Task<IActionResult> Create([Bind("FullName, ProfilePictureURL, Bio")] Producer producer)
        {
            if (!ModelState.IsValid) return View(producer);

            await _service.AddAsync(producer);

            return RedirectToAction(nameof(Index));
        }

        // Get: Producer/Edit
        public async Task<IActionResult> Edit(int id)
        {
            var producer = await _service.GetByIdAsync(id);

            if (producer == null) return View("NotFound");

            return View(producer);
        }

        // Post: Producer/Edit
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id, FullName, ProfilePictureURL, Bio")]Producer producer)
        {
            if (!ModelState.IsValid) return View(producer);

            await _service.UpdateAsync(id, producer);

            return RedirectToAction(nameof(Index));
        }

        // Get: Producer/Delete
        public async Task<IActionResult> Delete(int id)
        {
            var producer = await _service.GetByIdAsync(id);

            if (producer == null) return View("NotFound");

            return View(producer);
        }

        // Post: Producer/Delete
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producerDelete = await _service.GetByIdAsync(id);

            if (producerDelete == null) return View("NotFound");

            await _service.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
