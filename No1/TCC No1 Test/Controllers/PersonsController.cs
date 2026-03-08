using Microsoft.AspNetCore.Mvc;
using TCC_No1_Test.Entities;
using TCC_No1_Test.Service;

namespace TCC_No1_Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly IPersonService _service;
        public PersonsController(IPersonService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<List<Person>> GetList()
        {
            return await _service.GetList();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var person = await _service.GetById(id);

            if (person == null) return NotFound();

            return Ok(person);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Person value)
        {
            if (value == null) return BadRequest();
            var person = await _service.AddPerson(value);
            return Ok(person.Id);
        }
    }
}
