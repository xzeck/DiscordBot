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
        /// <summary>
        /// Returns a random number into the discord channel
        /// </summary>
        /// <param name="ctx">Context</param>
        /// <param name="a">Min Value</param>
        /// <param name="b">Max Value</param>
        /// <returns></returns>
        [Command("Random")]
        public async Task rnd(CommandContext ctx, int a, int b)
        {
            Random random = new Random();

            await ctx.Channel.SendMessageAsync($"Hey {ctx.User.Mention}, your random number is "
                + random.Next(a, b).ToString()).ConfigureAwait(false);

        }

        /// <summary>
        /// Says back whatever the user typed after invoking the command
        /// </summary>
        /// <param name="ctx">Context</param>
        /// <returns></returns>
        [Command("SayBack")]
        public async Task SayBack(CommandContext ctx)
        {
            var interactivity = ctx.Client.GetInteractivityModule();

            var message = await interactivity.WaitForMessageAsync(x => x.Channel == ctx.Channel).ConfigureAwait(false);

            await ctx.Channel.SendMessageAsync(message.Message.Content);

        }

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

        [Command("SayIt")]
        public async Task SayIt(CommandContext ctx)
        {
            await ctx.RespondAsync("That's what she said");
        }

    }
}
