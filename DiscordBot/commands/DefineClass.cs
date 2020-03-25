using System;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.IO;
using Newtonsoft.Json;
using RestSharp;

namespace DiscordBot.commands
{
    class DefineClass
    {
        [Command("Define")]
        public async Task Define(CommandContext ctx, string Query)
        {
            var json = string.Empty;
            using (var fs = File.OpenRead("config.json"))
            using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                json = await sr.ReadToEndAsync().ConfigureAwait(false);
            var configJson = JsonConvert.DeserializeObject<ConfigJson>(json);
            var client = new RestClient("https://wordsapiv1.p.rapidapi.com/words/hatchback/typeOf");
            var request = new RestRequest(Method.GET);
            request.AddHeader("x-rapidapi-host", "wordsapiv1.p.rapidapi.com");
            request.AddHeader("x-rapidapi-key", configJson.Key);
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
        }
    }
}
