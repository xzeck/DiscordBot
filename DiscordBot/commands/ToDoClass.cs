using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using Newtonsoft.Json.Linq;
using System.IO;
using Newtonsoft.Json;


namespace DiscordBot.commands
{
    class ToDoClass
    {
        PushToFirebase PTF = new PushToFirebase();

        private List<string> EmojiNames = new List<string>()
        {
            ":zero: ", 
            ":one: ",
            ":two: ",
            ":three: ",
            ":four: ",
            ":five: ",
            ":six: ",
            ":seven: ",
            ":eight: ",
            ":nine: "
        };


        [Command("Todo")]
        public async Task Todo(CommandContext ctx, params string[] Todo)
        {
            ToDo todo = new ToDo
            {
                User = ctx.User.Mention,
                Todo = Todo.ToList<string>()
            };


            await PTF.Insert(todo);

            await ctx.RespondAsync($"{ctx.User.Mention} your todo list is updated!");
        }

        [Command("GetTodo")]
        public async Task GetTodo(CommandContext ctx)
        {
            List<string> todo = await PTF.Retrieve(ctx.User.Mention);

            
            if(Object.ReferenceEquals(null, todo))
            {
                await ctx.RespondAsync($"{ctx.User.Mention} You have no todos, to create a new list type ?Todo <todo>");

            }
            else
            {
                Console.WriteLine("GetTodo");
                int count = todo.Count;
                string emoji = string.Empty;
                int rem; 

                for(int i =0; i<count; i++)
                {
                    int j = i+1; 

                    while(j!= 0)
                    {
                        rem = j % 10;
                        emoji = (EmojiNames[rem] + emoji);
                        j = j / 10;
                    }

                    todo[i] = emoji + todo[i] + "\n\n";
                    emoji = string.Empty;
                }

                string Desc = string.Join(",", todo);
                Desc = Desc.Replace(",", "");



                DiscordEmbed embed = new DiscordEmbedBuilder
                {
                    Title = "Your Todo List",
                    Description = Desc,
                    
                };

                await ctx.RespondAsync(ctx.User.Mention, embed: embed);
            }
        }

        [Command("Clear")]
        public async Task Clear(CommandContext ctx)
        {
            _ = PTF.Delete(ctx.User.Mention);

            await ctx.RespondAsync($"{ctx.User.Mention} your Todo list is cleared!");
        }
    }
}
