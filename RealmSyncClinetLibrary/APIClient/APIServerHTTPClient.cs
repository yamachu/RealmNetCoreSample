using System;
using System.Collections;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using RealmNetCoreSample.Services;

namespace RealmSyncClinetLibrary.APIClient
{
    public class APIServerHttpClient
    {
        private HttpClient HttpClient;
        private static PasswordHashService PasswordService = new PasswordHashService();

        public APIServerHttpClient(string accessToken)
        {
            HttpClient = new HttpClient(new APIServerHttpClientHandler(accessToken))
            {
                BaseAddress = new Uri("http://localhost:5001"),
            };
        }

        public async Task<string> GetDatabaseConfig()
        {
            var response = await HttpClient.GetAsync("/api/database/config").ConfigureAwait(false);
            var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            var json = JsonConvert.DeserializeObject<DatabaseConfigModel>(responseBody);

            return json.Server;
        }

        public static string GenerateAccessTokenFromNamePass(string userName, string password)
        {
            var hashedPass = PasswordService.GetHashedString(password);

            return PasswordService.GenerateAccessToken(userName, hashedPass);
        }

        public static async Task<bool> PostUserCreate(string userName, string password)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5001")
            };

            var contentBody = new
            {
                name = userName,
                password = password,
            };

            var content = JsonConvert.SerializeObject(contentBody);

            var response = await client.PostAsync("/api/user/create",
                                                  new StringContent(content, Encoding.UTF8, "application/json"))
                                       .ConfigureAwait(false);

            var created = (int)response.StatusCode;

            return created == 201;
        }

        internal class DatabaseConfigModel
        {
            public string Server { get; set; }
        }
    }
}
