using System;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using JikanDotNet;

namespace DiscordBot.commands
{
    class Anime
    {
        [Command("SuggestAnime")]
        public async Task SuggestAnime(CommandContext ctx)
        {

            IJikan jikan = new Jikan();
            JikanDotNet.Anime RandomAnime = null;
            int AnimeID = 0;

            Console.WriteLine("SuggestAnime");
            do
            {
                AnimeID = getRandom();

                Task<JikanDotNet.Anime> task = jikan.GetAnime(AnimeID);
                Task continutation = task.ContinueWith(t =>
                {
                    Console.WriteLine("Result : " + t.Result);
                    RandomAnime = t.Result;
                });
                continutation.Wait();

            } while (Object.ReferenceEquals(null, RandomAnime));


            string Desc = "Episodes :tv:        : {0}\n\n" +
                          "Duration :clock1:    : {1}\n\n" +
                          "Score    :star:      : {2}\n\n" +
                          "Rating   :underage:  : {3}\n";


            var AnimeDesc = string.Format(Desc, RandomAnime.Episodes,
                RandomAnime.Duration, RandomAnime.Score, RandomAnime.Rating);


            var embed = new DiscordEmbedBuilder
            {
                Title = RandomAnime.Title,
                Description = AnimeDesc,
                ImageUrl = RandomAnime.ImageURL,
            };

            await ctx.RespondAsync(ctx.User.Mention, embed: embed);
        }

        int getRandom()
        {
            Random rnd = new Random();
            int AnimeID = rnd.Next(1, 10502);

            return AnimeID;
        }

    }
}
