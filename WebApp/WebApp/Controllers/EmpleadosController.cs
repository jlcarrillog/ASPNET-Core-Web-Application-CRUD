using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class EmpleadosController : Controller
    {
        private readonly EmpleadosDbContext context;
        public EmpleadosController(IConfiguration config) { context = new EmpleadosDbContext(config); }

        // GET: Empleados
        public IActionResult Index()
        {
            return View(context.ToList());
        }

        // GET: Empleados/Details/0000000-0000-0000-0000-000000000000
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = context.Find(id);

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
        public IActionResult Create(Empleado model, IFormFile Foto)
        {
            if (model.EmpleadoID == Guid.Empty)
            {
                model.EmpleadoID = Guid.NewGuid();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    model.Foto = FileConverter.ConvertBinary(Foto);
                    context.Add(model);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (context.Find(model.EmpleadoID) == null)
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
            var model = context.Find(id);

            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        // POST: Empleados/Edit/0000000-0000-0000-0000-000000000000
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, Empleado model, IFormFile Foto)//[Bind("EmpleadoID,Nombre,Edad")] 
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.Foto = FileConverter.ConvertBinary(Foto);
                    context.Update(id, model);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (context.Find(id) == null)
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

            var model = context.Find(id);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }
    }
}
