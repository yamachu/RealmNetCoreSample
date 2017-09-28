using System;
using System.Threading.Tasks;
using RealmSyncClinetLibrary.APIClient;
using RealmSyncClinetLibrary.SyncLib;

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

            var shared = await SharedModelSync.Initialize("hoge@hoge", "fuga", "localhost", 9080, dbUrl);
            shared.AnnounceObservable.Subscribe((_) => {
                foreach (var an in shared.GetAnnouncement())
                {
                    Console.WriteLine($"{an.Title}: {an.Body}");
                }
            });
            var annou = shared.GetAnnouncement();
            foreach (var an in annou) {
                Console.WriteLine($"{an.Title}: {an.Body}");
            }

            Console.WriteLine("Press finish");
            Console.ReadLine();
        }
    }
}
