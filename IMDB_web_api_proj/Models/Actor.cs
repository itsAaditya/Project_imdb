using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMDB_web_api_proj.Models
{
    public class Actor
    {
        public int actor_id { get; set; }

        public string actor_name { get; set; }

        public string actor_bio { get; set; }

        public string gender { get; set; }
    }
}
