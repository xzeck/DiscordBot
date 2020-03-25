using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace DiscordBot
{
    class PushToFirebase
    {

        IFirebaseClient Client; 

        IFirebaseConfig config = new FirebaseConfig
        {
            BasePath = ReadConfig.config.DBURL,
            AuthSecret = ReadConfig.config.Secret
        };



        public PushToFirebase()
        {
            Client = new FireSharp.FirebaseClient(config);
        }

        public async Task<bool> Insert(ToDo data)
        {
            Console.WriteLine(data.User);

            string todoString = String.Join(" ", data.Todo.ToArray());

            try
            {
            
                FirebaseResponse response = await Client.PushAsync("/" + data.User, todoString);
            
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return true;

        }

        public async Task<List<string>> Retrieve(string User)
        {
            List<string> TodoList = new List<string>();
            JObject json = new JObject();
            string TodoString = null;
            try
            {

                FirebaseResponse response = await Client.GetAsync("/" + User);
                //todoString = response.Body.ToString();
                json = JObject.Parse(response.Body);

                IList<string> Keys = json.Properties().Select(p => p.Name).ToList();

                foreach (string Key in Keys)
                {
                    TodoString = TodoString + json[Key].ToString() + "\n\n";
                    TodoList.Add(json[Key].ToString());
                }


            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.WriteLine(TodoList.Count);
            
            return TodoList;

        }

        public async Task<bool> Delete(string User)
        {
            try
            {

            FirebaseResponse response = await Client.DeleteAsync("/" + User);

            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return true;
        }


    }
}
