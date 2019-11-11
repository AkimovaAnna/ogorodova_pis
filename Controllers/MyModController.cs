using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using pavlovLab.Models;
using pavlovLab.Storage;
using Serilog;

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
            Log.Information("Acquiring product_ID info");
            Log.Error("Product ID does not exist");

            if (!_memCache.Has(id)) 
                {
                    Log.Information($"Acquired product_ID is {_memCache[id].Id}");
                    Log.Debug($"Product: {@_memCache[id]} does not exist in shop");
                    return NotFound("No such");
                }
        
            
           return Ok(_memCache[id]);
        }

        [HttpPost]
        public IActionResult Post([FromBody] MyModData value)
        {  
            Log.Information("Acquiring product info");
            Log.Error("The entered data is not correct");

           var validationResult = value.Validate();
           if (!validationResult.IsValid) 
                {
                    Log.Information($"Date added {DateTime.Now}");
                    Log.Debug($"Uncorrected data");
                    return BadRequest(validationResult.Errors);
                }
           _memCache.Add(value);

           return Ok($"{value.ToString()} has been added");

        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] MyModData value)
        {
            Log.Information("Acquiring product info");
            Log.Warning("Someone is try to change information");

           if (!_memCache.Has(id)) return NotFound("No such");

           var validationResult = value.Validate();

           if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

           var previousValue = _memCache[id];
           _memCache[id] = value;

            Log.Information($"Date change {DateTime.Now}");
            Log.Debug($"Attempt to change information");

           return Ok($"{previousValue.ToString()} has been updated to {value.ToString()}");

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            Log.Information("Acquiring product info");
            Log.Warning("Someone is try to delete information");

            if (!_memCache.Has(id)) return NotFound("No such");

           var valueToRemove = _memCache[id];
           _memCache.RemoveAt(id);

            Log.Information($"Date delete {DateTime.Now}");
            Log.Debug($"Attempt to delete information");

           return Ok($"{valueToRemove.ToString()} has been removed");

        }
    }
}