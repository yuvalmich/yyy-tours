using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace yyytours.Models
{
    public class Trip
    {

        [Key]
        public string ID { get; set; }
        [Display(Name = "שם")]
        public string DisplayName { get; set; }
        [Display(Name = "תיאור")]
        public string Description { get; set; }
        [Display(Name = "מיקום")]
        [ForeignKey("Place")]
        public string PlaceId { get; set; }
        [Display(Name = "מיקום")]
        public Place Place { get; set; }
        [ForeignKey("Guide")]
        [Display(Name = "מדריך")]
        public string GuideId { get; set; }
        [Display(Name = "מדריך")]
        public User Guide { get; set; }
        [Display(Name = "מחיר")]
        public int Price { get; set; }
        [Display(Name = "תפוסה")]
        public int Capacity { get; set; }
        [Display(Name = "תאריך ושעה")]
        public DateTime Date { get; set; }
        [Display(Name = "זמן הטיול בשעות")]
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