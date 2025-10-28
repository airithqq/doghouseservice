using Microsoft.AspNetCore.Mvc;
using System;
using System.Reflection;
using TestTask.Application.DTO;
using TestTask.Application.Interfaces;

namespace TestTask.Controllers
{
    [ApiController]
    public class DogsController : ControllerBase
    {
        private readonly IDogsService _dogService;
        public DogsController(IDogsService dogService)
        {
            _dogService= dogService;
        }
        //[HttpGet("ping")]
        //public async Task<IActionResult> Ping()
        //{

        //}
        [HttpGet("dogs")] 
        public async Task<IActionResult> GetAll([FromQuery] string? attribute, [FromQuery] string? order)
        {
            var response = await _dogService.GetAllDogsAsync(attribute,order);
            return Ok(response);
        }
        [HttpPost("dog")]
        public async Task<IActionResult> Create([FromBody] CreateDogDTO dog)
        {
            var created = await _dogService.CreateDogAsync(dog);
            if (created == null) return BadRequest("Input correct values");
            return Ok(created);
        }
        [HttpGet("ping")]
        public string Ping()
        {
            var assembly = Assembly.GetExecutingAssembly().GetName();
            var version = Assembly.GetExecutingAssembly().GetName().Version!;
            var info = $"{assembly.Name}.Version{version.ToString(3)}";
            return info;
        }
    }
}
