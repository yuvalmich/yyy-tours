using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using yyytours.Models;
using Microsoft.EntityFrameworkCore;

namespace yyytours.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly yyyWebProjContext _context;

        public HomeController(ILogger<HomeController> logger, yyyWebProjContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {

            var posts = await FacebookApi.GetPagePosts();
            var recommenedePlaces = CreatePlacesStatistics().Take(5);
            return View(new HomeViewModel {FacebookPosts = posts, RecomendedPlaces = recommenedePlaces});
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {  });
        }

        private IEnumerable<Tuple<Place, int>> CreatePlacesStatistics()
        {
            var recommenedeTripsIds = from i in _context.TripRegistration
                orderby i.RegistrationDateTime descending
                group i by i.TripId into tripRegs
                orderby tripRegs.Count() descending
                select tripRegs.Key;
             var recommenedeTripsIdsLst = recommenedeTripsIds.ToList();
             var recommenedePlaces = _context.Trip.Include(t => t.Place)
             .Where(i=>recommenedeTripsIds.Contains(i.ID)).ToList()
             .OrderBy(i=> recommenedeTripsIdsLst.IndexOf(i.ID))
             .GroupBy(i=>i.Place)
             .OrderByDescending(byPlace => byPlace.Count())
             .Select(byPlace => new Tuple<Place, int>(byPlace.Key, byPlace.Where(i=>i.Date >= DateTime.Now).Count()));

            return recommenedePlaces;
        }
    }
}
