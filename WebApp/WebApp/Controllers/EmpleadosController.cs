using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class EmpleadosController : Controller
    {
        private readonly EmpleadosDbContext _context;
        public EmpleadosController(IConfiguration config)
        {
            _context = new EmpleadosDbContext(config);
        }

        // GET: Empleados
        public IActionResult Index()
        {
            return View(_context.ToList());
        }

        // GET: Empleados/Details/0000000-0000-0000-0000-000000000000
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = _context.Find(id);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // GET: Empleados/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Empleados/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Empleado model)
        {
            if (model.EmpleadoID == Guid.Empty)
            {
                model.EmpleadoID = Guid.NewGuid();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(model);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_context.Find(model.EmpleadoID) == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Empleados/Edit/0000000-0000-0000-0000-000000000000
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = _context.Find(id);

            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        // POST: Empleados/Edit/0000000-0000-0000-0000-000000000000
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, Empleado model)//[Bind("EmpleadoID,Nombre,Edad")] 
        {
            if (id != model.EmpleadoID)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(model);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_context.Find(model.EmpleadoID) == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Empleados/Delete/0000000-0000-0000-0000-000000000000
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = _context.Find(id);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }
    }
}
