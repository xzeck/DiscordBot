using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using JikanDotNet;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using DSharpPlus.CommandsNext.Converters;


namespace DiscordBot.commands
{
    public class RandomStuff
    {      

        [Command("Random")]
        public async Task rnd(CommandContext ctx, int a, int b)
        {
            Random random = new Random();

            await ctx.Channel.SendMessageAsync($"Hey {ctx.User.Mention}, your random number is "
                + random.Next(a, b).ToString()).ConfigureAwait(false);

        }

        [Command("SayBack")]
        public async Task SayBack(CommandContext ctx)
        {
            var interactivity = ctx.Client.GetInteractivityModule();

            var message = await interactivity.WaitForMessageAsync(x => x.Channel == ctx.Channel).ConfigureAwait(false);

            await ctx.Channel.SendMessageAsync(message.Message.Content);

        }

        [Command("Poll")]
        public async Task Poll(CommandContext ctx, string PollName, TimeSpan duration, params string[] UserOptions)
        {
            var interactivity = ctx.Client.GetInteractivityModule();
            var options = UserOptions.Select(x => x.ToString());

            
            for(var x = 0; x < UserOptions.Length; x++)
            {
                char alphabet = (char)(97 + x);

                string emoji = ":regional_indicator_" + alphabet.ToString() + ": ";

                UserOptions[x] = emoji + UserOptions[x];
            }

            var embed = new DiscordEmbedBuilder
            {
                Title = PollName,
                Description = string.Join("\n\n", UserOptions)
            };

            var msg = await ctx.RespondAsync(embed: embed);

            List<DiscordEmoji> reactions = new List<DiscordEmoji>(); 
            
            for(var x = 0; x < UserOptions.Length; x++)
            {
                char alphabet = (char)(97 + x);

                string emoji = ":regional_indicator_" + alphabet.ToString() + ":";
                reactions.Add(DiscordEmoji.FromName(client: ctx.Client, emoji));

                await msg.CreateReactionAsync(reactions[x]);
            }
            
            var poll_result = await interactivity.CollectReactionsAsync(msg, duration);

            var results = poll_result.Reactions.Where(xkvp => options.Contains(xkvp.Key))
                .Select(xkvp => $"{xkvp.Value} : {xkvp.Value}");


            await ctx.RespondAsync(string.Join("\n\n", results));

        }

        [Command("SuggestAnime")]
        public async Task SuggestAnime(CommandContext ctx)
        {
           


            IJikan jikan = new Jikan();
            Anime RandomAnime = null;
            int AnimeID = 0;

            Console.WriteLine("SuggestAnime");
            do
            {
                AnimeID = getRandom();

                Task<Anime> task = jikan.GetAnime(AnimeID);
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

/*
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
        }*/

        [Command("Love")]
        public async Task Love(CommandContext ctx, string User)
        {
            await ctx.RespondAsync($"{ctx.User.Mention} loves {User} :heart_eyes: :heart:");
        }

        [Command("Baka")]
        public async Task Baka(CommandContext ctx, string User)
        {
            await ctx.RespondAsync($"{User} is Baka - {ctx.User.Mention}");
        }

        [Command("Todo")]
        public async Task Todo(CommandContext ctx, string Todo)
        {
            string User = ctx.User.Mention;

            


            using (Stream fileStream = File.Open("todo.json", FileMode.Create))
                fileStream.Write(, 0,);


        }

        int getRandom()
        {
            Random rnd = new Random();
            int AnimeID = rnd.Next(1, 10502);

            return AnimeID;
        }

       

    }
}
