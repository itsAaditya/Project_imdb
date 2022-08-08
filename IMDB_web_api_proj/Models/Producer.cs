using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMDB_web_api_proj.Models
{
    public class Producer
    {
        public int producer_id { get; set; }

        public string producer_name { get; set; }

        public string bio { get; set; }

        public string company { get; set; }

        public string gender { get; set; }
    }
}
