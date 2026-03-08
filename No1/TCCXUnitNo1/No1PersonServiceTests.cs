using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using TCC_No1_Test.Entities;
using TCC_No1_Test.Repository;
using TCC_No1_Test.Service;

namespace TCCXUnitNo1
{
    public class No1PersonServiceTests
    {
        private readonly Mock<IPersonRepository> _repositoryMock;
        private readonly PersonService _service;

        public No1PersonServiceTests()
        {
            _repositoryMock = new Mock<IPersonRepository>();
            _service = new PersonService(_repositoryMock.Object);
        }

        [Fact]
        public async Task GetList()
        {
            // Arrange
            _repositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<Person>
            {
                new Person { Id = Guid.NewGuid(), FirstName = "Jane", LastName = "Smith", Address = "456 Elm St", BirthDate = new DateTime(1985, 5, 15), CreatedAt = DateTime.UtcNow }
            });

            // Act
            var person = await _service.GetList();

            // Assert
            Assert.NotNull(person);
        }

        [Fact]
        public async Task GetById()
        {
            // Arrange
            var personId = Guid.NewGuid();
            var person = new Person { Id = personId, FirstName = "Jane", LastName = "Smith", Address = "456 Elm St", BirthDate = new DateTime(1985, 5, 15), CreatedAt = DateTime.UtcNow };

            _repositoryMock.Setup(repo => repo.GetByIdAsync(personId)).ReturnsAsync(person);

            // Act
            var result = await _service.GetById(personId);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task AddPerson()
        {
            // Arrange
            var person = new Person
            {
                Id = Guid.NewGuid(),
                FirstName = "Max",
                LastName = "Maxiel",
                Address = "Bangkok",
                BirthDate = new DateTime(1999, 9, 5),
            };

            _repositoryMock
                .Setup(x => x.AddAsync(It.IsAny<Person>()))
                .ReturnsAsync(person);

            // Act
            var result = await _service.AddPerson(person);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Max", result.FirstName);

            _repositoryMock.Verify(x => x.AddAsync(It.IsAny<Person>()), Times.Once);
        }

    }
}
