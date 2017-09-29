using System;
using System.Threading.Tasks;
using Realms;
using Realms.Sync;

namespace RealmNetCoreSample.Services
{
    public interface IRealmProviderService
    {
        SyncConfiguration GetAdminConfiguration();
        SyncConfiguration GetSharedConfiguration();
    }

    public class RealmProviderService : IRealmProviderService
    {
        private SyncConfiguration SharedRealmConfiguration;
        private SyncConfiguration AdminRealmConfiguration;

        private string DatabaseServerDomain;
        private uint DatabaseServerPort;

        public RealmProviderService(/* 環境変数からもらったりするように */)
        {
            DatabaseServerDomain = "localhost";
            DatabaseServerPort = 9080;

            // 初回起動など，ユーザを作っていなければ 3つめの引数 を true に
            var adminCredential = Credentials.UsernamePassword("admin@admin", "P@ssword", false);

            Task.Run(async () =>
            {
                var adminUser = await Realms.Sync.User.LoginAsync(adminCredential,
                                                      new Uri($"http://{DatabaseServerDomain}:{DatabaseServerPort}/auth"));

                // For admin
                var adminDatabaseConfig = new SyncConfiguration(adminUser, new Uri($"realm://{DatabaseServerDomain}:{DatabaseServerPort}/~/Accounts"));
                adminDatabaseConfig.ObjectClasses = new Type[]{
                    typeof(RealmNetCoreSample.Models.User)
                };
                AdminRealmConfiguration = adminDatabaseConfig;

                // For shared model
                var sharedDatabaseConfig = new SyncConfiguration(adminUser, new Uri($"realm://{DatabaseServerDomain}:{DatabaseServerPort}/~/Shared"));
                sharedDatabaseConfig.ObjectClasses = new Type[]{
                    typeof(RealmNetCoreSample.Models.Announcement)
                };
                SharedRealmConfiguration = sharedDatabaseConfig;
                await Realm.GetInstanceAsync(SharedRealmConfiguration);

                // Shared の Realm は読み取り専用で全ユーザが見れるように
                // これを await すると返ってこなくなる...
                adminUser.ApplyPermissionsAsync(
                    PermissionCondition.UserId("*"),
                    $"realm://{DatabaseServerDomain}:{DatabaseServerPort}/~/Shared",
                    AccessLevel.Read);
            }).Wait();
        }

        public SyncConfiguration GetAdminConfiguration()
        {
            return AdminRealmConfiguration;
        }


        public SyncConfiguration GetSharedConfiguration()
        {
            return SharedRealmConfiguration;
        }
    }
}
