using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace yyytours.Models
{
    public class Trip
    {

        [Key]
        public string ID { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        [ForeignKey("Place")]
        public string PlaceId { get; set; }
        public Place Place { get; set; }
        [ForeignKey("Guide")]
        public string GuideId { get; set; }
        public User Guide { get; set; }
        public int Price { get; set; }
        public int Capacity { get; set; }
        public DateTime Date { get; set; }
        public int TimeInHours { get; set; }


        public Trip()
        {
        }

        public string GetTripDayInHebrew
        {
            get
            {
                string[] days = { "ראשון", "שני", "שלישי", "רביעי", "חמישי", "שישי", "שבת" };
                return days[(int)this.Date.DayOfWeek];
            }
        }

        public DateTime GetEndDateTime
        {
            get
            {
                return this.Date.AddHours(this.TimeInHours);
            }
        }

        public TimeSpan GetTimeToStart
        {
            get
            {
                return this.Date.Date - DateTime.Today.Date;
            }
        }
    }
}