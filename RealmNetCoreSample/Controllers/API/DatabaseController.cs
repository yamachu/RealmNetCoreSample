using System;
using Microsoft.AspNetCore.Mvc;
using Realms.Sync;
using RealmNetCoreSample.Services;
using System.Threading.Tasks;

namespace RealmNetCoreSample.Controllers.API
{
    [Route("/api/[controller]")]
    public class DatabaseController : Controller
    {
        private readonly IRealmProviderService RealmService;

        public DatabaseController(IRealmProviderService realmService)
        {
            RealmService = realmService;
        }

        [HttpGet]
        [Route("config")]
        public async Task<JsonResult> GetDatabaseConfig()
        {
            var realm = RealmService.GetSharedInstance();
            var session = realm.GetSession();

            var realmUri = session.ServerUri;

            return Json(new
            {
                server = realmUri.ToString()
            });
        }
    }
}
