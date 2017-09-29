using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RealmNetCoreSample.Services;
using Realms;

namespace RealmNetCoreSample.Pages.Admin.User
{
    public class IndexModel : PageModel
    {
        private readonly IRealmProviderService _context;

        public IndexModel(IRealmProviderService context)
        {
            _context = context;
        }

        public IList<Models.User> User { get;set; }

        public void OnGet()
        {
            var config = _context.GetAdminConfiguration();
            var realm = Realm.GetInstance(config);
            realm.Refresh();
            User = realm.All<Models.User>().OrderBy(user => user.CreatedAt).ToList();
        }
    }
}
