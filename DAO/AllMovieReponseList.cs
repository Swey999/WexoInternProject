using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class AllMovieReponseList
    {
        public List<Movie> results { get; set; } = new List<Movie>();
        public int total_results { get; set; }
    }
}
