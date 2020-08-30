using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using yyytours.Controllers;

namespace yyytours.Models
{
    public class TripRegistration
    {
        [Key]
        public string ID { get; set; }
        [ForeignKey("Trip")]
        public string TripId { get; set; }
        [ForeignKey("User")]
        public string UserEmail { get; set; }
        public DateTime RegistrationDateTime { get; set; }

        
        public TripRegistration() { }


    }
}
