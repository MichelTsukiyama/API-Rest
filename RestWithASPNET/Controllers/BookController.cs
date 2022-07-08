using Microsoft.AspNetCore.Mvc;
using RestWithASPNET.Business;
using RestWithASPNET.Data.VO;
using RestWithASPNET.Model;

namespace RestWithASPNET.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class BookController : ControllerBase
    {
        public readonly IBookBusiness _bookBusiness;

        public BookController(IBookBusiness bookBusiness)
        {
            _bookBusiness = bookBusiness;
        }

        [HttpGet]
        public ActionResult Get()
        {
            return Ok(_bookBusiness.FindAll());
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var book = _bookBusiness.FindById(id);

            if(book is null)
                return NotFound();
            
            return Ok(book);
        }

        [HttpPost]
        public ActionResult Post([FromBody] BookVO book)
        {
            if(book is null)
                return BadRequest();

            return Ok(_bookBusiness.Create(book));
        }

        [HttpPut]
        public ActionResult Put([FromBody] BookVO book)
        {
            if(book is null)
                return BadRequest();

            return Ok(_bookBusiness.Update(book));
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _bookBusiness.Delete(id);
            return NoContent();
        }
    }
}