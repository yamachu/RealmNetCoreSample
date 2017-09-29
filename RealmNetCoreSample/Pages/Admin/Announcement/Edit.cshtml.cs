using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RealmNetCoreSample.Services;
using Realms;

namespace RealmNetCoreSample.Pages.Admin.Announcement
{
    public class EditModel : PageModel
    {
        private readonly IRealmProviderService _context;

        public EditModel(IRealmProviderService context)
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

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var config = _context.GetSharedConfiguration();
            var realm = Realm.GetInstance(config);
            realm.Refresh();
            realm.Write(() =>
            {
                Announcement.ModifiedAt = System.DateTimeOffset.Now;
                realm.Add(Announcement, true);
            });

            return RedirectToPage("./Index");
        }
    }
}
