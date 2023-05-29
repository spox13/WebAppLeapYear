using EFDemo.Data;
using EFDemo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EFDemo.Services
{
    public class ILeapYearService : ILeapYearInterface
    {
        private readonly ApplicationDbContext _context;

        public ILeapYearService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Person>> GetAllPeopleAsync()
        {
            return await _context.Person.ToListAsync();
        }

        public async Task<Person> GetPersonByIdAsync(int id)
        {
            return await _context.Person.FindAsync(id);
        }

        public async Task CreatePersonAsync(Person person)
        {
            _context.Person.Add(person);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePersonAsync(Person person)
        {
            _context.Person.Update(person);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePersonAsync(int id)
        {
            var person = await _context.Person.FindAsync(id);
            if (person != null)
            {
                _context.Person.Remove(person);
                await _context.SaveChangesAsync();
            }
        }
    }
}
