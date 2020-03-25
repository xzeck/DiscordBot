using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DiscordBot
{
    public struct ConfigJson
    {
        [JsonProperty("token")]
        public string Token { get; private set; }

        [JsonProperty("prefix")]
        public string Prefix { get; private set; }

        [JsonProperty("key")]
        public string Key { get; private set; }

        [JsonProperty("DBRUL")]
        public string DBURL { get; private set; }

        [JsonProperty("Secret")]
        public string Secret { get; private set; }
    }
}
