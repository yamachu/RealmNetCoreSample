using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RealmNetCoreSample.Contexts;
using RealmNetCoreSample.Models;
using RealmNetCoreSample.Services;

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

        public async Task OnGetAsync()
        {
            var realm = _context.GetAdminInstance();
            User = realm.All<Models.User>().OrderBy(user => user.CreatedAt).ToList();
        }
    }
}
