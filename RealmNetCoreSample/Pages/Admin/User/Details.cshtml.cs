using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RealmNetCoreSample.Services;

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

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var realm = _context.GetAdminInstance();
            User = realm.Find<Models.User>(id);

            if (User == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
