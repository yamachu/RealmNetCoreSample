using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RealmNetCoreSample.Services;

namespace RealmNetCoreSample.Pages.Admin.User
{
    public class EditModel : PageModel
    {
        private readonly IRealmProviderService _context;

        public EditModel(IRealmProviderService context)
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var realm = _context.GetAdminInstance();
            realm.Write(() =>
            {
                realm.Add(User, true);
            });

            return RedirectToPage("./Index");
        }
    }
}
