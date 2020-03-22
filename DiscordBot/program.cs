using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot
{
    class program
    {
        static void Main(string[] args)
        {
            var bot = new Bot();

            bot.MainAsync(args).GetAwaiter().GetResult();
        }
    }
}
