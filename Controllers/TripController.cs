using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OpenWeatherMap.Standard;
using OpenWeatherMap.Standard.Enums;
using yyytours;
using yyytours.Models;
using Microsoft.AspNetCore.Http;
using System.Net;

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
            if (getSessionUserType() != UserType.Admin)
            {
                Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return View("Error", new ErrorViewModel {ErrorDescription = "אינך מורשה לגשת לעמוד זה"});
            }
                
            var yyyWebProjContext = _context.Trip.Include(t => t.Guide).Include(t => t.Place);
            return View(await yyyWebProjContext.ToListAsync());
        }

        // GET: Trip/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                return View("Error", new ErrorViewModel {ErrorDescription = "טיול זה לא נמצא", ControllerToLink="Trip", ActionToLink=nameof(Catalog), TextToLink="חזרה לקטלוג הטיולים"});
            }

            var trip = await _context.Trip
                .Include(t => t.Guide)
                .Include(t => t.Place)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (trip == null)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                return View("Error", new ErrorViewModel {ErrorDescription = "טיול זה לא נמצא", ControllerToLink="Trip", ActionToLink=nameof(Catalog), TextToLink="חזרה לקטלוג הטיולים"});
            }

            Current currentWeather = new Current("5572d59340b8fe8f0f32b4f5f6e2d57b", WeatherUnits.Metric);
            var weatherData = await currentWeather.GetWeatherDataByCityName(trip.Place.Name);
            if(weatherData != null)
            {
                trip.Place.Wethear = weatherData.WeatherDayInfo.Temperature;
            }
            return View(trip);
        }
        public async Task<IActionResult> Register(string tripID, string userEmail)
        {
            if(tripID == null || tripID == "" || userEmail == null || userEmail.Length == 0)
            {
                return BadRequest();
            }

            var registeredUser = await _context.TripRegistration
                .Where(tr => tr.TripId == tripID).Where(tr => tr.UserEmail == userEmail).ToListAsync();

            if(registeredUser.Count != 0)
            {
                return View("../TripRegistration/RegistrationExist");
            }

            var tripReg = new TripRegistration();
            tripReg.ID = Guid.NewGuid().ToString();
            tripReg.TripId = tripID;
            tripReg.UserEmail = userEmail;
            tripReg.RegistrationDateTime = DateTime.Now;

            _context.Add(tripReg);
            await _context.SaveChangesAsync();

            return View("../TripRegistration/RegistrationSuccess");
        }
        public async Task<IActionResult> Catalog(string searchString)
    {
        IQueryable<Trip> trips = _context.Trip
                .Include(t => t.Guide)
                .Include(t => t.Place)
                .Where(i=>i.Date > DateTime.Now).OrderBy(i=>i.Date);

        if (!String.IsNullOrEmpty(searchString))
        {
            var split = searchString.Split(' ');
            foreach (var word in split)
            {
                trips = trips.Where(i=> i.DisplayName.Contains(word) || i.Place.Name.Contains(word)
                || i.Guide.FullName.Contains(word));
            }
            
        }
        return View("catalog", await trips.ToListAsync());
    }

        // GET: Trip/Create
        public IActionResult Create()
        {
            if (getSessionUserType() != UserType.Admin)
            {
                Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return View("Error", new ErrorViewModel {ErrorDescription = "אינך מורשה לגשת לעמוד זה"});
            }

            ViewData["GuideId"] = new SelectList(_context.User.Where(i=> i.Type == UserType.Guide), "Email", "FullName");
            ViewData["PlaceId"] = new SelectList(_context.Place, "ID", "Name");
            return View();
        }

        // POST: Trip/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,DisplayName,Description,PlaceId,GuideId,Price,Date,TimeInHours")] Trip trip)
        {
            if (getSessionUserType() != UserType.Admin)
            {
                Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return View("Error", new ErrorViewModel {ErrorDescription = "אינך מורשה לגשת לעמוד זה"});
            }

            trip.ID = Guid.NewGuid().ToString();

            if (ModelState.IsValid)
            {
                _context.Add(trip);
                await _context.SaveChangesAsync();

                string[] postMessage = {
                    trip.DisplayName,
                    "תיאור  הטיול: " + trip.Description,
                    "למידע נוסף והרשמה  לטיול: " + "http://localhost:4000/trip/Details/" + trip.ID
                };
                await FacebookApi.CreateFacebookPost(postMessage);

                return RedirectToAction(nameof(Index));
            }
            ViewData["GuideId"] = new SelectList(_context.User, "Email", "FullName");
            ViewData["PlaceId"] = new SelectList(_context.Place, "ID", "Name");
            return View(trip);
        }

        // GET: Trip/Edit/5
        public async Task<IActionResult> Edit(string id)
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

            var trip = await _context.Trip.FindAsync(id);
            if (trip == null)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                return View("Error", new ErrorViewModel {ErrorDescription = "טיול זה לא נמצא", ControllerToLink="Trip", ActionToLink=nameof(Index), TextToLink="חזרה לרשימת הטיולים"});
            }

            ViewData["GuideId"] = new SelectList(_context.User.Where(i=> i.Type == UserType.Guide), "Email", "FullName");
            ViewData["PlaceId"] = new SelectList(_context.Place, "ID", "Name");
            return View(trip);
        }

        // POST: Trip/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ID,DisplayName,Description,PlaceId,GuideId,Price,Date,TimeInHours")] Trip trip)
        {
            if (getSessionUserType() != UserType.Admin)
            {
                Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return View("Error", new ErrorViewModel {ErrorDescription = "אינך מורשה לגשת לעמוד זה"});
            }

            if (id != trip.ID)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                return View("Error", new ErrorViewModel {ErrorDescription = "טיול זה לא נמצא", ControllerToLink="Trip", ActionToLink=nameof(Index), TextToLink="חזרה לרשימת הטיולים"});
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
                        Response.StatusCode = (int)HttpStatusCode.NotFound;
                        return View("Error", new ErrorViewModel {ErrorDescription = "טיול זה לא נמצא", ControllerToLink="Trip", ActionToLink=nameof(Index), TextToLink="חזרה לרשימת הטיולים"});
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["GuideId"] = new SelectList(_context.User, "Email", "FullName");
            ViewData["PlaceId"] = new SelectList(_context.Place, "ID", "Name");
            return View(trip);
        }

        // GET: Trip/Delete/5
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

            var trip = await _context.Trip
                .Include(t => t.Guide)
                .Include(t => t.Place)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (trip == null)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                return View("Error", new ErrorViewModel {ErrorDescription = "טיול זה לא נמצא", ControllerToLink="Trip", ActionToLink=nameof(Index), TextToLink="חזרה לרשימת הטיולים"});
            }

            return View(trip);
        }

        // POST: Trip/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (getSessionUserType() != UserType.Admin)
            {
                Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return View("Error", new ErrorViewModel {ErrorDescription = "אינך מורשה לגשת לעמוד זה"});
            }
                
            var trip = await _context.Trip.FindAsync(id);
            _context.Trip.Remove(trip);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TripExists(string id)
        {
            return _context.Trip.Any(e => e.ID == id);
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
