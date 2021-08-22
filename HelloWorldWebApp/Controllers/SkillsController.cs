// <copyright file="SkillsController.cs" company="PRINCIPAL33">
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
    /// Controller for the Skill entity.
    /// </summary>
    public class SkillsController : Controller
    {
        private readonly ApplicationContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="SkillsController"/> class.
        /// </summary>
        /// <param name="context">DI ApplicationContext.</param>
        public SkillsController(ApplicationContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Display the Index view.
        /// </summary>
        /// <returns>Index View with Index model.</returns>
        public async Task<IActionResult> Index()
        {
            var applicationContext = context.Skills.Include(s => s.Intern);
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

            var skill = await context.Skills
                .Include(s => s.Intern)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (skill == null)
            {
                return NotFound();
            }

            return View(skill);
        }

        /// <summary>
        /// Display the Create view.
        /// </summary>
        /// <returns>Create View.</returns>
        public IActionResult Create()
        {
            ViewData["InternId"] = new SelectList(context.Interns, "Id", "Id");
            return View();
        }

        /// <summary>
        /// Create the Skill entity.
        /// </summary>
        /// <param name="skill">Skill object.</param>
        /// <returns>Adds the entity and returns the Create View.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,SkillMatrixUrl,InternId")] Skill skill)
        {
            if (ModelState.IsValid)
            {
                context.Add(skill);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["InternId"] = new SelectList(context.Interns, "Id", "Id", skill.InternId);
            return View(skill);
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

            var skill = await context.Skills.FindAsync(id);
            if (skill == null)
            {
                return NotFound();
            }

            ViewData["InternId"] = new SelectList(context.Interns, "Id", "Id", skill.InternId);
            return View(skill);
        }

        /// <summary>
        /// Updates the Skill entity.
        /// </summary>
        /// <param name="id">Id of entity to be updated.</param>
        /// <param name="skill">Intern object with new values.</param>
        /// <returns>View.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,SkillMatrixUrl,InternId")] Skill skill)
        {
            if (id != skill.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(skill);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SkillExists(skill.Id))
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

            ViewData["InternId"] = new SelectList(context.Interns, "Id", "Id", skill.InternId);
            return View(skill);
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

            var skill = await context.Skills
                .Include(s => s.Intern)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (skill == null)
            {
                return NotFound();
            }

            return View(skill);
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
            var skill = await context.Skills.FindAsync(id);
            context.Skills.Remove(skill);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SkillExists(int id)
        {
            return context.Skills.Any(e => e.Id == id);
        }
    }
}
