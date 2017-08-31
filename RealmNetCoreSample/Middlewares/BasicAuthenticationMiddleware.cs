using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace RealmNetCoreSample.Middlewares
{
    /// <summary>
    /// Basic authentication middleware.
    /// </summary>
    /// <see cref="http://stackoverflow.com/questions/38977088/asp-net-core-web-api-authentication"/>
    public class BasicAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public BasicAuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            string authHeader = context.Request.Headers["Authorization"];
            if (authHeader != null && authHeader.StartsWith("Basic", StringComparison.CurrentCulture))
            {
                //Extract credentials
                string encodedUsernamePassword = authHeader.Substring("Basic ".Length).Trim();
                Encoding encoding = Encoding.GetEncoding("iso-8859-1");
                string usernamePassword = encoding.GetString(Convert.FromBase64String(encodedUsernamePassword));

                int seperatorIndex = usernamePassword.IndexOf(':');

                var username = usernamePassword.Substring(0, seperatorIndex);
                var password = usernamePassword.Substring(seperatorIndex + 1);

                if (username == "admin" && password == "password")
                {
                    await _next.Invoke(context);
                }
                else
                {
                    context.Response.StatusCode = 401; //Unauthorized
                    return;
                }
            }
            else
            {
                // http://tnakamura.hatenablog.com/entry/2017/07/06/aspnetcore-basic-authentication
                context.Response.Headers["WWW-Authenticate"] = "Basic";
                context.Response.StatusCode = 401; //Unauthorized
                return;
            }
        }
    }
}