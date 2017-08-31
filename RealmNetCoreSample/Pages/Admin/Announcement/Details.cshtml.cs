using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RealmNetCoreSample.Services;

namespace RealmNetCoreSample.Pages.Admin.Announcement
{
    public class DetailsModel : PageModel
    {
        private readonly IRealmProviderService _context;

        public DetailsModel(IRealmProviderService context)
        {
            _context = context;
        }

        public Models.Announcement Announcement { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var realm = _context.GetSharedInstance();
            Announcement = realm.Find<Models.Announcement>(id);

            if (Announcement == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
