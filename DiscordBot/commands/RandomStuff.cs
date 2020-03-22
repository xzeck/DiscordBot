using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity; 


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

    }
}
