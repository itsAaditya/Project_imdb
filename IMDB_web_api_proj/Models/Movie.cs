using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMDB_web_api_proj.Models
{
    public class Movie
    {
        //movie_id movie_name plot release_date actor_id producer_id poster
        public int movie_id { get; set; }

        public string movie_name { get; set; }

        public string plot { get; set; }

        public string release_date { get; set; }

        public int actor_id { get; set; }

        public int producer_id { get; set; }

        public string poster { get; set; }

    }
}
