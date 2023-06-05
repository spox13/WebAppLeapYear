using EFDemo.Models;

namespace EFDemo.Interfaces
{
    public interface ILeapYearRepository
    {
        IQueryable<Person> GetActivePeople();
    }
}
