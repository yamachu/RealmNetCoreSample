using System;
using System.Threading.Tasks;
using RealmSyncClinetLibrary.APIClient;

namespace RealmSyncClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            // すでに作ってあるなら不要
            var created = await APIServerHttpClient.PostUserCreate("hoge@hoge", "fuga");
            if (!created)
            {
               Console.WriteLine("Create Post failed");
               return;
            }

            var accessToken = APIServerHttpClient.GenerateAccessTokenFromNamePass("hoge@hoge", "fuga");
            var client = new APIServerHttpClient(accessToken);

            var dbUrl = await client.GetDatabaseConfig();

            Console.WriteLine(dbUrl);
        }
    }
}
