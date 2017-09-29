using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RealmNetCoreSample.Services;
using Realms;

namespace RealmNetCoreSample.Pages.Admin.Announcement
{
    public class CreateModel : PageModel
    {
        private readonly IRealmProviderService _context;

        public CreateModel(IRealmProviderService context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Models.Announcement Announcement { get; set; }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var config = _context.GetSharedConfiguration();
            var realm = Realm.GetInstance(config);
            realm.Write(() =>
            {
                realm.Add(Announcement);
            });

            return RedirectToPage("./Index");
        }
    }
}