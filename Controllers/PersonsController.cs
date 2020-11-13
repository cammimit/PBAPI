using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using PBAPI.Models;
using PBAPI.Services;

namespace PBAPI.Controllers
{
    //this can and should be refactored into partials to make it cleaner
    //there are too many individual parts here
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly PersonService _personService;

        public PersonsController(PersonService personService)
        {
            _personService = personService;
        }
        //simple get
        [HttpGet]
        public ActionResult<List<PBEntry>> Get() => _personService.Get();
        //overloaded with idarg
        [HttpGet("{id:length(24)}", Name = "GetPBEntry")]
        public ActionResult<PBEntry> Get(string id)
        {
            var person = _personService.Get(id);

            if (person == null)
            {
                return NotFound();
            }

            return person;
        }
        //simple post
        [HttpPost]
        public ActionResult<PBEntry> Create(PBEntry pbentry)
        {
            _personService.Create(pbentry);

            return CreatedAtRoute("GetPBEntry", new { id = pbentry.Id.ToString() }, pbentry);
        }
        //post with id to update
        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, PBEntry pbentryIn)
        {
            var person = _personService.Get(id);

            if (person == null)
            {
                return NotFound();
            }

            _personService.Update(id, pbentryIn);

            return NoContent();
        }
        //a delete
        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var person = _personService.Get(id);

            if (person == null)
            {
                return NotFound();
            }

            _personService.Remove(person.Id);

            return NoContent();
        }
    }
}
