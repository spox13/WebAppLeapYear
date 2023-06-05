using EFDemo.Data;
using EFDemo.Interfaces;
using EFDemo.Models;

namespace EFDemo.Repository
{
    public class LeapYearRepository : ILeapYearRepository
    {
        private readonly ApplicationDbContext _context;
        public LeapYearRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public IQueryable<Person> GetActivePeople()
        {
            return _context.Person;
        }
    }
}
