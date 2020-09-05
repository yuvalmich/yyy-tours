using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace yyytours.Models
{
    public class HomeViewModel
    {
        public IEnumerable<FacebookPost> FacebookPosts { get; set; }
        public IEnumerable<Tuple<Place, int>> RecomendedPlaces { get; set; }
    }
}