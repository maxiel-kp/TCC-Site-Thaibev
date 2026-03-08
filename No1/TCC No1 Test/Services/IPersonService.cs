using TCC_No1_Test.Entities;

namespace TCC_No1_Test.Service
{
    public interface IPersonService
    {
        public Task<List<Person>> GetList(int page);
        public Task<Person?> GetById(Guid id);
        public Task<Person> AddPerson(Person value);
    }
}