using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System.Net.Http;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq; 

namespace DiscordBot.commands
{
    class SuggestMovie
    {
        private string Key; 

        [Command("suggestmovie")]
        public async Task suggestmovie(CommandContext ctx)
        {
           
            var configJson = ReadConfig.config;

            Key = configJson.Key;

            JObject jsonObject;
            HttpClient client = new HttpClient();
            do
            {
                int MovieID = getRandom();
                var APILink = string.Format("http://www.omdbapi.com/?i=tt{0}&apikey={1}&type=movie", MovieID, Key);
                Console.WriteLine(APILink);

                var movie_content = await client.GetStringAsync(APILink);

                 jsonObject = JObject.Parse(movie_content);

            } while (jsonObject["Response"].ToString() == "False");
            

            string Desc = "Duration :clock1:    : {0}\n\n" +
                          "Score    :star:      : {1}\n\n" +
                          "Rating   :underage:  : {2}\n";

            var MovieDesc = string.Format(Desc, jsonObject["Runtime"], jsonObject["imdbRating"], jsonObject["Rated"]);

            DiscordEmbed embed; 
            if(!(jsonObject["Poster"].ToString() == "N/A"))
            {
                embed = new DiscordEmbedBuilder
                {
                    Title = jsonObject["Title"].ToString(),
                    Description = MovieDesc,
                    ImageUrl = jsonObject["Poster"].ToString()
                };
            }
            else
            {
                embed = new DiscordEmbedBuilder
                {
                    Title = jsonObject["Title"].ToString(),
                    Description = MovieDesc,
                };
            }
            

            await ctx.RespondAsync(ctx.User.Mention, embed: embed);
            
        }

        int getRandom()
        {
            Random rnd = new Random();
            int MovieID = rnd.Next(1, 9000560);

            return MovieID;
        }
    }
}
