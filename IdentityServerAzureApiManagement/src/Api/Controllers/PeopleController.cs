using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        protected List<Person> People = new List<Person>()
        {
            new Person() { Id = 1, Name = "Malcolm Reynolds", Active = true, Position = "Captain" },
            new Person() { Id = 2, Name = "Hoban Washburn", Active = false, Position = "Pilot" },
            new Person() { Id = 3, Name = "Zoe Washurn", Active = true, Position = "First Officer" },
            new Person() { Id = 4, Name = "Inara Serra", Active = false, Position = "Companion" },
            new Person() { Id = 5, Name = "Jayne Cobb", Active = true, Position = "Security Officer" },
            new Person() { Id = 6, Name = "Kaylee Frye", Active = true, Position = "Ship's Engineer" },
            new Person() { Id = 7, Name = "Simon Tam", Active = true, Position = "Doctor" },
            new Person() { Id = 8, Name = "River Tam", Active = true, Position = "Witch" }
        };

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Person>> Get()
        {
            return Ok(People);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<Person> GetById(int id)
        {
            return Ok(People.SingleOrDefault(p => p.Id == id));
        }
    }
}
