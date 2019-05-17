using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using pavlovLab.Models;
using pavlovLab.Storage;

namespace pavlovLab.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyModController : ControllerBase
    {
        private IStorage<MyModData> _memCache;

        public MyModController(IStorage<MyModData> memCache)
        {
            _memCache = memCache;
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<MyModData>> Get()
        {
            return Ok(_memCache.All);
        }

        [HttpGet("{id}")]
        public ActionResult<MyModData> Get(Guid id)
        {
            if (!_memCache.Has(id)) return NotFound("No such");

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
        public IActionResult Put(Guid id, [FromBody] MyModData value)
        {
           if (!_memCache.Has(id)) return NotFound("No such");

           var validationResult = value.Validate();

           if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

           var previousValue = _memCache[id];
           _memCache[id] = value;

           return Ok($"{previousValue.ToString()} has been updated to {value.ToString()}");

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            if (!_memCache.Has(id)) return NotFound("No such");

           var valueToRemove = _memCache[id];
           _memCache.RemoveAt(id);

           return Ok($"{valueToRemove.ToString()} has been removed");

        }
    }
}