using Microsoft.EntityFrameworkCore;
using TCC_No1_Test.Data;
using TCC_No1_Test.Entities;

namespace TCC_No1_Test.Repository
{
    public class PersonRepository : IPersonRepository
    {
        private readonly AppDbContext _dbContext;

        public PersonRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Person>> GetAllAsync()
        {
            return await _dbContext.Persons.ToListAsync();
        }

        public async Task<Person?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Persons.FindAsync(id);
        }

        public async Task<Person> AddAsync(Person person)
        {
            person.Id = Guid.NewGuid();
            await _dbContext.Persons.AddAsync(person);
            await _dbContext.SaveChangesAsync();
            return person;
        }
    }
}
