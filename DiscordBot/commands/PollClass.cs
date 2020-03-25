using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;


namespace DiscordBot.commands
{
    class PollClass
    {
        [Command("Poll")]
        public async Task Poll(CommandContext ctx, string PollName, TimeSpan duration, params string[] UserOptions)
        {
            var interactivity = ctx.Client.GetInteractivityModule();
            var options = UserOptions.Select(x => x.ToString());


            for (var x = 0; x < UserOptions.Length; x++)
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

            for (var x = 0; x < UserOptions.Length; x++)
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
