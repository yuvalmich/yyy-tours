using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using yyytours;
using yyytours.Models;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace yyytours.Controllers
{
    public class PlaceController : Controller
    {
        private readonly yyyWebProjContext _context;

        public PlaceController(yyyWebProjContext context)
        {
            _context = context;
        }

        // GET: Place
        public async Task<IActionResult> Index()
        {
            if (getSessionUserType() != UserType.Admin)
            {
                Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return View("Error", new ErrorViewModel {ErrorDescription = "אינך מורשה לגשת לעמוד זה"});
            }
                
            return View(await _context.Place.ToListAsync());
        }

        // GET: Place/Create
        public IActionResult Create()
        {
            if (getSessionUserType() != UserType.Admin)
            {
                Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return View("Error", new ErrorViewModel {ErrorDescription = "אינך מורשה לגשת לעמוד זה"});
            }
            
            return View();
        }

        // POST: Place/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Description,ImageUrl,Country")] Place place)
        {
            if (getSessionUserType() != UserType.Admin)
                return View("NotAuthorized");
            place.ID = Guid.NewGuid().ToString();
            if (ModelState.IsValid)
            {
                _context.Add(place);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(place);
        }

        // GET: Place/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (getSessionUserType() != UserType.Admin)
            {
                Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return View("Error", new ErrorViewModel {ErrorDescription = "אינך מורשה לגשת לעמוד זה"});
            }

            if (id == null)
            {
                return NotFound();
            }

            var place = await _context.Place.FindAsync(id);
            if (place == null)
            {
                return NotFound();
            }
            return View(place);
        }

        // POST: Place/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ID,Name,Description,ImageUrl,Country")] Place place)
        {
            if (getSessionUserType() != UserType.Admin)
            {
                Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return View("Error", new ErrorViewModel {ErrorDescription = "אינך מורשה לגשת לעמוד זה"});
            }

            if (id != place.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(place);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlaceExists(place.ID))
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
            return View(place);
        }

        // GET: Place/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (getSessionUserType() != UserType.Admin)
            {
                Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return View("Error", new ErrorViewModel {ErrorDescription = "אינך מורשה לגשת לעמוד זה"});
            }

            if (id == null)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                return View("Error", new ErrorViewModel {ErrorDescription = "טיול זה לא נמצא", ControllerToLink="Trip", ActionToLink=nameof(Index), TextToLink="חזרה לרשימת הטיולים"});
            }

            var place = await _context.Place
                .FirstOrDefaultAsync(m => m.ID == id);
            if (place == null)
            {
                return NotFound();
            }

            if(_context.Trip.Any(i=>i.PlaceId == id))
            {
                return BadRequest("אין אפשרות למחוק מיקום אשר קיימים טיולים בו");
            }
            return View(place);
        }

        // POST: Place/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (getSessionUserType() != UserType.Admin)
            {
                Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return View("Error", new ErrorViewModel {ErrorDescription = "אינך מורשה לגשת לעמוד זה"});
            }
                
            var place = await _context.Place.FindAsync(id);
            _context.Place.Remove(place);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlaceExists(string id)
        {
            return _context.Place.Any(e => e.ID == id);
        }

        private UserType? getSessionUserType()
        {
            if (!HttpContext.Session.Keys.Any(k => k.Equals("Type")))
            {
                return null;
            }
            return (UserType)HttpContext.Session.GetInt32("Type");
        }
    }
}
