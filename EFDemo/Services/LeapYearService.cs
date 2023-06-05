using EFDemo.Models;
using EFDemo.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EFDemo.Interfaces;
using EFDemo.ViewModels;
using ContosoUniversity;

namespace EFDemo.Services
{
    public class LeapYearService : ILeapYearService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILeapYearRepository _personRepository;

        public LeapYearService(ApplicationDbContext context,
            ILeapYearRepository personRepository)
        {
            _context = context;
            _personRepository = personRepository;
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

        public ListPersonForListVM GetPeopleForList()
        {
            var people = _personRepository.GetActivePeople();
            ListPersonForListVM result = new ListPersonForListVM();
            result.People = new List<PersonForListVM>();
            foreach (var person in people)
            {
                var pVM = new PersonForListVM()
                {
                    Id = person.Id,
                    Name = person.Name,
                    Year = person.Year,
                    Result = person.Result,
                    CreatedDate = person.CreatedDate,
                    user_id = person.user_id,
                    login = person.login
                };
                result.People.Add(pVM);
            }
            return result;
        }
    }
}
