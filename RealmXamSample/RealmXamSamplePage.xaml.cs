using Xamarin.Forms;
using RealmSyncClinetLibrary.APIClient;
using RealmSyncClinetLibrary.SyncLib;

namespace RealmXamSample
{
    public partial class RealmXamSamplePage : ContentPage
    {
        public RealmXamSamplePage()
        {
            InitializeComponent();
            Setup();
        }

        async private void Setup()
        {
            var accessToken = APIServerHttpClient.GenerateAccessTokenFromNamePass("hoge@hoge", "fuga");
            var client = new APIServerHttpClient(accessToken);

            var dbUrl = await client.GetDatabaseConfig();

            var shared = await SharedModelSync.Initialize("hoge@hoge", "fuga", "localhost", 9080, dbUrl);

            var announce = shared.GetAnnouncement();
            this.BindingContext = announce;
        }
    }
}
