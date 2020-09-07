﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
//using System.Data.Entity;
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
            var registeredUsersByTrips =  (await _context.TripRegistration.Include(tr => tr.Trip).Where(tr => tr.Trip.GuideId == guideEmail).Include(tr => tr.User).OrderBy(tr => tr.RegistrationDateTime).ToListAsync()).GroupBy(tr => tr.Trip.DisplayName);
            return View("../TripRegistration/TripGuide", registeredUsersByTrips);
        }

        public async Task<IActionResult> ManagerTrips()
        {
            if (HttpContext.Session.GetString("Email") == null || HttpContext.Session.GetString("Email") == "" || getSessionUserType() != UserType.Admin)
            {
                return View("../User/NotAuthorized");
            }

            var registeredUsersByTrips = (await _context.TripRegistration.Include(tr => tr.Trip).Include(tr => tr.User).OrderBy(tr => tr.RegistrationDateTime).ToListAsync()).GroupBy(tr => tr.Trip.DisplayName);
            return View("../TripRegistration/TripManager", registeredUsersByTrips);
        }

        public async Task<IActionResult> ManagerGraphs()
        {
            if (HttpContext.Session.GetString("Email") == null || HttpContext.Session.GetString("Email") == "" || getSessionUserType() != UserType.Admin)
            {
                return View("../User/NotAuthorized");
            }


            ViewData["Graph1"] = (await _context.TripRegistration.Include(tr => tr.Trip).ToListAsync()).OrderBy(tr => tr.RegistrationDateTime).GroupBy(tr => tr.RegistrationDateTime.Date).Select(tr => new RegistrationByDateCount
            {
                date = tr.Key.Date.ToShortDateString(),
                registeredCount = tr.Count()
            }).ToList();


            var registeredUsersCount = (await _context.TripRegistration.Include(tr => tr.Trip).ToListAsync()).GroupBy(tr => tr.Trip.DisplayName).Select(tr => new TripsRegistrationCount
            {
                tripName = tr.Key,
                registeredCount = tr.Count()
            }).ToList();

            Dictionary<string, int> registeredUsersCountDict = new Dictionary<string, int>();
            foreach (var item in registeredUsersCount)
            {
                registeredUsersCountDict.Add(item.tripName, item.registeredCount);
            }
            ViewData["Graph2"] = registeredUsersCountDict;

            return View("../TripRegistration/ManagerGraph");
        }


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

        public async Task<IActionResult> ManagerDelete(string id)
        {
            if (HttpContext.Session.GetString("Email") == null || HttpContext.Session.GetString("Email") == "" || getSessionUserType() != UserType.Admin)
            {
                return View("../User/NotAuthorized");
            }
            if (id == null)
            {
                return NotFound();
            }

            var tripRegistration = await _context.TripRegistration.FindAsync(id);
            _context.TripRegistration.Remove(tripRegistration);
            await _context.SaveChangesAsync();

            var registeredUsersByTrips = (await _context.TripRegistration.Include(tr => tr.Trip).Include(tr => tr.User).OrderBy(tr => tr.RegistrationDateTime).ToListAsync()).GroupBy(tr => tr.Trip.DisplayName);
            return View("../TripRegistration/TripManager", registeredUsersByTrips);
        }

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

    public class TripsRegistrationCount
    {
        public string tripName { get; set; }
        public int registeredCount { get; set; }
    }

    public class RegistrationByDateCount
    {
        public string date { get; set; }
        public int registeredCount { get; set; }
    }
}
