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
                    Id = "Masada",
                    Name = "מצדה",
                    Description = "מבצר עתיק על פסגתו של צוק מבודד, בשוליו המזרחיים של מדבר יהודה, המתנאשא לגובה של 63 מטרים מעל פני הים, וכ-450 מטרים מעל ים המלח שלמרגלותיו",
                    ImageUrl = "https://img.oastatic.com/img2/15281261/max/besucher-in-masada.jpg",
                    Country = "Israel"
                },
                new Place()
                {
                    Id = "Jerusalem",
                    Name = "ירושלים",
                    Description = "אלפי עמודים נכתבו על ירושלים ועדין הם מעטים מדי כדי להכיל את יופיה וקדושתה. ירושלים היא מקום שניתן לבלות בו שנים ולא למצותו. העיר העתיקה על סמטאותיה, עיר דוד, הרובע היהודי, עין כרם, שוק מחנה יהודה ועוד מהווים רק חלק קטן משיש לעיר להציע.",
                    ImageUrl = "https://www.touristisrael.com/wp-content/uploads/210910570-4.jpg",
                    Country = "Israel"
                },
                new Place()
                {
                    Id = "Tel Aviv",
                    Name = "תל אביב",
                    Description = "תל אביב, העיר העברית הראשונה, הפכה בשנים האחרונות ליעד אטרקטיבי במיוחד לתיירים מסביב לכל העולם וכמובן מכל רחבי הארץ. תל אביב הינה \"עיר ללא הפסקה\" המאגדת בתוכה כל כך הרבה גורמי משיכה - מהיסטוריה ומורשת אל מפגשי תרבויות מרתקים לצד אופנת רחוב וכמובן סצנה קולינרית מהמובילות בעולם.",
                    ImageUrl = "https://lh3.ggpht.com/p/AF1QipNxN5z3CL0sQi3q9aB1Rhf2EQG7_kT2NAoTqM0=s1536",
                    Country = "Israel"
                }
            
        };
        private static List<Trip> allTrips = new List<Trip> 
        {
            new Trip()
            {
                Id = "1122",
                DisplayName = "טיול של בוקר למצדה",
                Description = "בתחילת הטיפוס שלכם לאורך שביל הנחש (זמן טיפוס משוער כ45 דקות )– השמש תתחיל לזרוח ותוכלו להנות מהנוף המרהיב. אחרי שתגיעו לפסגה תוכלו לחקור את האתר הארכיאולוגי שנבנה על ידי המלך הרודוס",
                PlaceId = "Masada",
                Place = allPlaces.First(i=>i.Id == "Masada"),
                GuideEmail = "aa@yyy.com",
                Guide = new User(){
                    FullName = "יוסי פורטוגזי",
                    Type = UserType.Guide
                },
                Price = 100,
                Capacity = 20,
                Date = new DateTime(2020,6,3,4,30,0), // 14/5/20 04:30
                TimeInHours = 6
            },
            new Trip()
            {
                Id = "3344",
                DisplayName = "סיור סגווי בעיר העתיקה",
                Description = "את הסיור נתחיל משדרת החנויות המרהיבה ממילא, נעלה על הסגווי ונתקדם לאורך חומות העיר העתיקה עד לכניסה לתוך הרובע הנוצרי. משם נתקדם למגדל דוד ושער יפו. נלמד אודות החשיבות של שני האתרים ולאחר מכן נרחף לעבר הרובע הארמני ושער ציון, ממנו נביט לעבר הר ציון והאתרים ההיסטוריים שמקיפים אותו. לקינוח, נעצור בבית הכנסת החורבה ובקארדו לחוויה לימודית מרתקת לפני שנחזור לממילא.",
                PlaceId = "Jerusalem",
                Place = allPlaces.First(i=>i.Id == "Jerusalem"),
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
                PlaceId = "Jerusalem",
                Place = allPlaces.First(i=>i.Id == "Jerusalem"),
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
                PlaceId = "Tel Aviv",
                Place = allPlaces.First(i=>i.Id == "Tel Aviv"),
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

        public IActionResult Catalog()
    {
        var trips = allTrips.Where(i=>i.Date > DateTime.Now).OrderBy(i=>i.Date);
        return View("catalog", trips);
    }
    }
}