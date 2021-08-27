// <copyright file="LibraryResourcesController.cs" company="PRINCIPAL33">
// Copyright (c) PRINCIPAL33. All rights reserved.
// </copyright>

using System.Linq;
using System.Threading.Tasks;
using HelloWorldWebApp.Data;
using HelloWorldWebApp.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HelloWorldWebApp.Controllers
{
    /// <summary>
    /// Controller for the LibraryResources entity.
    /// </summary>
    [Authorize]
    public class LibraryResourcesController : Controller
    {
        private readonly ApplicationContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="LibraryResourcesController"/> class.
        /// </summary>
        /// <param name="context">DI ApplicationContext.</param>
        public LibraryResourcesController(ApplicationContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Display the Index view.
        /// </summary>
        /// <returns>Index View with Index model.</returns>
        public async Task<IActionResult> Index()
        {
            var applicationContext = context.LibraryResources.Include(l => l.Skill);
            return View(await applicationContext.ToListAsync());
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

            var libraryResource = await context.LibraryResources
                .Include(l => l.Skill)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (libraryResource == null)
            {
                return NotFound();
            }

            return View(libraryResource);
        }

        /// <summary>
        /// Display the Create view.
        /// </summary>
        /// <returns>Create View.</returns>
        [Authorize(Roles = "Operator")]
        public IActionResult Create()
        {
            ViewData["SkillId"] = new SelectList(context.Skills, "Id", "Id");
            return View();
        }

        /// <summary>
        /// Create the LibraryResource entity.
        /// </summary>
        /// <param name="libraryResource">LibraryResource object.</param>
        /// <returns>Adds the entity and returns the Create View.</returns>
        [Authorize(Roles = "Operator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Recommendation,URL,SkillId")] LibraryResource libraryResource)
        {
            if (ModelState.IsValid)
            {
                context.Add(libraryResource);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["SkillId"] = new SelectList(context.Skills, "Id", "Id", libraryResource.SkillId);
            return View(libraryResource);
        }

        /// <summary>
        /// Displays the Edit form.
        /// </summary>
        /// <param name="id">Id of entity to be edited.</param>
        /// <returns>Edit View.</returns>
        [Authorize(Roles = "Operator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libraryResource = await context.LibraryResources.FindAsync(id);
            if (libraryResource == null)
            {
                return NotFound();
            }

            ViewData["SkillId"] = new SelectList(context.Skills, "Id", "Id", libraryResource.SkillId);
            return View(libraryResource);
        }

        /// <summary>
        /// Updates the LibraryResource entity.
        /// </summary>
        /// <param name="id">Id of entity to be updated.</param>
        /// <param name="libraryResource">LibraryResource object with new values.</param>
        /// <returns>View.</returns>
        [Authorize(Roles = "Operator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Recommendation,URL,SkillId")] LibraryResource libraryResource)
        {
            if (id != libraryResource.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(libraryResource);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LibraryResourceExists(libraryResource.Id))
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

            ViewData["SkillId"] = new SelectList(context.Skills, "Id", "Id", libraryResource.SkillId);
            return View(libraryResource);
        }

        /// <summary>
        /// Displays the delete form.
        /// </summary>
        /// <param name="id">Id of entity to be deleted.</param>
        /// <returns>Delete View.</returns>
        [Authorize(Roles = "Operator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libraryResource = await context.LibraryResources
                .Include(l => l.Skill)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (libraryResource == null)
            {
                return NotFound();
            }

            return View(libraryResource);
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="id">Id of entity to be deleted.</param>
        /// <returns>Delete view.</returns>
        [Authorize(Roles = "Operator")]
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var libraryResource = await context.LibraryResources.FindAsync(id);
            context.LibraryResources.Remove(libraryResource);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LibraryResourceExists(int id)
        {
            return context.LibraryResources.Any(e => e.Id == id);
        }
    }
}
