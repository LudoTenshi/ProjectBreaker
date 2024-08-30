using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ProjectBreaker
{
    public class LevelManager
    {
        [JsonProperty]
        public int? Type { get; set; }
        [JsonProperty]
        public int? L { get; set; }
        [JsonProperty]
        public int? C { get; set; }
        public LevelManager() 
        {

        }
    }
}
