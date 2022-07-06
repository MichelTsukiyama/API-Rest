using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestWithASPNET.Model;
using RestWithASPNET.Services;

namespace RestWithASPNET.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly ILogger<PersonController> _logger;
        private readonly IPersonService _personService;

        public PersonController(ILogger<PersonController> logger, IPersonService personService)
        {
            _logger = logger;
            _personService = personService;
        }

        [HttpGet]
        public ActionResult Get()
        {
            return Ok(_personService.FindAll());
        }

        [HttpGet("{id}")]
        public ActionResult Get(long id)
        {
            var person = _personService.FindById(id);
            if(person is null)
                return NotFound();

            return Ok(person);
        }

        [HttpPost]
        public ActionResult Post([FromBody] Person person)
        {
            if(person is null)
                return BadRequest();

            return Ok(_personService.Create(person));
        }

        [HttpPut]
        public ActionResult Put([FromBody] Person person)
        {
            if(person is null)
                return BadRequest();

            return Ok(_personService.Update(person));
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(long id)
        {
            _personService.Delete(id);          
            return NoContent();
        }
    }
}