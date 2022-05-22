using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RSVP_website_SSTU.Models;

namespace RSVP_website_SSTU.Controllers
{
    public class RsvpsController : Controller
    {
        private readonly InfotechRSVPContext _context;

        public RsvpsController(InfotechRSVPContext context)
        {
            _context = context;
        }

        // GET: Rsvps
        public async Task<IActionResult> Index()
        {
            return _context.Rsvps != null ?
                        View(await _context.Rsvps.ToListAsync()) :
                        Problem("Ошибка: база пуста");
        }

        public IActionResult SuccesfulCreate()
        {
            return View("Welcome");
        }

        // GET: Rsvps/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Rsvps == null)
            {
                return NotFound();
            }

            var rsvp = await _context.Rsvps
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rsvp == null)
            {
                return NotFound();
            }

            return View(rsvp);
        }

        // GET: Rsvps/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Email,Phone,WillAttend")] Rsvp rsvp)
        {
            if (RsvpExistsPhone(rsvp.Phone))
            {
                if (ModelState.IsValid)
                { 
                    rsvp.Id = RsvpGetIdByPhone(rsvp.Phone);
                    _context.Update(rsvp);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(SuccesfulCreate));
                }
            }
            else if (ModelState.IsValid && !RsvpExistsPhone(rsvp.Phone))
            {
                rsvp.Id = Guid.NewGuid();
                _context.Add(rsvp);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(SuccesfulCreate));
            }
            return View(rsvp);
        }

        // GET: Rsvps/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Rsvps == null)
            {
                return NotFound();
            }

            var rsvp = await _context.Rsvps.FindAsync(id);
            if (rsvp == null)
            {
                return NotFound();
            }
            return View(rsvp);
        }

        // POST: Rsvps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Email,Phone,WillAttend")] Rsvp rsvp)
        {
            if (id != rsvp.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rsvp);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RsvpExists(rsvp.Id))
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
            return View(rsvp);
        }

        // GET: Rsvps/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Rsvps == null)
            {
                return NotFound();
            }

            var rsvp = await _context.Rsvps
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rsvp == null)
            {
                return NotFound();
            }

            return View(rsvp);
        }

        // POST: Rsvps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Rsvps == null)
            {
                return Problem("Entity set 'InfotechRSVPContext.Rsvps'  is null.");
            }
            var rsvp = await _context.Rsvps.FindAsync(id);
            if (rsvp != null)
            {
                _context.Rsvps.Remove(rsvp);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RsvpExists(Guid id)
        {
            return (_context.Rsvps?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private bool RsvpExistsPhone(string phone)
        {
            return (_context.Rsvps?.Any(e => e.Phone == phone)).GetValueOrDefault();
        }

        private Guid RsvpGetIdByPhone(string phone)
        {
            return _context.Rsvps.AsNoTracking().Where(e => e.Phone == phone).First().Id;
        }
    }
}
