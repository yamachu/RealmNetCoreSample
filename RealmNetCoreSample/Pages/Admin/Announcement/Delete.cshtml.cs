using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RealmNetCoreSample.Services;
using Realms;

namespace RealmNetCoreSample.Pages.Admin.Announcement
{
    public class DeleteModel : PageModel
    {
        private readonly IRealmProviderService _context;

        public DeleteModel(IRealmProviderService context)
        {
            _context = context;
        }

        [BindProperty]
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

        public IActionResult OnPost(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var config = _context.GetSharedConfiguration();
            var realm = Realm.GetInstance(config);
            realm.Refresh();
            Announcement = realm.Find<Models.Announcement>(id);

            if (Announcement != null)
            {
                using (var trans = realm.BeginWrite())
                {
                    realm.Remove(Announcement);
                    trans.Commit();
                }
            }

            return RedirectToPage("./Index");
        }
    }
}
