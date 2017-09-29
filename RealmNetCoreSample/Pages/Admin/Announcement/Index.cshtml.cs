using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RealmNetCoreSample.Services;
using Realms;

namespace RealmNetCoreSample.Pages.Admin.Announcement
{
    public class IndexModel : PageModel
    {
        private readonly IRealmProviderService _context;

        public IndexModel(IRealmProviderService context)
        {
            _context = context;
        }

        public IList<Models.Announcement> Announcement { get; set; }

        public void OnGet()
        {
            var config = _context.GetSharedConfiguration();
            var realm = Realm.GetInstance(config);
            realm.Refresh();
            Announcement = realm.All<Models.Announcement>()
                                .OrderBy(announce => announce.ModifiedAt)
                                .ThenBy(annouce => annouce.CreatedAt)
                                .ToList();
        }
    }
}
