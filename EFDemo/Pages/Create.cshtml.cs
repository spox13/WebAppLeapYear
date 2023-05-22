using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using EFDemo.Data;
using EFDemo.Models;
using Newtonsoft.Json;

namespace EFDemo.Pages
{
    public class CreateModel : PageModel
    {
        private readonly EFDemo.Data.ApplicationDbContext _context;

        public CreateModel(EFDemo.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Person Person { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Person == null || Person == null)
            {
                return Page();
            }

            Person.CreatedDate = DateTime.Now;

            if (Person.Year % 4 == 0 && (Person.Year % 100 != 0 || Person.Year % 400 == 0))
            {
                Person.Result = "rok przestępny";
            }
            else
            {
                Person.Result = "rok nieprzestępny";
            }

            string message = string.Empty;

            if (!string.IsNullOrEmpty(Person.Name) && Person.Year >= 1899 && Person.Year <= 2022)
            {
                message = $"{Person.Name} urodził się w {Person.Year} roku. Był to {Person.Result}";
            }
            else if (Person.Year >= 1899 && Person.Year <= 2022)
            {
                message = $"{Person.Year} rok. Był to {Person.Result}";
            }

            if (!string.IsNullOrEmpty(message))
            {
                ViewData["Message"] = message;

                _context.Person.Add(Person);
                await _context.SaveChangesAsync();
            }

            return Page();
        }
    }
}
