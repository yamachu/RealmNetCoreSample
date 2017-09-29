using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Realms;
using Realms.Sync;
using RealmNetCoreSample.Models;
using System.Reactive.Subjects;

namespace RealmSyncClinetLibrary.SyncLib
{
    public class SharedModelSync
    {
        private Realm _realm;
        private IDisposable annoucementToken;
        private Subject<int> annouceSub;
        public IObservable<int> AnnounceObservable {
            get { return annouceSub; }
        }

        private SharedModelSync(){ }

        public async static Task<SharedModelSync> Initialize(string user, string password, string domain, uint port, string dbUrl)
        {
            var modelSync = new SharedModelSync();
            var userCredential = Credentials.UsernamePassword(user, password, false);

            var _user = await Realms.Sync.User.LoginAsync(userCredential, new Uri($"http://{domain}:{port}/auth"));

            // For shared model
            var sharedDatabaseConfig = new SyncConfiguration(_user, new Uri(dbUrl));
            sharedDatabaseConfig.ObjectClasses = new Type[]{
                typeof(Announcement)
            };
            modelSync._realm = await Realm.GetInstanceAsync(sharedDatabaseConfig);
            modelSync.annouceSub = new Subject<int>();

            modelSync.annoucementToken = modelSync._realm.All<Announcement>().SubscribeForNotifications((sender, changes, error) =>
            {
                modelSync.annouceSub.OnNext(0);
            });

            return modelSync;
        }

        public IQueryable<Announcement> GetAnnouncement()
        {
            return _realm.All<Announcement>();
        }
    }
}
