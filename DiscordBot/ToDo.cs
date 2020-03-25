using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json; 


namespace DiscordBot
{
    internal class ToDo
    {
        [JsonProperty("User")]
        public string User { get; set; }

        [JsonProperty("Todo")]
        public List<string> Todo { get; set; }
    }
}
