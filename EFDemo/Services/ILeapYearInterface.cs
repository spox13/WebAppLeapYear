using EFDemo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EFDemo.Services
{
    public interface ILeapYearInterface
    {
        Task<List<Person>> GetAllPeopleAsync();
        Task<Person> GetPersonByIdAsync(int id);
        Task CreatePersonAsync(Person person);
        Task UpdatePersonAsync(Person person);
        Task DeletePersonAsync(int id);
    }
}
