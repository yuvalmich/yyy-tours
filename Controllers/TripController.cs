using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using yyytours;
using yyytours.Models;

namespace yyytours.Controllers
{
    public class TripController : Controller
    {
        private readonly yyyWebProjContext _context;

        public TripController(yyyWebProjContext context)
        {
            _context = context;
        }

        // GET: Trip
        public async Task<IActionResult> Index()
        {
            var yyyWebProjContext = _context.Trip.Include(t => t.Guide).Include(t => t.Place);
            return View(await yyyWebProjContext.ToListAsync());
        }

        // GET: Trip/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trip = await _context.Trip
                .Include(t => t.Guide)
                .Include(t => t.Place)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (trip == null)
            {
                return NotFound();
            }

            return View(trip);
        }

        public async Task<IActionResult> Register(string tripID, string userEmail)
        {
            if(tripID == null)
            {
                return NotFound();
            }

            if(userEmail == null || userEmail.Length == 0)
            {
                return BadRequest();
            }

            var registeredUser = await _context.TripRegistration
                .Where(tr => ((tr.TripId == tripID) && (tr.UserEmail == userEmail))).ToListAsync();

            if(registeredUser != null)
            {
                // Change here to navigate to already registered page
                return BadRequest();
            }

            var tripReg = new TripRegistration();
            tripReg.TripId = tripID;
            tripReg.UserEmail = userEmail;
            tripReg.RegistrationDateTime = DateTime.Now;

            _context.Add(tripReg);
            await _context.SaveChangesAsync();

            return View("../Home/Index");
        }

        public async Task<IActionResult> Catalog()
    {
        var trips = await _context.Trip
                .Include(t => t.Guide)
                .Include(t => t.Place)
                .Where(i=>i.Date > DateTime.Now).OrderBy(i=>i.Date).ToListAsync();
        return View("catalog", trips);
    }

        // GET: Trip/Create
        public IActionResult Create()
        {
            ViewData["GuideId"] = new SelectList(_context.User, "Email", "FullName");
            ViewData["PlaceId"] = new SelectList(_context.Place, "ID", "Name");
            return View();
        }

        // POST: Trip/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,DisplayName,Description,PlaceId,GuideId,Price,Capacity,Date,TimeInHours")] Trip trip)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trip);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GuideId"] = new SelectList(_context.User, "ID", "ID", trip.GuideId);
            ViewData["PlaceId"] = new SelectList(_context.Place, "ID", "ID", trip.PlaceId);
            return View(trip);
        }

        // GET: Trip/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trip = await _context.Trip.FindAsync(id);
            if (trip == null)
            {
                return NotFound();
            }
            ViewData["GuideId"] = new SelectList(_context.User, "ID", "ID", trip.GuideId);
            ViewData["PlaceId"] = new SelectList(_context.Place, "ID", "ID", trip.PlaceId);
            return View(trip);
        }

        // POST: Trip/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ID,DisplayName,Description,PlaceId,GuideId,Price,Capacity,Date,TimeInHours")] Trip trip)
        {
            if (id != trip.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trip);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TripExists(trip.ID))
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
            ViewData["GuideId"] = new SelectList(_context.User, "ID", "ID", trip.GuideId);
            ViewData["PlaceId"] = new SelectList(_context.Place, "ID", "ID", trip.PlaceId);
            return View(trip);
        }

        // GET: Trip/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trip = await _context.Trip
                .Include(t => t.Guide)
                .Include(t => t.Place)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (trip == null)
            {
                return NotFound();
            }

            return View(trip);
        }

        // POST: Trip/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var trip = await _context.Trip.FindAsync(id);
            _context.Trip.Remove(trip);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TripExists(string id)
        {
            return _context.Trip.Any(e => e.ID == id);
        }
    }
}
