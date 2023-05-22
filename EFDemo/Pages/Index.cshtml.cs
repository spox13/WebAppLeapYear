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

namespace EFDemo.Pages
{
    public class IndexModel : PageModel
    {
        private readonly EFDemo.Data.ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public IndexModel(EFDemo.Data.ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public string NameSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
        public PaginatedList<Person> Person { get; set; } = default!;

        public async Task OnGetAsync(string sortOrder, string searchString, int? pageIndex)
        {
            CurrentSort = sortOrder;
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";

            IQueryable<Person> personIQ = from s in _context.Person select s;
            switch (sortOrder)
            {
                case "Date":
                    personIQ = personIQ.OrderBy(s => s.CreatedDate);
                    break;
                case "date_desc":
                    personIQ = personIQ.OrderByDescending(s => s.CreatedDate);
                    break;
                default:
                    personIQ = personIQ.OrderByDescending(s => s.CreatedDate);
                    break;
            }
            if (_context.Person != null)
            {
                var pageSize = _configuration.GetValue<int>("PageSize", 20);
                Person = await PaginatedList<Person>.CreateAsync(personIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
            }
        }
    }
}
