using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using pavlovLab.Models;

namespace pavlovLab.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyModController : ControllerBase
    {
        private static List<MyModData> _memCache = new List<MyModData>();

        [HttpGet]
        public ActionResult<IEnumerable<MyModData>> Get()
        {
            return Ok(_memCache);
        }

        [HttpGet("{id}")]
        public ActionResult<MyModData> Get(int id)
        {
            if (_memCache.Count <= id) return NotFound("No such");

           return Ok(_memCache[id]);
        }

        [HttpPost]
        public IActionResult Post([FromBody] MyModData value)
        {
            var validationResult = value.Validate();
           if (!validationResult.IsValid) return BadRequest(validationResult.Errors);
           _memCache.Add(value);
           return Ok($"{value.ToString()} has been added");

        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] MyModData value)
        {
           if (_memCache.Count <= id) return NotFound("No such");

           var validationResult = value.Validate();

           if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

           var previousValue = _memCache[id];
           _memCache[id] = value;

           return Ok($"{previousValue.ToString()} has been updated to {value.ToString()}");

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (_memCache.Count <= id) return NotFound("No such");

           var valueToRemove = _memCache[id];
           _memCache.RemoveAt(id);

           return Ok($"{valueToRemove.ToString()} has been removed");

        }
    }
}