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
                Place = new Place()
                {
                    Id = "mesada",
                    Name = "מצדה",
                    Description = "מבצר עתיק על פסגתו של צוק מבודד, בשוליו המזרחיים של מדבר יהודה, המתנאשא לגובה של 63 מטרים מעל פני הים, וכ-450 מטרים מעל ים המלח שלמרגלותיו",
                    ImageUrl = "https://img.oastatic.com/img2/15281261/max/besucher-in-masada.jpg",
                    GoogleMapEmbededUrl = "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d13634.96476502167!2d35.371785313395826!3d31.31089758714479!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x150305b8cab1b3e1%3A0x3850c0aaa6a68544!2z157XpteT15Q!5e0!3m2!1siw!2sil!4v1588782761948!5m2!1siw!2sil"
                },
                GuideEmail = "aa@yyy.com",
                Guide = new User(){
                    FullName = "יוסי פורטוגזי",
                    Type = UserType.Guide
                },
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