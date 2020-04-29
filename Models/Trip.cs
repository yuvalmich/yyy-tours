using System;
namespace yyy_tours.Models
{
    public class Trip
    {

        public string Id { get; set; }
        public string DisplayName { get; set; }
        public string PlaceId { get; set; }
        public string GuideEmail { get; set; }
        public int Price { get; set; }
        public int Capacity { get; set; }
        public DateTime Date { get; set; }


        public Trip()
        {
        }
    }
}
