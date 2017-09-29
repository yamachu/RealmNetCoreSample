using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RealmNetCoreSample.Services;
using Realms;

namespace RealmNetCoreSample.Pages.Admin.User
{
    public class DetailsModel : PageModel
    {
        private readonly IRealmProviderService _context;

        public DetailsModel(IRealmProviderService context)
        {
            _context = context;
        }

        public Models.User User { get; set; }

        public IActionResult OnGet(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var config = _context.GetAdminConfiguration();
            var realm = Realm.GetInstance(config);
            realm.Refresh();
            User = realm.Find<Models.User>(id);

            if (User == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
