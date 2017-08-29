using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RealmNetCoreSample.Models;
using RealmNetCoreSample.Services;

namespace RealmNetCoreSample.Controllers.API
{
    [Route("/api/[controller]")]
    public class UserController : Controller
    {
        private readonly IRealmProviderService RealmService;
        private readonly IPasswordHashService PasswordService;

        public UserController(IRealmProviderService realmService, IPasswordHashService passwordService)
        {
            RealmService = realmService;
            PasswordService = passwordService;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> PostCreateUser([FromBody] User user)
        {
            if (!ModelState.IsValid || user.Name.Contains(":") || user.Password.Contains(":"))
            {
                return BadRequest(ModelState);
            }

            user.Password = PasswordService.GetHashedString(user.Password);
            user.AccessToken = PasswordService.GenerateAccessToken(user.Name, user.Password);

            var adminRealm = await RealmService.GetAdminInstanceAsync();
            adminRealm.Write(() =>
            {
                adminRealm.Add(user);
            });
            return StatusCode(201);
        }
    }
}
