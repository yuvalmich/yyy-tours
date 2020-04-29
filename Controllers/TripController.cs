using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using yyy_tours.Models;

namespace yyy_tours.Controllers
{
    public class TripController : Controller
    {
        public IActionResult TripDetails(string id)
        {
            if(id == "1122"){
            Trip demo = new Trip()
            {
                Id = "1122",
                DisplayName = "טיול של בוקר למצדה",
                Description = "בתחילת הטיפוס שלכם לאורך שביל הנחש (זמן טיפוס משוער כ45 דקות )– השמש תתחיל לזרוח ותוכלו להנות מהנוף המרהיב. אחרי שתגיעו לפסגה תוכלו לחקור את האתר הארכיאולוגי שנבנה על ידי המלך הרודוס",
                PlaceId = "mesada",
                GuideEmail = "aa@yyy.com",
                Price = 100,
                Capacity = 20,
                Date = new DateTime(2020,5,14,4,30,0), // 14/5/20 04:30
                TimeInHours = 6
            };
            return View("TripDetails", demo);
            }
            else
            {
                return NotFound($"not found trip id = {id}");
            }
        }
    }
}