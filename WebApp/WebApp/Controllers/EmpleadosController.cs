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

            var empleado = _context.Find(id);

            if (empleado == null)
            {
                return NotFound();
            }

            return View(empleado);
        }

        // GET: Empleados/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Empleados/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Empleado empleado)
        {
            if (empleado.EmpleadoID == Guid.Empty)
            {
                empleado.EmpleadoID = Guid.NewGuid();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(empleado);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_context.Find(empleado.EmpleadoID) == null)
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
            return View(empleado);
        }

        // GET: Empleados/Edit/0000000-0000-0000-0000-000000000000
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleado = _context.Find(id);

            if (empleado == null)
            {
                return NotFound();
            }
            return View(empleado);
        }

        // POST: Empleados/Edit/0000000-0000-0000-0000-000000000000
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, Empleado empleado)//[Bind("EmpleadoID,Nombre,Edad")] 
        {
            if (id != empleado.EmpleadoID)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(empleado);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_context.Find(empleado.EmpleadoID) == null)
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
            return View(empleado);
        }

        // GET: Empleados/Delete/0000000-0000-0000-0000-000000000000
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleado = _context.Find(id);

            if (empleado == null)
            {
                return NotFound();
            }

            return View(empleado);
        }

        // POST: Empleados/Delete/0000000-0000-0000-0000-000000000000
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Guid id)
        {
            var empleado = _context.Find(id);

            if (empleado == null)
            {
                return NotFound();
            }

            _context.Remove(empleado.EmpleadoID);

            return RedirectToAction(nameof(Index));
        }
    }
}
