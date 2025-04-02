using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DAO
{
    public class GenreListResponse { 
        public List<Genre> Genres { get; set; } = new List<Genre>();
    }
}
