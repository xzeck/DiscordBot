using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes; 


namespace DiscordBot.commands
{
    public class Music 
    {
        [Command("Hello")]
        [Description("Greets the user")]
        public async Task Hello(CommandContext ctx)
        {
            await ctx.RespondAsync($"Hey, there {ctx.User.Mention}");
        }
    }
}
