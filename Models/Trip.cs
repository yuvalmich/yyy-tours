using System;
namespace yyy_tours.Models
{
    public class Trip
    {

        public string Id { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public string PlaceId { get; set; }
        public Place Place { get; set; }
        public string GuideEmail { get; set; }
        public User Guide { get; set; } 
        public int Price { get; set; }
        public int Capacity { get; set; }
        public DateTime Date { get; set; }
        public int TimeInHours { get; set; }


        public Trip()
        {
        }

        public string GetTripDayInHebrew()
        {
            string[] days = {"ראשון", "שני", "שלישי", "רביעי", "חמישי", "שישי", "שבת"};
            return days[(int)this.Date.DayOfWeek];
        }

        public DateTime GetEndDateTime()
        {
            return this.Date.AddHours(this.TimeInHours);
        }
    }
}
