using TCC_No1_Test.Entities;

namespace TCC_No1_Test.Repository
{
    public interface IPersonRepository
    {
        Task<List<Person>> GetAllAsync(int page);

        Task<Person?> GetByIdAsync(Guid id);

        Task<Person> AddAsync(Person person);
    }
}