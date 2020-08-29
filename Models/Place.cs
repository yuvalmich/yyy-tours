using System;
using System.ComponentModel.DataAnnotations;

namespace yyytours.Models
{
    public class Place
    {
        [Key]
        public string ID { get; set; }
        [Display(Name = "שם")]
        public string Name { get; set; }
        [Display(Name = "תיאור")]
        public string Description { get; set; }
        [Display(Name = "כתובת לתמונה")]
        public string ImageUrl { get; set; } 
        [Display(Name = "מדינה")]
        public string Country { get; set; }   

        public Place()
        {
        }
    }
}