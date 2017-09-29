using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RealmNetCoreSample.Services;
using Realms;

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

        public IActionResult OnGetA(string id)
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

        public IActionResult OnPost(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var config = _context.GetAdminConfiguration();
            var realm = Realm.GetInstance(config);
            realm.Refresh();
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
