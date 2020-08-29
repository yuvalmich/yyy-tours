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
        [Required(ErrorMessage = "שם אינו יכול להיות ריק")]
        public string DisplayName { get; set; }
        
        [Display(Name = "תיאור")]
        [Required(ErrorMessage = "תיאור אינו יכול להיות ריק")]
        public string Description { get; set; }
        [Display(Name = "מיקום")]
        [Required(ErrorMessage = "מיקום אינו יכול להיות ריק")]
        [ForeignKey("Place")]
        public string PlaceId { get; set; }
        [Display(Name = "מיקום")]
        [Required(ErrorMessage = "מיקום אינו יכול להיות ריק")]
        public Place Place { get; set; }
        [ForeignKey("Guide")]
        [Display(Name = "מדריך")]
        [Required(ErrorMessage = "מדריך אינו יכול להיות ריק")]
        public string GuideId { get; set; }
        [Display(Name = "מדריך")]
        [Required(ErrorMessage = "מדריך אינו יכול להיות ריק")]
        public User Guide { get; set; }
        [Display(Name = "מחיר")]
        [Required(ErrorMessage = "מחיר אינו יכול להיות ריק")]
        [Range(0, double.MaxValue)]
        public int Price { get; set; }
        [Display(Name = "תפוסה")]
        [Required(ErrorMessage = "תפוסה אינו יכול להיות ריק")]
        public int Capacity { get; set; }
        [Display(Name = "תאריך ושעה")]
        [Required(ErrorMessage = "תאריך ושעת הטיול אינו יכול להיות ריק")]
        public DateTime Date { get; set; }
        [Display(Name = "זמן הטיול בשעות")]
        [Required(ErrorMessage = "זמן הטיול בשעות אינו יכול להיות ריק")]
        [Range(0.5, 24)]
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