﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using yyytours;
using yyytours.Models;

namespace yyy_tours
{
    public class TripRegistrationController : Controller
    {
        private readonly yyyWebProjContext _context;

        public TripRegistrationController(yyyWebProjContext context)
        {
            _context = context;
        }

        private UserType? getSessionUserType()
        {
            if (!HttpContext.Session.Keys.Any(k => k.Equals("Type")))
            {
                return null;
            }
            return (UserType)HttpContext.Session.GetInt32("Type");
        }

        // GET: TripRegistration
        public async Task<IActionResult> Index()
        {
            if(HttpContext.Session.GetString("Email") == "")
            {
                return View("../User/NotAuthorized");
            }
            string userEmail = HttpContext.Session.GetString("Email");
            return View(await _context.TripRegistration.Where(tr => tr.UserEmail == userEmail).Include(tr => tr.Trip).Include(tr => tr.Trip.Place).OrderBy(tr => tr.Trip.Date).ToListAsync());
        }


        public async Task<IActionResult> GuideTrips()
        {
            if (HttpContext.Session.GetString("Email") == null || HttpContext.Session.GetString("Email") == "" || getSessionUserType() != UserType.Guide)
            {
                return View("../User/NotAuthorized");
            }
            string guideEmail = HttpContext.Session.GetString("Email");
            var registeredUsersByTrips =  await _context.TripRegistration.Include(tr => tr.Trip).Where(tr => tr.Trip.GuideId == guideEmail).Include(tr => tr.User).GroupBy(tr => tr.Trip.DisplayName).ToDictionaryAsync(y => y.Key, y => y.Select(z => z));
            return View("../TripRegistration/TripGuide", registeredUsersByTrips);
            //var guideTrips = await _context.Trip.Where(tr => tr.GuideId == guideEmail).Select(tr => tr.ID).ToListAsync();
            //return View(await _context.TripRegistration.Where(tr => guideTrips.Contains(tr.TripId)).Include(tr => tr.Trip).Include(tr => tr.Trip.Place).OrderBy(tr => tr.Trip.Date).ToListAsync());
        }

        // GET: TripRegistration/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (HttpContext.Session.GetString("Email") == "")
            {
                return View("../User/NotAuthorized");
            }
            if (id == null)
            {
                return NotFound();
            }

            var tripRegistration = await _context.TripRegistration
                .FirstOrDefaultAsync(m => m.ID == id);
            if (tripRegistration == null)
            {
                return NotFound();
            }

            return View(tripRegistration);
        }

        // GET: TripRegistration/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TripRegistration/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,TripId,UserEmail,RegistrationDateTime")] TripRegistration tripRegistration)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tripRegistration);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tripRegistration);
        }

        // GET: TripRegistration/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tripRegistration = await _context.TripRegistration.FindAsync(id);
            if (tripRegistration == null)
            {
                return NotFound();
            }
            return View(tripRegistration);
        }

        // POST: TripRegistration/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ID,TripId,UserEmail,RegistrationDateTime")] TripRegistration tripRegistration)
        {
            if (id != tripRegistration.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tripRegistration);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TripRegistrationExists(tripRegistration.ID))
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
            return View(tripRegistration);
        }

        // GET: TripRegistration/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (HttpContext.Session.GetString("Email") == "")
            {
                return View("../User/NotAuthorized");
            }
            if (id == null)
            {
                return NotFound();
            }

            var tripRegistration = await _context.TripRegistration.Include(tr => tr.Trip).Include(tr => tr.Trip.Place)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (tripRegistration == null)
            {
                return NotFound();
            }

            return View(tripRegistration);
        }

        // POST: TripRegistration/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var tripRegistration = await _context.TripRegistration.FindAsync(id);
            _context.TripRegistration.Remove(tripRegistration);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TripRegistrationExists(string id)
        {
            return _context.TripRegistration.Any(e => e.ID == id);
        }
    }
}
