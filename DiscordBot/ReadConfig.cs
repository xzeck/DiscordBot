using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace DiscordBot
{
    static class ReadConfig
    {
        public static ConfigJson config { get; private set; }

        internal static void Read()
        {
            var json = string.Empty;

            using (var fs = File.OpenRead("config.json"))
            using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                json = sr.ReadToEnd();

            config = JsonConvert.DeserializeObject<ConfigJson>(json);

        }
    }
}
