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
using EFDemo.Services;

namespace EFDemo.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration Configuration;
        private readonly ILogger<IndexModel> _logger;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ILeapYearInterface _personService;

        public IndexModel(ILeapYearInterface personService, IConfiguration configuration, ILogger<IndexModel> logger, IHttpContextAccessor contextAccessor)
        {
            _personService = personService;
            Configuration = configuration;
            _logger = logger;
            _contextAccessor = contextAccessor;
        }
        public Person object_toSearch { get; set; } = new Person();
        public string NameSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
        public PaginatedList<Person> Person { get; set; }

        public async Task OnGetAsync(string sortOrder, string searchString, string currentFilter, int? pageIndex)
        {
            if (_contextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                var user_id = _contextAccessor.HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
                object_toSearch.user_id = user_id.Value;
            }

            CurrentSort = sortOrder == "Date" ? "date_desc" : "Date";

            var people = await _personService.GetAllPeopleAsync();

            people = people.OrderByDescending(s => s.CreatedDate).ToList();


            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }


            var peopleQueryable = people.AsQueryable();
            var pageSize = Configuration.GetValue("PageSize", 20);
            Person = await PaginatedList<Person>.CreateAsync(people, pageIndex ?? 1, pageSize);
        }
    }
}
