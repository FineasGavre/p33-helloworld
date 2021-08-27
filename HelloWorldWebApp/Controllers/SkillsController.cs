// <copyright file="SkillsController.cs" company="PRINCIPAL33">
// Copyright (c) PRINCIPAL33. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloWorldWebApp.Data;
using HelloWorldWebApp.Data.Models;
using HelloWorldWebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HelloWorldWebApp.Controllers
{
    /// <summary>
    /// Controller for the Skill entity.
    /// </summary>
    [Authorize]
    public class SkillsController : Controller
    {
        private readonly ApplicationContext context;
        private readonly IBroadcastService broadcastService;

        /// <summary>
        /// Initializes a new instance of the <see cref="SkillsController"/> class.
        /// </summary>
        /// <param name="context">DI ApplicationContext.</param>
        /// <param name="broadcastService">DI BroadcastService.</param>
        public SkillsController(ApplicationContext context, IBroadcastService broadcastService)
        {
            this.context = context;
            this.broadcastService = broadcastService;
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
        [Authorize(Roles = "Operator")]
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
        [Authorize(Roles = "Operator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,SkillMatrixUrl,InternId")] Skill skill)
        {
            if (ModelState.IsValid)
            {
                context.Add(skill);
                await context.SaveChangesAsync();
                await broadcastService.SendEntityAddedNotification("Skill", skill.Id.ToString());
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
        [Authorize(Roles = "Operator")]
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
        [Authorize(Roles = "Operator")]
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
                    await broadcastService.SendEntityUpdatedNotification("Skill", skill.Id.ToString());
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
        [Authorize(Roles = "Operator")]
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
        [Authorize(Roles = "Operator")]
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var skill = await context.Skills.FindAsync(id);
            context.Skills.Remove(skill);
            await context.SaveChangesAsync();
            await broadcastService.SendEntityRemovedNotification("Skill", id.ToString());
            return RedirectToAction(nameof(Index));
        }

        private bool SkillExists(int id)
        {
            return context.Skills.Any(e => e.Id == id);
        }
    }
}
