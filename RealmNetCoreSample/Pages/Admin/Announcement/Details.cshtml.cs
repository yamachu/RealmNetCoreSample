using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RealmNetCoreSample.Services;
using Realms;

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

        public IActionResult OnGet(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var config = _context.GetSharedConfiguration();
            var realm = Realm.GetInstance(config);
            realm.Refresh();
            Announcement = realm.Find<Models.Announcement>(id);

            if (Announcement == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
