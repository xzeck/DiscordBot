using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.EventArgs;
using Newtonsoft.Json;
using DiscordBot.commands;
using DSharpPlus.Interactivity;
 


namespace DiscordBot
{
    class Bot
    {
        public DiscordClient Client { get; private set; }

        public CommandsNextModule Commands { get; private set; }
     
        public async Task MainAsync(string[] args)
        {
            /*var json = string.Empty;

            using (var fs = File.OpenRead("config.json"))
            using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                json = await sr.ReadToEndAsync().ConfigureAwait(false);*/


            var configJson = ReadConfig.config;


            Client = new DiscordClient(new DiscordConfiguration
            {
                Token = configJson.Token,
                TokenType = TokenType.Bot,
                AutoReconnect = true,
                UseInternalLogHandler = true,
                LogLevel = LogLevel.Debug,
                
            });


            var commandsConfig = new CommandsNextConfiguration
            {
                StringPrefix = configJson.Prefix,
                EnableMentionPrefix = true,
                EnableDms = false,
                CaseSensitive = false,
                IgnoreExtraArguments = true,
            };

            Commands = Client.UseCommandsNext(commandsConfig);            

            Commands.RegisterCommands<Music>();
            Commands.RegisterCommands<RandomStuff>();
            Commands.RegisterCommands<Anime>();
            Commands.RegisterCommands<PollClass>();
            Commands.RegisterCommands<SuggestMovie>();
            Commands.RegisterCommands<ToDoClass>();

            await Client.ConnectAsync();

            Client.Ready += OnClientReady;

            Client.UseInteractivity(new InteractivityConfiguration
            {
                Timeout = TimeSpan.FromMinutes(2)
            });

            await Task.Delay(-1);
            
        }

        private Task OnClientReady(ReadyEventArgs e)
        {
            
            return Task.CompletedTask;
        }



    }
}
