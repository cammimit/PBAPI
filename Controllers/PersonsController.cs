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

    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly PersonService _personService;

        public PersonsController(PersonService personService)
        {
            _personService = personService;
        }

        [HttpGet]
        public ActionResult<List<PBEntry>> Get() => _personService.Get();
        /*
        //hack it
        //public ActionResult<PBEntry> Get()
        //{
        //    var pbentry = new PBEntry();
        //    pbentry.FirstName = "First";
        //    pbentry.Surname = "Surname";
        //    pbentry.PreferredName = "Preferred";
        //    pbentry.PrimaryEmail = "emailaddy";
        //    pbentry.PrimaryMobile = "numbers";
        //
        //    return pbentry;
        //}
        */


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

        [HttpPost]
        public ActionResult<PBEntry> Create(PBEntry pbentry)
        {
            _personService.Create(pbentry);

            return CreatedAtRoute("GetPBEntry", new { id = pbentry.Id.ToString() }, pbentry);
        }

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
    //  public class PersonsController : Controller
    //  {
    //     public IActionResult Index()
    //     {
    //         return View();
    //     }
    // }
}
