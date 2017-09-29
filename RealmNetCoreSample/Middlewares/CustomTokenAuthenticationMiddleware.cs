using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Http;
using RealmNetCoreSample.Models;
using RealmNetCoreSample.Services;
using Realms;

namespace RealmNetCoreSample.Middlewares
{
    public class CustomTokenAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IRealmProviderService RealmService;

        public CustomTokenAuthenticationMiddleware(RequestDelegate next, IRealmProviderService realmService)
        {
            _next = next;
            RealmService = realmService;
        }

        public async Task Invoke(HttpContext context)
        {
            string authHeader = context.Request.Headers["X-AuthToken"];

            if (authHeader == null)
            {
                context.Response.StatusCode = 401;
                return;
            }

            var config = RealmService.GetAdminConfiguration();
            var realm = Realm.GetInstance(config);
            var authUser = realm.All<User>().FirstOrDefault(user => user.AccessToken.Equals(authHeader, StringComparison.OrdinalIgnoreCase));

            if (authUser != default(User))
            {
                await _next.Invoke(context);
            }
            else
            {
                context.Response.StatusCode = 401;
                return;
            }
        }
    }
}
