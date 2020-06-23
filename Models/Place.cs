using System;
using System.ComponentModel.DataAnnotations;

namespace yyytours.Models
{
    public class Place
    {
        [Key]
        public string ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; } 
        public string Country { get; set; }   

        public Place()
        {
        }
    }
}