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

        private static List<Place> allPlaces = new List<Place>
        {
            new Place()
                {
                    Id = "mesada",
                    Name = "מצדה",
                    Description = "מבצר עתיק על פסגתו של צוק מבודד, בשוליו המזרחיים של מדבר יהודה, המתנאשא לגובה של 63 מטרים מעל פני הים, וכ-450 מטרים מעל ים המלח שלמרגלותיו",
                    ImageUrl = "https://img.oastatic.com/img2/15281261/max/besucher-in-masada.jpg",
                    GoogleMapEmbededUrl = "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d13634.96476502167!2d35.371785313395826!3d31.31089758714479!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x150305b8cab1b3e1%3A0x3850c0aaa6a68544!2z157XpteT15Q!5e0!3m2!1siw!2sil!4v1588782761948!5m2!1siw!2sil"
                },
                new Place()
                {
                    Id = "jerusalem",
                    Name = "ירושלים",
                    Description = "אלפי עמודים נכתבו על ירושלים ועדין הם מעטים מדי כדי להכיל את יופיה וקדושתה. ירושלים היא מקום שניתן לבלות בו שנים ולא למצותו. העיר העתיקה על סמטאותיה, עיר דוד, הרובע היהודי, עין כרם, שוק מחנה יהודה ועוד מהווים רק חלק קטן משיש לעיר להציע.",
                    ImageUrl = "https://www.touristisrael.com/wp-content/uploads/210910570-4.jpg",
                    GoogleMapEmbededUrl = "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d108513.70308822999!2d35.245399358753616!3d31.79629942856135!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x1502d7d634c1fc4b%3A0xd96f623e456ee1cb!2z15nXqNeV16nXnNeZ150!5e0!3m2!1siw!2sil!4v1591037595852!5m2!1siw!2sil"
                },
                new Place()
                {
                    Id = "telaviv",
                    Name = "תל אביב",
                    Description = "תל אביב, העיר העברית הראשונה, הפכה בשנים האחרונות ליעד אטרקטיבי במיוחד לתיירים מסביב לכל העולם וכמובן מכל רחבי הארץ. תל אביב הינה \"עיר ללא הפסקה\" המאגדת בתוכה כל כך הרבה גורמי משיכה - מהיסטוריה ומורשת אל מפגשי תרבויות מרתקים לצד אופנת רחוב וכמובן סצנה קולינרית מהמובילות בעולם.",
                    ImageUrl = "https://lh3.ggpht.com/p/AF1QipNxN5z3CL0sQi3q9aB1Rhf2EQG7_kT2NAoTqM0=s1536",
                    GoogleMapEmbededUrl = "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d108169.91309721212!2d34.86728635054896!3d32.0879122326561!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x151d4ca6193b7c1f%3A0xc1fb72a2c0963f90!2z16rXnCDXkNeR15nXkSDXmdek15U!5e0!3m2!1siw!2sil!4v1591125060220!5m2!1siw!2sil"
                }
            
        };
        private static List<Trip> allTrips = new List<Trip> 
        {
            new Trip()
            {
                Id = "1122",
                DisplayName = "טיול של בוקר למצדה",
                Description = "בתחילת הטיפוס שלכם לאורך שביל הנחש (זמן טיפוס משוער כ45 דקות )– השמש תתחיל לזרוח ותוכלו להנות מהנוף המרהיב. אחרי שתגיעו לפסגה תוכלו לחקור את האתר הארכיאולוגי שנבנה על ידי המלך הרודוס",
                PlaceId = "mesada",
                Place = allPlaces.First(i=>i.Id == "mesada"),
                GuideEmail = "aa@yyy.com",
                Guide = new User(){
                    FullName = "יוסי פורטוגזי",
                    Type = UserType.Guide
                },
                Price = 100,
                Capacity = 20,
                Date = new DateTime(2020,5,14,4,30,0), // 14/5/20 04:30
                TimeInHours = 6
            },
            new Trip()
            {
                Id = "3344",
                DisplayName = "סיור סגווי בעיר העתיקה",
                Description = "את הסיור נתחיל משדרת החנויות המרהיבה ממילא, נעלה על הסגווי ונתקדם לאורך חומות העיר העתיקה עד לכניסה לתוך הרובע הנוצרי. משם נתקדם למגדל דוד ושער יפו. נלמד אודות החשיבות של שני האתרים ולאחר מכן נרחף לעבר הרובע הארמני ושער ציון, ממנו נביט לעבר הר ציון והאתרים ההיסטוריים שמקיפים אותו. לקינוח, נעצור בבית הכנסת החורבה ובקארדו לחוויה לימודית מרתקת לפני שנחזור לממילא.",
                PlaceId = "jerusalem",
                Place = allPlaces.First(i=>i.Id == "jerusalem"),
                GuideEmail = "aa@yyy.com",
                Guide = new User(){
                    FullName = "יוסי פורטוגזי",
                    Type = UserType.Guide
                },
                Price = 100,
                Capacity = 20,
                Date = new DateTime(2020,6,30,9,30,0), // 14/5/20 04:30
                TimeInHours = 2
            },
            new Trip()
            {
                Id = "3344",
                DisplayName = "סיור סגווי בעיר העתיקה",
                Description = "את הסיור נתחיל משדרת החנויות המרהיבה ממילא, נעלה על הסגווי ונתקדם לאורך חומות העיר העתיקה עד לכניסה לתוך הרובע הנוצרי. משם נתקדם למגדל דוד ושער יפו. נלמד אודות החשיבות של שני האתרים ולאחר מכן נרחף לעבר הרובע הארמני ושער ציון, ממנו נביט לעבר הר ציון והאתרים ההיסטוריים שמקיפים אותו. לקינוח, נעצור בבית הכנסת החורבה ובקארדו לחוויה לימודית מרתקת לפני שנחזור לממילא.",
                PlaceId = "jerusalem",
                Place = allPlaces.First(i=>i.Id == "jerusalem"),
                GuideEmail = "aa@yyy.com",
                Guide = new User(){
                    FullName = "יוסי פורטוגזי",
                    Type = UserType.Guide
                },
                Price = 200,
                Capacity = 20,
                Date = new DateTime(2020,6,30,9,30,0), // 14/5/20 04:30
                TimeInHours = 2
            },
            new Trip()
            {
                Id = "5566",
                DisplayName = "סיור במתחם שרונה",
                Description = "במהלך הסיור בשרונה נהלך בין בתי הטמפלרים, מבני המשק והמתקנים החקלאיים. נכיר דמויות כמו האלמנה הנחשקת של המושבה, הרוקח היהודי ובעיקר נשמע הרבה סיפורים על המושבה הגרמנית, הבסיס הצבאי הבריטי, קריית השלטון הישראלי וכל מה שביניהם... נסיים במתחם \"שרונה מרקט\" ונטעם מדוכנים של שפים צעירים שהגיעו לנסות את מזלם בשוק האוכל החדש.",
                PlaceId = "telaviv",
                Place = allPlaces.First(i=>i.Id == "telaviv"),
                GuideEmail = "aa@yyy.com",
                Guide = new User(){
                    FullName = "יוסי פורטוגזי",
                    Type = UserType.Guide
                },
                Price = 120,
                Capacity = 50,
                Date = new DateTime(2020,6,30,14,00,0),
                TimeInHours = 3
            }
            
        };
        public IActionResult TripDetails(string id)
        {
            Trip requested = allTrips.FirstOrDefault(i=>i.Id == id);
            if(requested != null)
            {
                return View("TripDetails", requested);
            }
            else
            {
                return NotFound($"not found trip id = {id}");
            }
        }
    }
}