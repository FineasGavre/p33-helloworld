using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HelloWorldWebApp.Data;
using HelloWorldWebApp.Data.Models;

namespace HelloWorldWebApp.Controllers
{
    public class LibraryResourcesController : Controller
    {
        private readonly ApplicationContext _context;

        public LibraryResourcesController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: LibraryResources
        public async Task<IActionResult> Index()
        {
            var applicationContext = _context.LibraryResources.Include(l => l.Skill);
            return View(await applicationContext.ToListAsync());
        }

        // GET: LibraryResources/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libraryResource = await _context.LibraryResources
                .Include(l => l.Skill)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (libraryResource == null)
            {
                return NotFound();
            }

            return View(libraryResource);
        }

        // GET: LibraryResources/Create
        public IActionResult Create()
        {
            ViewData["SkillId"] = new SelectList(_context.Skills, "Id", "Id");
            return View();
        }

        // POST: LibraryResources/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Recommendation,URL,SkillId")] LibraryResource libraryResource)
        {
            if (ModelState.IsValid)
            {
                _context.Add(libraryResource);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SkillId"] = new SelectList(_context.Skills, "Id", "Id", libraryResource.SkillId);
            return View(libraryResource);
        }

        // GET: LibraryResources/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libraryResource = await _context.LibraryResources.FindAsync(id);
            if (libraryResource == null)
            {
                return NotFound();
            }
            ViewData["SkillId"] = new SelectList(_context.Skills, "Id", "Id", libraryResource.SkillId);
            return View(libraryResource);
        }

        // POST: LibraryResources/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
                    _context.Update(libraryResource);
                    await _context.SaveChangesAsync();
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
            ViewData["SkillId"] = new SelectList(_context.Skills, "Id", "Id", libraryResource.SkillId);
            return View(libraryResource);
        }

        // GET: LibraryResources/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libraryResource = await _context.LibraryResources
                .Include(l => l.Skill)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (libraryResource == null)
            {
                return NotFound();
            }

            return View(libraryResource);
        }

        // POST: LibraryResources/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var libraryResource = await _context.LibraryResources.FindAsync(id);
            _context.LibraryResources.Remove(libraryResource);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LibraryResourceExists(int id)
        {
            return _context.LibraryResources.Any(e => e.Id == id);
        }
    }
}
