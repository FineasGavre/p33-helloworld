// <copyright file="InternsController.cs" company="PRINCIPAL33">
// Copyright (c) PRINCIPAL33. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloWorldWebApp.Data;
using HelloWorldWebApp.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HelloWorldWebApp.Controllers
{
    /// <summary>
    /// Controller for the Intern entity.
    /// </summary>
    public class InternsController : Controller
    {
        private readonly ApplicationContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="InternsController"/> class.
        /// </summary>
        /// <param name="context">DI injected ApplicationContext.</param>
        public InternsController(ApplicationContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Display the Index view.
        /// </summary>
        /// <returns>Index View with Index model.</returns>
        public async Task<IActionResult> Index()
        {
            return View(await context.Interns.ToListAsync());
        }

        /// <summary>
        /// Display the Detail view.
        /// </summary>
        /// <param name="id">Id of entity to be edited.</param>
        /// <returns>Detail View with Detail model.</returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var intern = await context.Interns
                .FirstOrDefaultAsync(m => m.Id == id);
            if (intern == null)
            {
                return NotFound();
            }

            return View(intern);
        }

        /// <summary>
        /// Display the Create view.
        /// </summary>
        /// <returns>Create View.</returns>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Create the Intern entity.
        /// </summary>
        /// <param name="intern">Intern object.</param>
        /// <returns>Adds the entity and returns the Create View.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Birthdate,Email")] Intern intern)
        {
            if (ModelState.IsValid)
            {
                context.Add(intern);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(intern);
        }

        /// <summary>
        /// Displays the Edit form.
        /// </summary>
        /// <param name="id">Id of entity to be edited.</param>
        /// <returns>Edit View.</returns>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var intern = await context.Interns.FindAsync(id);
            if (intern == null)
            {
                return NotFound();
            }

            return View(intern);
        }

        /// <summary>
        /// Updates the intern entity.
        /// </summary>
        /// <param name="id">Id of entity to be updated.</param>
        /// <param name="intern">Intern object with new values.</param>
        /// <returns>View.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Birthdate,Email")] Intern intern)
        {
            if (id != intern.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(intern);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InternExists(intern.Id))
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

            return View(intern);
        }

        /// <summary>
        /// Displays the delete form.
        /// </summary>
        /// <param name="id">Id of entity to be deleted.</param>
        /// <returns>Delete View.</returns>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var intern = await context.Interns
                .FirstOrDefaultAsync(m => m.Id == id);
            if (intern == null)
            {
                return NotFound();
            }

            return View(intern);
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="id">Id of entity to be deleted.</param>
        /// <returns>Delete view.</returns>
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var intern = await context.Interns.FindAsync(id);
            context.Interns.Remove(intern);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InternExists(int id)
        {
            return context.Interns.Any(e => e.Id == id);
        }
    }
}
