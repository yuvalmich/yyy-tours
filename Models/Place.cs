using System;
namespace yyy_tours.Models
{
    public class Place
    {

        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; } 
        public string GoogleMapEmbededUrl { get; set; }   

        public Place()
        {
        }
    }
}
