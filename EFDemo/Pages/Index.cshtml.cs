using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EFDemo.Data;
using EFDemo.Models;
using ContosoUniversity;
using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace EFDemo.Pages
{
    public class IndexModel : PageModel
    {
        private readonly EFDemo.Data.ApplicationDbContext _context;
        private readonly IConfiguration Configuration;
        private readonly ILogger<IndexModel> _logger;
        private readonly IHttpContextAccessor _contextAccessor;

        public IndexModel(EFDemo.Data.ApplicationDbContext context, IConfiguration configuration, ILogger<IndexModel> logger, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            Configuration = configuration;
            _logger = logger;
            _contextAccessor = contextAccessor;
        }
        public Person object_toSearch { get; set; } = new Person();
        public string NameSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
        public PaginatedList<Person> Person { get; set; } = default!;

        public async Task OnGetAsync(string sortOrder, string searchString, string currentFilter, int? pageIndex)
        {
            if (_contextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                var user_id = _contextAccessor.HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
                object_toSearch.user_id = user_id.Value;
            }

            CurrentSort = sortOrder == "Date" ? "date_desc" : "Date";

            IQueryable<Person> personIQ = from s in _context.Person select s;


            personIQ = personIQ.OrderByDescending(s => s.CreatedDate);


            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            

            var pageSize = Configuration.GetValue("PageSize", 20);
            Person = await PaginatedList<Person>.CreateAsync(personIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}
