using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RealmNetCoreSample.Services;
using Realms;

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

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var config = _context.GetAdminConfiguration();
            var realm = Realm.GetInstance(config);
            realm.Refresh();
            realm.Write(() =>
            {
                realm.Add(User, true);
            });

            return RedirectToPage("./Index");
        }
    }
}
