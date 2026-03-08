using TCC_No1_Test.Entities;
using TCC_No1_Test.Repository;

namespace TCC_No1_Test.Service
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _repository;

        public PersonService(IPersonRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Person>> GetList()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Person?> GetById(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Person> AddPerson(Person value)
        {
            return await _repository.AddAsync(value);
        }
    }
}
