using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class Movie
    {
        public int id { get; set; }
        public string title { get; set; }

        public string overview { get; set; }

        public string release_date { get; set; }

        public string backdrop_path { get; set; }

        public List<Genre> genres { get; set; } = new List<Genre>();

        public List<Credits> Credits { get; set; } = new List<Credits>();

        public string Poster_Path { get; set; }
    }
}
