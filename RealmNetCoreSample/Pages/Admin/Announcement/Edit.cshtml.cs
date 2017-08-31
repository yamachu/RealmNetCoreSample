using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RealmNetCoreSample.Services;

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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var realm = _context.GetSharedInstance();
            realm.Write(() =>
            {
                Announcement.ModifiedAt = System.DateTimeOffset.Now;
                realm.Add(Announcement, true);
            });

            return RedirectToPage("./Index");
        }
    }
}
