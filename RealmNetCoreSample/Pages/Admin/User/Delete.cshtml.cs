using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RealmNetCoreSample.Services;

namespace RealmNetCoreSample.Pages.Admin.User
{
    public class DeleteModel : PageModel
    {
        private readonly IRealmProviderService _context;

        public DeleteModel(IRealmProviderService context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var realm = _context.GetAdminInstance();
            User = realm.Find<Models.User>(id);

            if (User != null)
            {
                using (var trans = realm.BeginWrite())
                {
                    realm.Remove(User);
                    trans.Commit();
                }
            }

            return RedirectToPage("./Index");
        }
    }
}
