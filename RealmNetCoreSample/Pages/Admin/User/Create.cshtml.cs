using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RealmNetCoreSample.Services;
using Realms;

namespace RealmNetCoreSample.Pages.Admin.User
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
        public Models.User User { get; set; }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var config = _context.GetAdminConfiguration();
            var realm = Realm.GetInstance(config);
            realm.Write(() =>
            {
                // Todo: 
                // - Password に不正な文字が入っていないかのバリデーション
                // -- User モデルにバリデーションのアトリビュートを追加するのがいいかもしれない
                // - Hash にする
                // -- DI していい感じに
                realm.Add(User);
            });

            return RedirectToPage("./Index");
        }
    }
}